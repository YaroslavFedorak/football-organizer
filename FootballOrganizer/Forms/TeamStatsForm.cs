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
}
