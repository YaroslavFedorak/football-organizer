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
            panelCanvas.Invalidate();
            return;
        }

        for (int i = 0; i < cup.Rounds.Count; i++)
            comboBoxRound.Items.Add(cup.RoundName(i + 1));
        comboBoxRound.SelectedIndex = cup.Rounds.Count - 1;

        UpdateStatus();
    }

    private void UpdateStatus()
    {
        ResizeCanvas();
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

    // ---------------- сітка кубка ----------------

    private const int BoxW = 190;
    private const int BoxH = 44;
    private const int Gap = 50;
    private const int Top = 20;
    private const int Left = 20;

    // y центру пари j в раунді r (r з нуля)
    // пара наступного раунду стоїть посередині між двома своїми попередніми
    private int CenterY(int r, int j)
    {
        int unit = BoxH + 16;
        int k = (int)Math.Pow(2, r);
        return Top + BoxH / 2 + (int)((j * k + (k - 1) / 2.0) * unit);
    }

    private void ResizeCanvas()
    {
        if (cup.Teams.Count < 2)
            return;
        int rounds = cup.TotalRounds;
        int firstTies = cup.BracketSize / 2;
        panelCanvas.Width = Left + (rounds + 1) * (BoxW + Gap);
        panelCanvas.Height = Top * 2 + firstTies * (BoxH + 16);
        panelCanvas.Invalidate();
    }

    private void panelCanvas_Paint(object? sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.Clear(Color.White);
        if (cup.Rounds.Count == 0)
        {
            g.DrawString("Жеребкування ще не проводилось", Font, Brushes.Gray, Left, Top);
            return;
        }

        Font bold = new Font(Font, FontStyle.Bold);
        Pen line = new Pen(Color.Gray);
        int rounds = cup.TotalRounds;

        for (int r = 0; r < rounds; r++)
        {
            int ties = cup.BracketSize / (int)Math.Pow(2, r + 1);
            int x = Left + r * (BoxW + Gap);
            g.DrawString(cup.RoundName(r + 1), bold, Brushes.DarkBlue, x, 2);

            for (int j = 0; j < ties; j++)
            {
                int cy = CenterY(r, j);
                Rectangle box = new Rectangle(x, cy - BoxH / 2, BoxW, BoxH);
                g.FillRectangle(Brushes.WhiteSmoke, box);
                g.DrawRectangle(Pens.DarkGray, box);
                g.DrawLine(Pens.LightGray, box.Left, cy, box.Right, cy);

                CupTie? tie = null;
                if (r < cup.Rounds.Count && j < cup.Rounds[r].Count)
                    tie = cup.Rounds[r][j];

                string nameA = "?", nameB = "?", scoreA = "", scoreB = "";
                bool winA = false, winB = false;
                if (tie != null)
                {
                    nameA = tie.TeamA.Name;
                    nameB = tie.IsBye ? "вільний прохід" : tie.TeamB!.Name;
                    if (!tie.IsBye && tie.Matches.Any(m => m.IsPlayed))
                    {
                        scoreA = tie.GoalsA.ToString();
                        scoreB = tie.GoalsB.ToString();
                    }
                    winA = tie.Winner == tie.TeamA;
                    winB = tie.Winner != null && tie.Winner == tie.TeamB;
                }

                g.DrawString(nameA, winA ? bold : Font, Brushes.Black, x + 4, cy - BoxH / 2 + 3);
                g.DrawString(nameB, winB ? bold : Font, tie != null && tie.IsBye ? Brushes.Gray : Brushes.Black, x + 4, cy + 3);
                g.DrawString(scoreA, Font, Brushes.Black, x + BoxW - 22, cy - BoxH / 2 + 3);
                g.DrawString(scoreB, Font, Brushes.Black, x + BoxW - 22, cy + 3);

                // лінія до пари наступного раунду
                if (r < rounds - 1)
                {
                    int midX = x + BoxW + Gap / 2;
                    int nextY = CenterY(r + 1, j / 2);
                    g.DrawLine(line, x + BoxW, cy, midX, cy);
                    g.DrawLine(line, midX, cy, midX, nextY);
                    g.DrawLine(line, midX, nextY, x + BoxW + Gap, nextY);
                }
            }
        }

        // переможець
        if (cup.Champion != null)
        {
            int x = Left + rounds * (BoxW + Gap);
            int cy = CenterY(rounds - 1, 0);
            g.DrawLine(line, x - Gap, cy, x, cy);
            g.DrawString("Володар кубка:", Font, Brushes.DarkGreen, x, cy - 20);
            g.DrawString(cup.Champion.Name, bold, Brushes.DarkGreen, x, cy);
        }

        bold.Dispose();
        line.Dispose();
    }
}
