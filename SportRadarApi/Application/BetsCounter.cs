using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportRadarApi.Application;

public class BetsCounter
{
    private int betsCounter = 0;
    
    public async Task Add()
    {
        betsCounter++;
    }
    
    public int Get()
    {
        return betsCounter;
    }
}