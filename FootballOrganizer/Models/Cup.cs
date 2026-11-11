namespace FootballOrganizer.Models;

// кубок, олімпійська система - хто програв, той вибуває
public class Cup : Tournament
{
    public Cup(string name, bool hasReturnMatches) : base(name, hasReturnMatches)
    {
    }

    public override string TypeName
    {
        get { return "Кубок"; }
    }

    public override void GenerateSchedule()
    {
        // TODO: сітка кубка, спочатку відомий тільки перший раунд
        throw new NotImplementedException();
    }
}
