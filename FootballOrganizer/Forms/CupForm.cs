using FootballOrganizer.Models;

namespace FootballOrganizer.Forms;

// раунди, результати і сітка кубка
public partial class CupForm : Form
{
    private Cup cup;

    public CupForm(Cup cup)
    {
        InitializeComponent();
        this.cup = cup;
        Text = cup.Name;
        FillRounds();
    }

    private void FillRounds()
    {
        comboBoxRound.Items.Clear();
        listBoxTies.Items.Clear();
        ShowTie(null);

        if (cup.Rounds.Count == 0)
        {
            labelStatus.Text = "Жеребкування ще не проводилось";
            buttonNext.Enabled = false;
            return;
        }

        for (int i = 0; i < cup.Rounds.Count; i++)
            comboBoxRound.Items.Add(cup.RoundName(i + 1));
        comboBoxRound.SelectedIndex = cup.Rounds.Count - 1;

        UpdateStatus();
    }

    private void UpdateStatus()
    {
        if (cup.Champion != null)
            labelStatus.Text = "Володар кубка: " + cup.Champion.Name;
        else
            labelStatus.Text = "Команд: " + cup.Teams.Count + ", раундів до фіналу: " + cup.TotalRounds;
        buttonNext.Enabled = cup.CanAdvance;
    }

    private void ShowTies()
    {
        int index = listBoxTies.SelectedIndex;
        listBoxTies.Items.Clear();
        if (comboBoxRound.SelectedIndex < 0)
            return;

        foreach (CupTie tie in cup.Rounds[comboBoxRound.SelectedIndex])
            listBoxTies.Items.Add(tie);

        if (index >= 0 && index < listBoxTies.Items.Count)
            listBoxTies.SelectedIndex = index;
    }

    private void ShowTie(CupTie? tie)
    {
        listBoxMatches.Items.Clear();
        comboBoxPenalty.Items.Clear();
        comboBoxPenalty.Enabled = false;
        buttonPenalty.Enabled = false;

        if (tie == null)
        {
            labelTie.Text = "Виберіть пару";
            return;
        }

        labelTie.Text = tie.ToString();
        foreach (Match m in tie.Matches)
            listBoxMatches.Items.Add(m);
        if (listBoxMatches.Items.Count > 0)
            listBoxMatches.SelectedIndex = 0;

        if (tie.NeedsPenalties)
        {
            comboBoxPenalty.Items.Add(tie.TeamA);
            comboBoxPenalty.Items.Add(tie.TeamB!);
            comboBoxPenalty.Enabled = true;
            buttonPenalty.Enabled = true;
            if (tie.PenaltyWinner != null)
                comboBoxPenalty.SelectedItem = tie.PenaltyWinner;
        }
    }

    // результати можна міняти тільки в останньому раунді,
    // бо інакше поламаються пари наступних раундів
    private bool IsCurrentRound()
    {
        if (comboBoxRound.SelectedIndex != cup.Rounds.Count - 1)
        {
            MessageBox.Show("Результати попередніх раундів змінювати не можна", "Кубок",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
        return true;
    }

    private void RefreshAfterChange()
    {
        ShowTies();
        ShowTie(listBoxTies.SelectedItem as CupTie);
        UpdateStatus();
    }

    private void buttonDraw_Click(object sender, EventArgs e)
    {
        if (cup.Teams.Count < 2)
            return;

        if (cup.Matches.Any(m => m.IsPlayed))
        {
            DialogResult answer = MessageBox.Show("Всі результати кубка буде видалено. Провести жеребкування заново?",
                "Жеребкування", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer != DialogResult.Yes)
                return;
        }

        cup.GenerateSchedule();
        FillRounds();
    }

    private void comboBoxRound_SelectedIndexChanged(object sender, EventArgs e)
    {
        listBoxTies.SelectedIndex = -1;
        ShowTies();
    }

    private void listBoxTies_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowTie(listBoxTies.SelectedItem as CupTie);
    }

    private void listBoxMatches_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listBoxMatches.SelectedItem is Match m)
        {
            numericHome.Value = m.HomeGoals ?? 0;
            numericAway.Value = m.AwayGoals ?? 0;
        }
    }

    private void buttonSave_Click(object sender, EventArgs e)
    {
        if (listBoxTies.SelectedItem is not CupTie tie || listBoxMatches.SelectedItem is not Match m)
            return;
        if (!IsCurrentRound())
            return;

        try
        {
            m.SetResult((int)numericHome.Value, (int)numericAway.Value);
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // рахунок змінився - старе пенальті вже не актуальне
        if (!tie.NeedsPenalties)
            tie.PenaltyWinner = null;

        int matchIndex = listBoxMatches.SelectedIndex;
        RefreshAfterChange();
        if (matchIndex < listBoxMatches.Items.Count)
            listBoxMatches.SelectedIndex = matchIndex;
    }

    private void buttonClear_Click(object sender, EventArgs e)
    {
        if (listBoxTies.SelectedItem is not CupTie tie || listBoxMatches.SelectedItem is not Match m)
            return;
        if (!IsCurrentRound())
            return;

        m.ClearResult();
        tie.PenaltyWinner = null;
        RefreshAfterChange();
    }

    private void buttonPenalty_Click(object sender, EventArgs e)
    {
        if (listBoxTies.SelectedItem is not CupTie tie)
            return;
        if (!IsCurrentRound())
            return;
        if (comboBoxPenalty.SelectedItem is not Team winner)
        {
            MessageBox.Show("Виберіть команду, яка виграла серію пенальті", "Пенальті",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        tie.PenaltyWinner = winner;
        RefreshAfterChange();
    }

    private void buttonNext_Click(object sender, EventArgs e)
    {
        cup.AdvanceRound();
        FillRounds();
    }
}
