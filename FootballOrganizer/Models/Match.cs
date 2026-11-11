namespace FootballOrganizer.Models;

public class Match
{
    public Team HomeTeam { get; set; }
    public Team AwayTeam { get; set; }

    // null поки матч не зіграний
    public int? HomeGoals { get; set; }
    public int? AwayGoals { get; set; }

    // номер туру (для кубка - номер раунду)
    public int Round { get; set; }

    public Match(Team home, Team away, int round)
    {
        HomeTeam = home;
        AwayTeam = away;
        Round = round;
    }

    public bool IsPlayed
    {
        get { return HomeGoals != null && AwayGoals != null; }
    }

    // TODO: перевірка що голи не відємні
    public void SetResult(int homeGoals, int awayGoals)
    {
        HomeGoals = homeGoals;
        AwayGoals = awayGoals;
    }

    public void ClearResult()
    {
        HomeGoals = null;
        AwayGoals = null;
    }

    public override string ToString()
    {
        string score = IsPlayed ? HomeGoals + ":" + AwayGoals : "-:-";
        return HomeTeam.Name + " " + score + " " + AwayTeam.Name;
    }
}
