namespace FootballOrganizer.Models;

// базовий клас для всіх турнірів
public abstract class Tournament
{
    public string Name { get; set; }

    // чи граються матчі у відповідь
    public bool HasReturnMatches { get; set; }

    public List<Team> Teams { get; private set; } = new List<Team>();
    public List<Match> Matches { get; private set; } = new List<Match>();

    protected Tournament(string name, bool hasReturnMatches)
    {
        Name = name;
        HasReturnMatches = hasReturnMatches;
    }

    // "Чемпіонат" або "Кубок"
    public abstract string TypeName { get; }

    // складає календар матчів
    public abstract void GenerateSchedule();

    public bool HasTeam(string name)
    {
        return Teams.Any(t => string.Equals(t.Name, name.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    public void AddTeam(Team team)
    {
        if (HasTeam(team.Name))
            throw new ArgumentException("Команда \"" + team.Name + "\" вже є в турнірі");
        Teams.Add(team);
    }

    public void RemoveTeam(Team team)
    {
        Teams.Remove(team);
    }

    public override string ToString()
    {
        return Name + " [" + TypeName + "]";
    }
}
