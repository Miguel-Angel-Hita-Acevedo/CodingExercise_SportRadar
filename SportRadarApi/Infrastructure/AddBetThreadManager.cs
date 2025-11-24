using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SportRadarApi.Application;
using SportRadarApi.Domain.Entities;
using SportRadarApi.Infrastructure.Repository;

namespace SportRadarApi.Infrastructure;

public class AddBetThreadManager
{
    private static AddBetThreadManager singleton;
    private static bool shutdown = false;
    private ConcurrentQueue<Bet> betQueue;// 1st queue to order all the requests
    private ConcurrentDictionary<int, ConcurrentQueue<Bet>> betQueueDictionary;// bets ordered by bet.Id pending to  be thrown as threads
    private ConcurrentDictionary<int, Thread> threadDictionary;
    public static AddBetThreadManager GetInstance()
    {
        if(singleton == null)
        {
            singleton = new AddBetThreadManager();
            singleton.betQueueDictionary = new ConcurrentDictionary<int, ConcurrentQueue<Bet>>();
            singleton.betQueue = new ConcurrentQueue<Bet>();
            singleton.threadDictionary = new ConcurrentDictionary<int, Thread>();
        }
        return singleton;
    }
    
    public void ManageQueue()
    {
        BetDbCollection.GetInstance();
        ConcurrentQueue<Bet> queueFound;
        
        while(!shutdown)
        {
            if(betQueue.Count > 0)
            {
                // takes next Bet
                betQueue.TryDequeue(out Bet bet);
                // clasifies the bet on bet dictionary by id as a key and enqueue
                queueFound = betQueueDictionary.GetOrAdd(bet.Id, new ConcurrentQueue<Bet>());
                
                betQueueDictionary[bet.Id].Enqueue(bet);
                // when queue dont have more elements than the current enqueued, then throw this bet as thread
                if(queueFound.Count <= 1)
                    ThrowNextThread(bet);
            }
            else
            {
                Thread.Sleep(5);
            }
        }
    }
    
    public void Add(Bet bet)
    {
        CounterManager.TotalCounter.Add();
        singleton.betQueue.Enqueue(bet);
    }
    
    public void FinishThread(int key)
    {
        betQueueDictionary[key].TryDequeue(out Bet b);
        bool peekNext = betQueueDictionary[key].TryPeek(out Bet betPeek);
        
        if(peekNext)
        {
            ThrowNextThread(betPeek);
        }
    }
    
    public void Shutdown()
    {
        shutdown = true;
        Thread[] threadArray = threadDictionary.Values.ToArray();
        foreach(Thread thread in threadArray)
        {
            thread.Interrupt();
        }
    }
    
    private void ThrowNextThread(Bet bet)
    {
        Thread currentThread = new Thread(() =>
        {
            new BetService(new BetRepository(BetDbCollection.GetInstance())).SaveBet(bet);
        });
        currentThread.Start();
        threadDictionary.GetOrAdd(bet.Id, currentThread);
    }
}