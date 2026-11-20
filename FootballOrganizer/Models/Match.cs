namespace FootballOrganizer.Models;

public class Match
{
    public Team HomeTeam { get; private set; }
    public Team AwayTeam { get; private set; }

    // null поки матч не зіграний, змінюються тільки через SetResult() і ClearResult()
    public int? HomeGoals { get; private set; }
    public int? AwayGoals { get; private set; }

    // номер туру (для кубка - номер раунду)
    public int Round { get; private set; }

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

    public void SetResult(int homeGoals, int awayGoals)
    {
        if (homeGoals < 0 || awayGoals < 0)
            throw new ArgumentException("Кількість голів не може бути від'ємною");

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
