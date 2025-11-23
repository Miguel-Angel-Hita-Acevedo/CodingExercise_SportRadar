using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CodingExercise_SportRadar.Domain.Entities;
using CodingExercise_SportRadar.Domain.Enums;
using CodingExercise_SportRadar.Domain.Repository;
using CodingExercise_SportRadar.Infrastructure;

namespace CodingExercise_SportRadar.Application;

public class BetService
{
    private readonly IBetRepository _betRepository;

    public BetService(IBetRepository betRepository)
    {
        _betRepository = betRepository;
    }
    
    public async Task SaveBet(Bet bet)
    {
        Bet betFound = await _betRepository.GetByIdAsync(bet.Id);
        await Save(bet, betFound);
        AddBetThreadManager.GetInstance().FinishThread(bet.Id);
    }
    
    public async Task Save(Bet bet, Bet betFound)
    {
        bool validated = StatusValidations(bet, betFound);
        
        if (validated)
        {
            if(betFound == null)
                await _betRepository.AddAsync(bet);
            else
                await _betRepository.UpdateAsync(bet);
            MoneyBalance(bet);
        }
    }
    
    private bool StatusValidations(Bet bet, Bet betFound)
    {
        if(betFound != null)
        {
            if(betFound.Status == bet.Status)
            {
                Console.WriteLine($"Bet{bet.Id} {bet.Status} (Idempotent).");
                return false;
            }
            if(betFound.Status != BetStatus.OPEN && bet.Status != BetStatus.OPEN)
            {
                Console.WriteLine($"Bet{bet.Id} {bet.Status} (Invalid.. to be reviewed).");
                return false;
            }
            Console.WriteLine($"Bet{bet.Id} {bet.Status}");
        }
        else
        {
            if(bet.Status != BetStatus.OPEN)
            {
                Console.WriteLine($"Bet{bet.Id} {bet.Status} (Invalid.. to be reviewed).");
                return false;
            }
            Console.WriteLine($"Bet{bet.Id} {bet.Status}");
        }
        
        CounterManager.BetsProcessedCounter.Add();
        return true;
    }
    
    public void MoneyBalance(Bet bet)
    {
        if(bet.Status == BetStatus.WINNER)
        {
            CounterManager.TotalProfit.Add(bet.Ammount);
            return;
        }
        if(bet.Status == BetStatus.LOSER)
        {
            CounterManager.TotalLoses.Add(bet.Ammount);
            return;
        }
    }
}