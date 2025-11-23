using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingExercise_SportRadar.Application;

public class CounterManager
{
    private static BetsCounter betsProcessedCounter = new BetsCounter();
    private static BetsCounter totalCounter = new BetsCounter();
    private static BetsMoney totalProfit = new BetsMoney();
    private static BetsMoney totalLoses = new BetsMoney();

    public static BetsCounter BetsProcessedCounter { get => betsProcessedCounter; set => betsProcessedCounter = value; }
    public static BetsCounter TotalCounter { get => totalCounter; set => totalCounter = value; }
    public static BetsMoney TotalProfit { get => totalProfit; set => totalProfit = value; }
    public static BetsMoney TotalLoses { get => totalLoses; set => totalLoses = value; }
}