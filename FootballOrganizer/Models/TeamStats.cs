namespace FootballOrganizer.Models;

// статистика однієї команди в турнірі
public class TeamStats
{
    // велика перемога - коли виграли з різницею 3 м'ячі і більше
    public const int BigWinDifference = 3;

    public Team Team { get; private set; }
    public List<Match> PlayedMatches { get; private set; }

    public int Won { get; private set; }
    public int Drawn { get; private set; }
    public int Lost { get; private set; }
    public int GoalsFor { get; private set; }
    public int GoalsAgainst { get; private set; }
    public int BigWins { get; private set; }
    public int LongestUnbeatenStreak { get; private set; }
    public Match? BiggestWin { get; private set; }

    public TeamStats(Tournament tournament, Team team)
    {
        Team = team;
        // матчі команди по порядку турів
        PlayedMatches = tournament.Matches
            .Where(m => m.IsPlayed && (m.HomeTeam == team || m.AwayTeam == team))
            .OrderBy(m => m.Round)
            .ToList();

        int streak = 0;
        int biggestDiff = 0;
        foreach (Match m in PlayedMatches)
        {
            int scored = Scored(m);
            int conceded = Conceded(m);
            GoalsFor += scored;
            GoalsAgainst += conceded;

            if (scored > conceded)
            {
                Won++;
                if (scored - conceded >= BigWinDifference)
                    BigWins++;
                if (scored - conceded > biggestDiff)
                {
                    biggestDiff = scored - conceded;
                    BiggestWin = m;
                }
            }
            else if (scored == conceded)
                Drawn++;
            else
                Lost++;

            // безпрограшна серія - перемоги і нічиї підряд
            if (scored >= conceded)
            {
                streak++;
                if (streak > LongestUnbeatenStreak)
                    LongestUnbeatenStreak = streak;
            }
            else
                streak = 0;
        }
    }

    public int Played
    {
        get { return PlayedMatches.Count; }
    }

    public int Scored(Match m)
    {
        return m.HomeTeam == Team ? m.HomeGoals!.Value : m.AwayGoals!.Value;
    }

    public int Conceded(Match m)
    {
        return m.HomeTeam == Team ? m.AwayGoals!.Value : m.HomeGoals!.Value;
    }

    // "В", "Н" або "П"
    public string ResultLetter(Match m)
    {
        int s = Scored(m);
        int c = Conceded(m);
        if (s > c) return "В";
        if (s == c) return "Н";
        return "П";
    }

    // місце команди в таблиці після кожного туру (тільки для чемпіонату)
    // ключ - номер туру, значення - місце
    public static Dictionary<int, int> PlacesByRound(League league, Team team)
    {
        Dictionary<int, int> places = new Dictionary<int, int>();
        int lastRound = 0;
        foreach (Match m in league.Matches)
        {
            if (m.IsPlayed && m.Round > lastRound)
                lastRound = m.Round;
        }

        for (int r = 1; r <= lastRound; r++)
        {
            List<StandingRow> table = league.GetStandings(r);
            int place = table.FindIndex(row => row.Team == team) + 1;
            if (place > 0)
                places[r] = place;
        }
        return places;
    }
}
