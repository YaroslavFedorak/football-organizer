namespace FootballOrganizer.Models;

// один рядок турнірної таблиці
public class StandingRow
{
    public Team Team { get; private set; }

    public int Played { get; private set; }
    public int Won { get; private set; }
    public int Drawn { get; private set; }
    public int Lost { get; private set; }
    public int GoalsFor { get; private set; }
    public int GoalsAgainst { get; private set; }

    public StandingRow(Team team)
    {
        Team = team;
    }

    public int GoalDifference
    {
        get { return GoalsFor - GoalsAgainst; }
    }

    // перемога - 3 очки, нічия - 1
    public int Points
    {
        get { return Won * 3 + Drawn; }
    }

    public void AddResult(int scored, int conceded)
    {
        Played++;
        GoalsFor += scored;
        GoalsAgainst += conceded;

        if (scored > conceded)
            Won++;
        else if (scored == conceded)
            Drawn++;
        else
            Lost++;
    }
}
