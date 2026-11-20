using System.Drawing.Printing;
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

    // ---------------- друк таблиці ----------------

    private int printRow;

    private void buttonPrint_Click(object sender, EventArgs e)
    {
        PrintDocument doc = new PrintDocument();
        doc.DocumentName = league.Name;
        doc.BeginPrint += (s, args) => printRow = 0;
        doc.PrintPage += PrintTable;

        using PrintPreviewDialog preview = new PrintPreviewDialog();
        preview.Document = doc;
        preview.Text = "Попередній перегляд друку";
        preview.Width = 900;
        preview.Height = 750;
        preview.ShowDialog(this);
    }

    private void PrintTable(object? sender, PrintPageEventArgs e)
    {
        Graphics g = e.Graphics!;
        Rectangle area = e.MarginBounds;
        using Font title = new Font("Segoe UI", 14, FontStyle.Bold);
        using Font text = new Font("Segoe UI", 10);
        using Font bold = new Font("Segoe UI", 10, FontStyle.Bold);

        int y = area.Top;
        g.DrawString(league.Name, title, Brushes.Black, area.Left, y);
        y += 30;
        int played = league.Matches.Count(m => m.IsPlayed);
        g.DrawString("Турнірна таблиця станом на " + DateTime.Now.ToString("dd.MM.yyyy") +
            ", зіграно матчів: " + played + " з " + league.Matches.Count, text, Brushes.Gray, area.Left, y);
        y += 35;

        // ширина колонок: №, команда, І, В, Н, П, м'ячі, різниця, очки
        string[] headers = { "№", "Команда", "І", "В", "Н", "П", "М'ячі", "Р", "О" };
        int[] widths = { 35, 260, 45, 45, 45, 45, 80, 50, 50 };
        int rowH = 24;

        DrawRow(g, headers, widths, area.Left, y, rowH, bold, Brushes.Gainsboro);
        y += rowH;

        List<StandingRow> table = league.GetStandings();
        while (printRow < table.Count)
        {
            // якщо не влазить на аркуш - друкуємо далі на наступному
            if (y + rowH > area.Bottom)
            {
                e.HasMorePages = true;
                return;
            }
            StandingRow r = table[printRow];
            string diff = r.GoalDifference > 0 ? "+" + r.GoalDifference : r.GoalDifference.ToString();
            string[] cells = { (printRow + 1).ToString(), r.Team.Name, r.Played.ToString(), r.Won.ToString(),
                r.Drawn.ToString(), r.Lost.ToString(), r.GoalsFor + "-" + r.GoalsAgainst, diff, r.Points.ToString() };
            DrawRow(g, cells, widths, area.Left, y, rowH, text, null);
            y += rowH;
            printRow++;
        }
        e.HasMorePages = false;
    }

    private void DrawRow(Graphics g, string[] cells, int[] widths, int x, int y, int h, Font font, Brush? back)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            Rectangle cell = new Rectangle(x, y, widths[i], h);
            if (back != null)
                g.FillRectangle(back, cell);
            g.DrawRectangle(Pens.Gray, cell);
            g.DrawString(cells[i], font, Brushes.Black, x + 4, y + 4);
            x += widths[i];
        }
    }
}
