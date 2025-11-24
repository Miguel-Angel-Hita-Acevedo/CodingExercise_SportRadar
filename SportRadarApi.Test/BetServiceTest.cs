using SportRadarApi.Domain.Entities;
using SportRadarApi.Domain.Enums;
using SportRadarApi.Application;
using SportRadarApi.Infrastructure.Repository;

namespace SportRadarApi.Test;

public class BetServiceTest
{
    [Fact]
    public void StatusValidations_BetWithOpenStatusAndFoundNull_ReturnTrue()
    {
        BetService betService = new BetService(new BetRepository(BetDbCollection.GetInstance()));
        Bet bet = new Bet(1, 100, BetStatus.OPEN);

        bool validated = betService.StatusValidations(bet, null);

        Assert.True(validated);
    }

    [Fact]
    public void StatusValidations_BetWithOpenStatusAndFoundBetWithOpenStatus_ReturnFalse()
    {
        BetService betService = new BetService(new BetRepository(BetDbCollection.GetInstance()));
        Bet bet = new Bet(1, 100, BetStatus.OPEN);
        Bet betFound = new Bet(1, 100, BetStatus.OPEN);

        bool validated = betService.StatusValidations(bet, betFound);

        Assert.False(validated);
    }

    [Fact]
    public void StatusValidations_BetWithOpenStatusAndFoundWinnerStatus_ReturnTrue()
    {
        BetService betService = new BetService(new BetRepository(BetDbCollection.GetInstance()));
        Bet bet = new Bet(1, 100, BetStatus.OPEN);
        Bet betFound = new Bet(1, 100, BetStatus.WINNER);

        bool validated = betService.StatusValidations(bet, betFound);

        Assert.True(validated);
    }

    [Fact]
    public void StatusValidations_BetWithOpenStatusAndFoundLoserStatus_ReturnTrue()
    {
        BetService betService = new BetService(new BetRepository(BetDbCollection.GetInstance()));
        Bet bet = new Bet(1, 100, BetStatus.OPEN);
        Bet betFound = new Bet(1, 100, BetStatus.LOSER);

        bool validated = betService.StatusValidations(bet, betFound);

        Assert.True(validated);
    }

    [Fact]
    public void StatusValidations_BetWithWinnerStatusAndFoundWinner_ReturnFalse()
    {
        BetService betService = new BetService(new BetRepository(BetDbCollection.GetInstance()));
        Bet bet = new Bet(1, 100, BetStatus.WINNER);
        Bet betFound = new Bet(1, 100, BetStatus.WINNER);

        bool validated = betService.StatusValidations(bet, betFound);

        Assert.False(validated);
    }

    [Fact]
    public void StatusValidations_BetWithWinnerStatusAndFoundLoser_ReturnFalse()
    {
        BetService betService = new BetService(new BetRepository(BetDbCollection.GetInstance()));
        Bet bet = new Bet(1, 100, BetStatus.WINNER);
        Bet betFound = new Bet(1, 100, BetStatus.LOSER);

        bool validated = betService.StatusValidations(bet, betFound);

        Assert.False(validated);
    }

    [Fact]
    public void StatusValidations_BetWithWinnerStatusAndFoundNull_ReturnFalse()
    {
        BetService betService = new BetService(new BetRepository(BetDbCollection.GetInstance()));
        Bet bet = new Bet(1, 100, BetStatus.WINNER);

        bool validated = betService.StatusValidations(bet, null);

        Assert.False(validated);
    }
}