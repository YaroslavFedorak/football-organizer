using FootballOrganizer.Models;

namespace FootballOrganizer.Forms;

// календар, результати і таблиця чемпіонату
public partial class LeagueForm : Form
{
    private League league;

    public LeagueForm(League league)
    {
        InitializeComponent();
        this.league = league;
        Text = league.Name;

        FillRounds();
        RefreshTable();
    }

    private void FillRounds()
    {
        comboBoxRound.Items.Clear();
        listBoxMatches.Items.Clear();

        int rounds = league.RoundsCount;
        if (rounds == 0)
        {
            labelHint.Text = "Календар ще не складено. Натисніть \"Скласти календар\".";
            return;
        }

        labelHint.Text = "Турів: " + rounds + ", матчів: " + league.Matches.Count;
        for (int i = 1; i <= rounds; i++)
            comboBoxRound.Items.Add("Тур " + i);
        comboBoxRound.SelectedIndex = 0;
    }

    private void ShowRound()
    {
        int index = listBoxMatches.SelectedIndex;
        listBoxMatches.Items.Clear();
        if (comboBoxRound.SelectedIndex < 0)
            return;

        foreach (Match m in league.GetRound(comboBoxRound.SelectedIndex + 1))
            listBoxMatches.Items.Add(m);

        // щоб після збереження виділення не злітало
        if (index >= 0 && index < listBoxMatches.Items.Count)
            listBoxMatches.SelectedIndex = index;
    }

    private void RefreshTable()
    {
        dataGridViewTable.Rows.Clear();
        int place = 1;
        foreach (StandingRow row in league.GetStandings())
        {
            string diff = row.GoalDifference > 0 ? "+" + row.GoalDifference : row.GoalDifference.ToString();
            dataGridViewTable.Rows.Add(place, row.Team.Name, row.Played, row.Won, row.Drawn, row.Lost,
                row.GoalsFor + "-" + row.GoalsAgainst, diff, row.Points);
            place++;
        }
    }

    private void buttonGenerate_Click(object sender, EventArgs e)
    {
        if (league.Matches.Any(m => m.IsPlayed))
        {
            DialogResult answer = MessageBox.Show("Вже є зіграні матчі, їх результати буде видалено. Продовжити?",
                "Скласти календар", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer != DialogResult.Yes)
                return;
        }

        league.GenerateSchedule();
        FillRounds();
        RefreshTable();
    }

    private void comboBoxRound_SelectedIndexChanged(object sender, EventArgs e)
    {
        listBoxMatches.SelectedIndex = -1;
        ShowRound();
    }

    private void listBoxMatches_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listBoxMatches.SelectedItem is not Match m)
        {
            labelMatch.Text = "Виберіть матч";
            return;
        }

        labelMatch.Text = m.HomeTeam.Name + " - " + m.AwayTeam.Name;
        numericHome.Value = m.HomeGoals ?? 0;
        numericAway.Value = m.AwayGoals ?? 0;
    }

    private void buttonSave_Click(object sender, EventArgs e)
    {
        if (listBoxMatches.SelectedItem is not Match m)
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

        ShowRound();
        RefreshTable();
    }

    private void buttonClear_Click(object sender, EventArgs e)
    {
        if (listBoxMatches.SelectedItem is Match m)
        {
            m.ClearResult();
            ShowRound();
            RefreshTable();
        }
    }
}
