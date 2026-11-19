using FootballOrganizer.Models;

namespace FootballOrganizer.Tests;

public class CupTests
{
    private static Cup CreateCup(int teams, bool returnMatches)
    {
        Cup cup = new Cup("Кубок", returnMatches);
        for (int i = 1; i <= teams; i++)
            cup.AddTeam(new Team("Команда " + i));
        return cup;
    }

    // перша команда пари виграє всі свої матчі 1:0
    private static void TeamAWinsAll(List<CupTie> round)
    {
        foreach (CupTie tie in round)
            foreach (Match m in tie.Matches)
                m.SetResult(m.HomeTeam == tie.TeamA ? 1 : 0, m.HomeTeam == tie.TeamA ? 0 : 1);
    }

    [Fact]
    public void Draw_SixTeams_TwoByesInDifferentHalves()
    {
        Cup cup = CreateCup(6, false);
        cup.GenerateSchedule();

        Assert.Equal(8, cup.BracketSize);
        Assert.Equal(3, cup.TotalRounds);
        List<CupTie> first = cup.Rounds[0];
        Assert.Equal(4, first.Count);
        Assert.Equal(2, first.Count(t => t.IsBye));
        // пари 0-1 і 2-3 дають півфінали, вільні проходи не мають бути в одній
        Assert.False(first[0].IsBye && first[1].IsBye);
        Assert.False(first[2].IsBye && first[3].IsBye);
        // кожна команда в першому раунді рівно один раз
        var teams = first.SelectMany(t => t.IsBye ? new[] { t.TeamA } : new[] { t.TeamA, t.TeamB! }).ToList();
        Assert.Equal(6, teams.Distinct().Count());
    }

    [Fact]
    public void Advance_NotAllTiesPlayed_Throws()
    {
        Cup cup = CreateCup(4, false);
        cup.GenerateSchedule();
        Assert.False(cup.CanAdvance);
        Assert.Throws<InvalidOperationException>(() => cup.AdvanceRound());
    }

    [Fact]
    public void Draw_InTwoMatches_NeedsPenaltyWinner()
    {
        Cup cup = CreateCup(2, true);
        cup.GenerateSchedule();
        CupTie tie = cup.Rounds[0][0];

        // фінал - один матч навіть якщо матчі у відповідь увімкнені
        Assert.Single(tie.Matches);
        tie.Matches[0].SetResult(2, 2);
        Assert.True(tie.NeedsPenalties);
        Assert.Null(tie.Winner);

        tie.PenaltyWinner = tie.TeamB;
        Assert.Equal(tie.TeamB, cup.Champion);
    }

    [Fact]
    public void ReturnMatches_WinnerByAggregate()
    {
        Cup cup = CreateCup(4, true);
        cup.GenerateSchedule();
        CupTie tie = cup.Rounds[0][0];
        Assert.Equal(2, tie.Matches.Count);

        // 0:1 і 3:1 на користь команди А -> сума 3:2
        tie.Matches[0].SetResult(0, 1);
        tie.Matches[1].SetResult(1, 3);
        Assert.Equal(3, tie.GoalsA);
        Assert.Equal(2, tie.GoalsB);
        Assert.Equal(tie.TeamA, tie.Winner);
    }

    [Fact]
    public void FullCup_FromDrawToChampion()
    {
        Cup cup = CreateCup(8, false);
        cup.GenerateSchedule();

        TeamAWinsAll(cup.Rounds[0]);
        cup.AdvanceRound();
        Assert.Equal("1/2 фіналу", cup.RoundName(2));
        TeamAWinsAll(cup.Rounds[1]);
        cup.AdvanceRound();
        Assert.Equal("Фінал", cup.RoundName(3));
        TeamAWinsAll(cup.Rounds[2]);

        Assert.False(cup.CanAdvance);
        Assert.Equal(cup.Rounds[2][0].TeamA, cup.Champion);
    }

    [Fact]
    public void ClearSchedule_RemovesRoundsAndMatches()
    {
        Cup cup = CreateCup(4, false);
        cup.GenerateSchedule();
        cup.ClearSchedule();
        Assert.Empty(cup.Rounds);
        Assert.Empty(cup.Matches);
    }
}
