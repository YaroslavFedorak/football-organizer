using FootballOrganizer.Models;

namespace FootballOrganizer.Tests;

public class LeagueTests
{
    private static League CreateLeague(int teams, bool returnMatches)
    {
        League league = new League("Тест", returnMatches);
        for (int i = 1; i <= teams; i++)
            league.AddTeam(new Team("Команда " + i));
        return league;
    }

    [Fact]
    public void Schedule_EvenTeams_EveryPairPlaysOnce()
    {
        League league = CreateLeague(6, false);
        league.GenerateSchedule();

        Assert.Equal(5, league.RoundsCount);
        Assert.Equal(15, league.Matches.Count);

        // кожна пара зустрічається рівно один раз
        var pairs = league.Matches
            .Select(m => string.Join("-", new[] { m.HomeTeam.Name, m.AwayTeam.Name }.OrderBy(n => n)))
            .ToList();
        Assert.Equal(pairs.Count, pairs.Distinct().Count());
    }

    [Fact]
    public void Schedule_EveryTeamPlaysOncePerRound()
    {
        League league = CreateLeague(8, false);
        league.GenerateSchedule();

        for (int r = 1; r <= league.RoundsCount; r++)
        {
            var teamsInRound = league.GetRound(r).SelectMany(m => new[] { m.HomeTeam, m.AwayTeam }).ToList();
            Assert.Equal(8, teamsInRound.Count);
            Assert.Equal(8, teamsInRound.Distinct().Count());
        }
    }

    [Fact]
    public void Schedule_OddTeams_OneTeamRestsEachRound()
    {
        League league = CreateLeague(5, false);
        league.GenerateSchedule();

        // 5 команд: 5 турів по 2 матчі, кожна команда грає 4 матчі
        Assert.Equal(5, league.RoundsCount);
        Assert.Equal(10, league.Matches.Count);
        foreach (Team t in league.Teams)
            Assert.Equal(4, league.Matches.Count(m => m.HomeTeam == t || m.AwayTeam == t));
    }

    [Fact]
    public void Schedule_ReturnMatches_SecondCircleSwapsHomeAndAway()
    {
        League league = CreateLeague(4, true);
        league.GenerateSchedule();

        Assert.Equal(6, league.RoundsCount);
        Assert.Equal(12, league.Matches.Count);
        foreach (Match m in league.Matches.Where(x => x.Round <= 3))
            Assert.Contains(league.Matches, x => x.Round > 3 && x.HomeTeam == m.AwayTeam && x.AwayTeam == m.HomeTeam);
    }

    [Fact]
    public void Standings_CountsPointsAndSortsTable()
    {
        League league = CreateLeague(4, false);
        league.GenerateSchedule();

        Match first = league.GetRound(1)[0];
        first.SetResult(3, 0);
        Match second = league.GetRound(1)[1];
        second.SetResult(1, 1);

        List<StandingRow> table = league.GetStandings();
        Assert.Equal(first.HomeTeam, table[0].Team);
        Assert.Equal(3, table[0].Points);
        Assert.Equal(3, table[0].GoalDifference);
        Assert.Equal(1, table[1].Points);
        Assert.Equal(0, table[3].Points);
        Assert.Equal(first.AwayTeam, table[3].Team);
    }

    [Fact]
    public void Standings_UpToRound_IgnoresLaterRounds()
    {
        League league = CreateLeague(4, false);
        league.GenerateSchedule();
        league.GetRound(1)[0].SetResult(1, 0);
        league.GetRound(2)[0].SetResult(2, 0);

        int playedAfterRound1 = league.GetStandings(1).Sum(r => r.Played);
        Assert.Equal(2, playedAfterRound1);
    }

    [Fact]
    public void AddTeam_SameName_Throws()
    {
        League league = CreateLeague(2, false);
        Assert.Throws<ArgumentException>(() => league.AddTeam(new Team("команда 1")));
    }
}
