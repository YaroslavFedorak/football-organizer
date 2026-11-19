using FootballOrganizer.Data;
using FootballOrganizer.Models;

namespace FootballOrganizer.Tests;

public class MatchTests
{
    [Fact]
    public void SetResult_NegativeGoals_Throws()
    {
        Match m = new Match(new Team("А"), new Team("Б"), 1);
        Assert.Throws<ArgumentException>(() => m.SetResult(-1, 2));
        Assert.False(m.IsPlayed);
    }

    [Fact]
    public void ClearResult_MakesMatchNotPlayed()
    {
        Match m = new Match(new Team("А"), new Team("Б"), 1);
        m.SetResult(2, 0);
        Assert.True(m.IsPlayed);
        m.ClearResult();
        Assert.False(m.IsPlayed);
    }
}

public class TeamStatsTests
{
    [Fact]
    public void Stats_StreakAndBigWins()
    {
        Team a = new Team("А");
        Team b = new Team("Б");
        League league = new League("Тест", false);
        league.AddTeam(a);
        league.AddTeam(b);

        // результати команди А по турах: В 4:0, Н 1:1, В 2:1, П 0:1
        int[,] scores = { { 4, 0 }, { 1, 1 }, { 2, 1 }, { 0, 1 } };
        for (int i = 0; i < 4; i++)
        {
            Match m = new Match(a, b, i + 1);
            m.SetResult(scores[i, 0], scores[i, 1]);
            league.Matches.Add(m);
        }

        TeamStats stats = new TeamStats(league, a);
        Assert.Equal(4, stats.Played);
        Assert.Equal(2, stats.Won);
        Assert.Equal(1, stats.Drawn);
        Assert.Equal(1, stats.Lost);
        Assert.Equal(3, stats.LongestUnbeatenStreak);
        Assert.Equal(1, stats.BigWins);
        Assert.Equal(1, stats.BiggestWin!.Round);
    }
}

public class StorageTests
{
    [Fact]
    public void SaveAndLoad_KeepsLeagueAndCup()
    {
        League league = new League("Ліга", true);
        foreach (string n in new[] { "Динамо", "Шахтар", "Ворскла" })
            league.AddTeam(new Team(n, "Місто"));
        league.GenerateSchedule();
        league.Matches[0].SetResult(2, 1);

        Cup cup = new Cup("Кубок", false);
        foreach (string n in new[] { "А", "Б", "В", "Г", "Д" })
            cup.AddTeam(new Team(n));
        cup.GenerateSchedule();
        CupTie played = cup.Rounds[0].First(t => !t.IsBye);
        played.Matches[0].SetResult(0, 0);
        played.PenaltyWinner = played.TeamB;

        string path = Path.Combine(Path.GetTempPath(), "fo_test_" + Guid.NewGuid() + ".json");
        try
        {
            Storage.Save(path, new List<Tournament> { league, cup });
            List<Tournament> loaded = Storage.Load(path);

            Assert.Equal(2, loaded.Count);
            League l2 = Assert.IsType<League>(loaded[0]);
            Assert.Equal("Ліга", l2.Name);
            Assert.True(l2.HasReturnMatches);
            Assert.Equal(league.Matches.Count, l2.Matches.Count);
            Assert.Equal(2, l2.Matches[0].HomeGoals);
            // посилання на ті самі об'єкти команд, а не копії
            Assert.Contains(l2.Matches[0].HomeTeam, l2.Teams);

            Cup c2 = Assert.IsType<Cup>(loaded[1]);
            Assert.Equal(cup.Rounds[0].Count, c2.Rounds[0].Count);
            CupTie tie2 = c2.Rounds[0].First(t => !t.IsBye);
            Assert.Equal(played.TeamB!.Name, tie2.Winner!.Name);
            Assert.Same(tie2.Matches[0], c2.Matches.First(m => m == tie2.Matches[0]));
        }
        finally
        {
            File.Delete(path);
        }
    }
}
