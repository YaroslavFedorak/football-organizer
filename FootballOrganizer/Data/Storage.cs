using System.Text.Json;
using FootballOrganizer.Models;

namespace FootballOrganizer.Data;

// збереження і завантаження турнірів у JSON файл
// об'єкти посилаються один на одного (матч -> команда), тому в файл
// пишемо номери команд і матчів у списках, а при завантаженні збираємо назад
public static class Storage
{
    // файл за замовчуванням: C:\Users\...\AppData\Roaming\FootballOrganizer\tournaments.json
    public static string DefaultPath
    {
        get
        {
            string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FootballOrganizer");
            return Path.Combine(dir, "tournaments.json");
        }
    }

    private static JsonSerializerOptions options = new JsonSerializerOptions
    {
        WriteIndented = true,
        // щоб українські літери були нормальні, а не \u0456
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public static void Save(string path, List<Tournament> tournaments)
    {
        List<TournamentDto> list = new List<TournamentDto>();
        foreach (Tournament t in tournaments)
            list.Add(ToDto(t));

        string? dir = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(dir))
            Directory.CreateDirectory(dir);
        File.WriteAllText(path, JsonSerializer.Serialize(list, options));
    }

    public static List<Tournament> Load(string path)
    {
        string json = File.ReadAllText(path);
        List<TournamentDto>? list = JsonSerializer.Deserialize<List<TournamentDto>>(json, options);
        if (list == null)
            throw new InvalidDataException("Файл порожній або пошкоджений");

        List<Tournament> result = new List<Tournament>();
        foreach (TournamentDto dto in list)
            result.Add(FromDto(dto));
        return result;
    }

    private static TournamentDto ToDto(Tournament t)
    {
        TournamentDto dto = new TournamentDto
        {
            Type = t is Cup ? "cup" : "league",
            Name = t.Name,
            HasReturnMatches = t.HasReturnMatches
        };

        foreach (Team team in t.Teams)
            dto.Teams.Add(new TeamDto { Name = team.Name, City = team.City });

        foreach (Match m in t.Matches)
        {
            dto.Matches.Add(new MatchDto
            {
                Home = t.Teams.IndexOf(m.HomeTeam),
                Away = t.Teams.IndexOf(m.AwayTeam),
                Round = m.Round,
                HomeGoals = m.HomeGoals,
                AwayGoals = m.AwayGoals
            });
        }

        if (t is Cup cup)
        {
            foreach (List<CupTie> round in cup.Rounds)
            {
                List<TieDto> r = new List<TieDto>();
                foreach (CupTie tie in round)
                {
                    r.Add(new TieDto
                    {
                        A = t.Teams.IndexOf(tie.TeamA),
                        B = tie.TeamB == null ? null : t.Teams.IndexOf(tie.TeamB),
                        Matches = tie.Matches.Select(m => t.Matches.IndexOf(m)).ToList(),
                        PenaltyWinner = tie.PenaltyWinner == null ? null : t.Teams.IndexOf(tie.PenaltyWinner)
                    });
                }
                dto.Rounds.Add(r);
            }
        }
        return dto;
    }

    private static Tournament FromDto(TournamentDto dto)
    {
        Tournament t;
        if (dto.Type == "cup")
            t = new Cup(dto.Name, dto.HasReturnMatches);
        else
            t = new League(dto.Name, dto.HasReturnMatches);

        foreach (TeamDto team in dto.Teams)
            t.AddTeam(new Team(team.Name, team.City));

        foreach (MatchDto md in dto.Matches)
        {
            Match m = new Match(t.Teams[md.Home], t.Teams[md.Away], md.Round);
            if (md.HomeGoals != null && md.AwayGoals != null)
                m.SetResult(md.HomeGoals.Value, md.AwayGoals.Value);
            t.Matches.Add(m);
        }

        if (t is Cup cup)
        {
            foreach (List<TieDto> r in dto.Rounds)
            {
                List<CupTie> round = new List<CupTie>();
                foreach (TieDto td in r)
                {
                    CupTie tie = new CupTie(t.Teams[td.A], td.B == null ? null : t.Teams[td.B.Value]);
                    foreach (int mi in td.Matches)
                        tie.Matches.Add(t.Matches[mi]);
                    if (td.PenaltyWinner != null)
                        tie.PenaltyWinner = t.Teams[td.PenaltyWinner.Value];
                    round.Add(tie);
                }
                cup.Rounds.Add(round);
            }
        }
        return t;
    }
}

// класи тільки для файлу
public class TournamentDto
{
    public string Type { get; set; } = "league";
    public string Name { get; set; } = "";
    public bool HasReturnMatches { get; set; }
    public List<TeamDto> Teams { get; set; } = new List<TeamDto>();
    public List<MatchDto> Matches { get; set; } = new List<MatchDto>();
    public List<List<TieDto>> Rounds { get; set; } = new List<List<TieDto>>();
}

public class TeamDto
{
    public string Name { get; set; } = "";
    public string City { get; set; } = "";
}

public class MatchDto
{
    public int Home { get; set; }
    public int Away { get; set; }
    public int Round { get; set; }
    public int? HomeGoals { get; set; }
    public int? AwayGoals { get; set; }
}

public class TieDto
{
    public int A { get; set; }
    public int? B { get; set; }
    public List<int> Matches { get; set; } = new List<int>();
    public int? PenaltyWinner { get; set; }
}
