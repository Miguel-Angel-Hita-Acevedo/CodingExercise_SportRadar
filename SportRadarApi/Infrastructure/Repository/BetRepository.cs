using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportRadarApi.Domain.Entities;
using SportRadarApi.Domain.Repository;

namespace SportRadarApi.Infrastructure.Repository;

public class BetRepository : IBetRepository
{
    private BetDbCollection _db;
    private int PROCESSING_TIME_MILISECONDS = 50;
    public BetRepository(BetDbCollection db) => _db = db;
    public async Task<Bet> AddAsync(Bet bet)
    {
        await Processing();
        return _db.Add(bet);
    }

    public async Task<Bet> GetByIdAsync(int id)
    {
        await Processing();
        return _db.GetById(id);
    }
    public async Task<Bet> UpdateAsync(Bet bet)
    {
        await Processing();
        return _db.Update(bet);
    }
    
    private async Task Processing()
    {
        Thread.Sleep(PROCESSING_TIME_MILISECONDS);
    }
}