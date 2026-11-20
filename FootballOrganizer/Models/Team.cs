namespace FootballOrganizer.Models;

// команда - учасник турніру
// назва і місто задаються тільки при створенні
public class Team
{
    public string Name { get; private set; }
    public string City { get; private set; }

    public Team(string name, string city = "")
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Назва команди не може бути порожньою");
        Name = name.Trim();
        City = city.Trim();
    }

    public override string ToString()
    {
        if (City == "")
            return Name;
        return Name + " (" + City + ")";
    }
}
