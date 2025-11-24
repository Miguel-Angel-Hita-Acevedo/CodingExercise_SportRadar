using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportRadarApi.Application;

public class BetsMoney
{
    private double betsMoney = 0;
    
    public void Add(double money)
    {
        betsMoney+=money;
    }
    
    public double Get()
    {
        return betsMoney;
    }
}