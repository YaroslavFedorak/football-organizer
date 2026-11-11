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

    public void AddTeam(Team team)
    {
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
