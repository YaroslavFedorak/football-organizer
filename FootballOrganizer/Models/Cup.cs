namespace FootballOrganizer.Models;

// кубок, олімпійська система - хто програв, той вибуває
public class Cup : Tournament
{
    // раунди кубка, в кожному список пар
    public List<List<CupTie>> Rounds { get; private set; } = new List<List<CupTie>>();

    private static Random random = new Random();

    public Cup(string name, bool hasReturnMatches) : base(name, hasReturnMatches)
    {
    }

    public override string TypeName
    {
        get { return "Кубок"; }
    }

    public override void ClearSchedule()
    {
        base.ClearSchedule();
        Rounds.Clear();
    }

    // розмір сітки - найближчий степінь двійки (4, 8, 16...)
    public int BracketSize
    {
        get
        {
            int size = 2;
            while (size < Teams.Count)
                size *= 2;
            return size;
        }
    }

    // скільки всього раундів до фіналу включно
    public int TotalRounds
    {
        get { return (int)Math.Log2(BracketSize); }
    }

    // жеребкування першого раунду
    // далі пари стають відомі тільки після зіграних матчів
    public override void GenerateSchedule()
    {
        Matches.Clear();
        Rounds.Clear();

        // перемішуємо команди
        List<Team> list = Teams.OrderBy(t => random.Next()).ToList();

        // якщо команд не 4, 8, 16... то частина команд проходить далі без гри
        int byes = BracketSize - list.Count;

        // вільні проходи розкидаємо по сітці рівномірно,
        // інакше команди без гри зустрічаються між собою вже в наступному раунді
        int slots = BracketSize / 2;
        HashSet<int> byeSlots = new HashSet<int>();
        for (int k = 0; k < byes; k++)
            byeSlots.Add(k * slots / byes);

        List<CupTie> first = new List<CupTie>();
        int i = 0;
        for (int slot = 0; slot < slots; slot++)
        {
            if (byeSlots.Contains(slot))
            {
                first.Add(new CupTie(list[i], null));
                i++;
            }
            else
            {
                first.Add(CreateTie(list[i], list[i + 1], 1));
                i += 2;
            }
        }
        Rounds.Add(first);
    }

    private CupTie CreateTie(Team a, Team b, int round)
    {
        CupTie tie = new CupTie(a, b);
        Match m1 = new Match(a, b, round);
        tie.Matches.Add(m1);
        Matches.Add(m1);

        // фінал завжди один матч
        bool isFinal = round == TotalRounds;
        if (HasReturnMatches && !isFinal)
        {
            Match m2 = new Match(b, a, round);
            tie.Matches.Add(m2);
            Matches.Add(m2);
        }
        return tie;
    }

    public List<CupTie>? CurrentRound
    {
        get { return Rounds.Count == 0 ? null : Rounds[Rounds.Count - 1]; }
    }

    // можна переходити далі, якщо в поточному раунді всі пари вирішені і це не фінал
    public bool CanAdvance
    {
        get
        {
            List<CupTie>? current = CurrentRound;
            return current != null && current.Count > 1 && current.All(t => t.IsFinished);
        }
    }

    // переможець фіналу
    public Team? Champion
    {
        get
        {
            List<CupTie>? current = CurrentRound;
            if (current == null || current.Count != 1)
                return null;
            return current[0].Winner;
        }
    }

    // переможці пар 1 і 2 грають між собою, 3 і 4 між собою і т.д.
    public void AdvanceRound()
    {
        if (!CanAdvance)
            throw new InvalidOperationException("Ще не всі пари поточного раунду зіграні");

        List<CupTie> current = CurrentRound!;
        int round = Rounds.Count + 1;
        List<CupTie> next = new List<CupTie>();
        for (int i = 0; i < current.Count; i += 2)
            next.Add(CreateTie(current[i].Winner!, current[i + 1].Winner!, round));
        Rounds.Add(next);
    }

    // назва раунду: 1/8 фіналу, 1/4, півфінал, фінал
    public string RoundName(int round)
    {
        int ties = BracketSize / (int)Math.Pow(2, round);
        if (ties == 1)
            return "Фінал";
        if (ties == 2)
            return "1/2 фіналу";
        return "1/" + ties + " фіналу";
    }
}
