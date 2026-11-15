namespace FootballOrganizer.Models;

// пара команд у кубку: один матч або два (вдома і в гостях)
public class CupTie
{
    public Team TeamA { get; private set; }
    // null - у команди А вільний прохід в наступний раунд
    public Team? TeamB { get; private set; }

    public List<Match> Matches { get; private set; } = new List<Match>();

    // якщо за сумою матчів нічия - хто виграв по пенальті
    public Team? PenaltyWinner { get; set; }

    public CupTie(Team teamA, Team? teamB)
    {
        TeamA = teamA;
        TeamB = teamB;
    }

    public bool IsBye
    {
        get { return TeamB == null; }
    }

    public bool AllPlayed
    {
        get { return Matches.All(m => m.IsPlayed); }
    }

    // скільки забила команда А за всі матчі пари
    public int GoalsA
    {
        get
        {
            int sum = 0;
            foreach (Match m in Matches)
            {
                if (!m.IsPlayed)
                    continue;
                sum += m.HomeTeam == TeamA ? m.HomeGoals!.Value : m.AwayGoals!.Value;
            }
            return sum;
        }
    }

    public int GoalsB
    {
        get
        {
            int sum = 0;
            foreach (Match m in Matches)
            {
                if (!m.IsPlayed)
                    continue;
                sum += m.HomeTeam == TeamA ? m.AwayGoals!.Value : m.HomeGoals!.Value;
            }
            return sum;
        }
    }

    // всі матчі зіграні а рахунок рівний - треба пенальті
    public bool NeedsPenalties
    {
        get { return !IsBye && AllPlayed && GoalsA == GoalsB; }
    }

    public Team? Winner
    {
        get
        {
            if (IsBye)
                return TeamA;
            if (!AllPlayed)
                return null;
            if (GoalsA > GoalsB)
                return TeamA;
            if (GoalsB > GoalsA)
                return TeamB;
            return PenaltyWinner;
        }
    }

    public bool IsFinished
    {
        get { return Winner != null; }
    }

    public override string ToString()
    {
        if (IsBye)
            return TeamA.Name + " - вільний прохід";

        string text = TeamA.Name + " - " + TeamB!.Name;
        if (Matches.Any(m => m.IsPlayed))
        {
            text += "   " + string.Join(", ", Matches.Where(m => m.IsPlayed).Select(m => m.HomeGoals + ":" + m.AwayGoals));
            if (Matches.Count > 1)
                text += " (сума " + GoalsA + ":" + GoalsB + ")";
        }
        if (PenaltyWinner != null)
            text += ", пен. - " + PenaltyWinner.Name;
        return text;
    }
}
