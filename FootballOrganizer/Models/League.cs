namespace FootballOrganizer.Models;

// чемпіонат, кругова система - кожен грає з кожним
public class League : Tournament
{
    public League(string name, bool hasReturnMatches) : base(name, hasReturnMatches)
    {
    }

    public override string TypeName
    {
        get { return "Чемпіонат"; }
    }

    // скільки всього турів
    public int RoundsCount
    {
        get
        {
            if (Matches.Count == 0)
                return 0;
            return Matches.Max(m => m.Round);
        }
    }

    // календар по круговій системі (метод "кола"):
    // перша команда стоїть на місці, решта кожен тур зсуваються по колу
    public override void GenerateSchedule()
    {
        Matches.Clear();

        List<Team?> list = new List<Team?>(Teams);
        // якщо команд непарна кількість, додаємо "пусте місце"
        // хто з ним в парі - той в цьому турі відпочиває
        if (list.Count % 2 != 0)
            list.Add(null);

        int n = list.Count;
        int rounds = n - 1;

        for (int r = 0; r < rounds; r++)
        {
            for (int i = 0; i < n / 2; i++)
            {
                Team? a = list[i];
                Team? b = list[n - 1 - i];
                if (a == null || b == null)
                    continue;

                // щоб перша команда не грала весь час вдома
                if (i == 0 && r % 2 == 1)
                    Matches.Add(new Match(b, a, r + 1));
                else
                    Matches.Add(new Match(a, b, r + 1));
            }

            // зсув: остання команда стає на друге місце
            Team? last = list[n - 1];
            list.RemoveAt(n - 1);
            list.Insert(1, last);
        }

        // друге коло - ті самі пари, але господарі і гості міняються
        if (HasReturnMatches)
        {
            int count = Matches.Count;
            for (int i = 0; i < count; i++)
            {
                Match m = Matches[i];
                Matches.Add(new Match(m.AwayTeam, m.HomeTeam, m.Round + rounds));
            }
        }
    }

    public List<Match> GetRound(int round)
    {
        return Matches.Where(m => m.Round == round).ToList();
    }
}
