using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using CodingExercise_SportRadar.Domain.Entities;

namespace CodingExercise_SportRadar.Infrastructure.Repository;

public class BetDbCollection
{
    private static BetDbCollection _betDbCollection;
    private ConcurrentDictionary<int, Bet> dbBets = new ConcurrentDictionary<int, Bet>();
    
    public static BetDbCollection GetInstance()
    {
        if(_betDbCollection == null)
            _betDbCollection = new BetDbCollection();
        return _betDbCollection;
    }
    
    public Bet Add(Bet bet)
    {
        bool added = dbBets.TryAdd(bet.Id, bet);
        return added ? bet : null;
    }
    
    public Bet GetById(int id)
    {
        bool found = dbBets.TryGetValue(id, out Bet betFound);
        return found ? betFound : null;
    }
    
    public Bet Update(Bet bet)
    {
        bool added = dbBets.TryAdd(bet.Id, bet);
        dbBets[bet.Id].Status = bet.Status;
        return added ? bet : null;
    }
}