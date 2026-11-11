namespace FootballOrganizer.Models;

// команда - учасник турніру
public class Team
{
    public string Name { get; set; }
    public string City { get; set; }

    public Team(string name, string city = "")
    {
        Name = name;
        City = city;
    }

    public override string ToString()
    {
        if (City == "")
            return Name;
        return Name + " (" + City + ")";
    }
}
