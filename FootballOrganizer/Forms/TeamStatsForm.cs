using FootballOrganizer.Models;

namespace FootballOrganizer.Forms;

// статистика вибраної команди в турнірі
public partial class TeamStatsForm : Form
{
    private Tournament tournament;
    private Dictionary<int, int> places = new Dictionary<int, int>();

    public TeamStatsForm(Tournament tournament)
    {
        InitializeComponent();
        this.tournament = tournament;
        Text = "Статистика - " + tournament.Name;

        foreach (Team t in tournament.Teams)
            comboBoxTeam.Items.Add(t);
        if (comboBoxTeam.Items.Count > 0)
            comboBoxTeam.SelectedIndex = 0;
    }

    private void comboBoxTeam_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (comboBoxTeam.SelectedItem is Team team)
            ShowStats(team);
    }

    private void ShowStats(Team team)
    {
        TeamStats stats = new TeamStats(tournament, team);

        string text = "Зіграно матчів: " + stats.Played + "\n" +
            "Перемоги / нічиї / поразки: " + stats.Won + " / " + stats.Drawn + " / " + stats.Lost + "\n" +
            "М'ячі: " + stats.GoalsFor + " - " + stats.GoalsAgainst + "\n" +
            "Найдовша безпрограшна серія: " + stats.LongestUnbeatenStreak + "\n" +
            "Великих перемог (різниця " + TeamStats.BigWinDifference + "+): " + stats.BigWins + "\n";
        if (stats.BiggestWin != null)
            text += "Найбільша перемога: " + stats.BiggestWin;
        else
            text += "Перемог поки немає";
        labelSummary.Text = text;

        listBoxMatches.Items.Clear();
        foreach (Match m in stats.PlayedMatches)
            listBoxMatches.Items.Add("Тур " + m.Round + ":  " + m + "  (" + stats.ResultLetter(m) + ")");
        if (stats.Played == 0)
            listBoxMatches.Items.Add("Команда ще не зіграла жодного матчу");

        if (tournament is League league)
            places = TeamStats.PlacesByRound(league, team);
        else
            places.Clear();
        panelChart.Invalidate();
    }

    // графік: по горизонталі тури, по вертикалі місце (1 місце зверху)
    private void panelChart_Paint(object? sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        if (tournament is not League league)
        {
            g.DrawString("Графік місця є тільки для чемпіонату", Font, Brushes.Gray, 10, 10);
            return;
        }
        if (places.Count == 0)
        {
            g.DrawString("Ще немає зіграних турів", Font, Brushes.Gray, 10, 10);
            return;
        }

        int left = 40, top = 15, right = 20, bottom = 30;
        int w = panelChart.Width - left - right;
        int h = panelChart.Height - top - bottom;
        int teams = league.Teams.Count;
        int rounds = places.Keys.Max();

        // горизонтальні лінії для кожного місця
        for (int p = 1; p <= teams; p++)
        {
            int y = top + (teams == 1 ? 0 : (p - 1) * h / (teams - 1));
            g.DrawLine(Pens.Gainsboro, left, y, left + w, y);
            g.DrawString(p.ToString(), Font, Brushes.Gray, 8, y - 8);
        }

        // підписи турів, щоб не налазили - через один якщо їх багато
        int step = rounds > 15 ? 2 : 1;
        List<Point> points = new List<Point>();
        for (int r = 1; r <= rounds; r++)
        {
            int x = left + (rounds == 1 ? w / 2 : (r - 1) * w / (rounds - 1));
            if ((r - 1) % step == 0)
                g.DrawString(r.ToString(), Font, Brushes.Gray, x - 5, top + h + 8);
            if (places.ContainsKey(r))
            {
                int p = places[r];
                int y = top + (teams == 1 ? 0 : (p - 1) * h / (teams - 1));
                points.Add(new Point(x, y));
            }
        }

        if (points.Count > 1)
        {
            using Pen pen = new Pen(Color.RoyalBlue, 2);
            g.DrawLines(pen, points.ToArray());
        }
        foreach (Point pt in points)
            g.FillEllipse(Brushes.RoyalBlue, pt.X - 4, pt.Y - 4, 8, 8);

        // підпис осі зліва знизу, щоб не налазив на номер останнього туру
        g.DrawString("тур:", Font, Brushes.Gray, 2, top + h + 8);
    }
}
