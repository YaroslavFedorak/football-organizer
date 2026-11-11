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

    public override void GenerateSchedule()
    {
        // TODO: генерація турів по круговій системі
        throw new NotImplementedException();
    }
}
