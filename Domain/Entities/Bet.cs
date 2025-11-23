using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingExercise_SportRadar.Domain.Enums;

namespace CodingExercise_SportRadar.Domain.Entities;

public class Bet
{
    public Bet(int id, double ammount, BetStatus status)
    {
        Id = id;
        Ammount = ammount;
        Status = status;
    }
    
    public Bet(int id, double ammount, string status)
    {
        Id = id;
        Ammount = ammount;
        Enum.TryParse(status, out BetStatus convertedStatus);
        Status = convertedStatus;
    }

    public int Id { get; set; }
    public double Ammount { get; set; }
    public double Odds { get; set; }
    public string Client { get; set; }
    public string Event { get; set; }
    public string Market { get; set; }
    public string Selection { get; set; }
    public BetStatus Status { get; set; }
}