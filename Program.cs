using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using CodingExercise_SportRadar.Application;
using CodingExercise_SportRadar.Domain.Entities;
using CodingExercise_SportRadar.Domain.Enums;
using CodingExercise_SportRadar.Infrastructure;
using CodingExercise_SportRadar.Infrastructure.Repository;

namespace CodingExercise_SportRadar;
public class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

        builder.Services.AddControllers();
        builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        builder.Services.AddEndpointsApiExplorer();

        var app = builder.Build();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        
        AddBetThreadManager addBetThreadManager = AddBetThreadManager.GetInstance();
        Thread manageQueueThread = new Thread(() => {addBetThreadManager.ManageQueue();});
        manageQueueThread.Start();
        FirstRequests(addBetThreadManager);
        
        app.Run();
        manageQueueThread.Interrupt();
        addBetThreadManager.Shutdown();
        GetSummary();
    }
    
    public static void GetSummary()
    {
        Console.WriteLine($"Bets processed: {CounterManager.BetsProcessedCounter.Get()} / {CounterManager.TotalCounter.Get()}");
        Console.WriteLine($"Total profit:   {CounterManager.TotalProfit.Get()}");
        Console.WriteLine($"Total loss:     {CounterManager.TotalLoses.Get()}");
    }
    
    private static void FirstRequests(AddBetThreadManager addBetThreadManager)
    {
        Bet[] betArray = 
        {
            new Bet(1, 100, BetStatus.OPEN),
            new Bet(2, 100, BetStatus.OPEN),
            new Bet(3, 100, BetStatus.OPEN),
            new Bet(4, 100, BetStatus.OPEN),
            new Bet(5, 100, BetStatus.OPEN),
            new Bet(6, 100, BetStatus.OPEN),
            new Bet(7, 100, BetStatus.OPEN),
            new Bet(8, 100, BetStatus.OPEN),
            new Bet(9, 100, BetStatus.OPEN),
            new Bet(10, 100, BetStatus.OPEN),
            new Bet(11, 100, BetStatus.OPEN),
            new Bet(12, 100, BetStatus.OPEN),
            new Bet(13, 100, BetStatus.OPEN),
            new Bet(14, 100, BetStatus.OPEN),
            new Bet(15, 100, BetStatus.OPEN),
            new Bet(16, 100, BetStatus.OPEN),
            new Bet(17, 100, BetStatus.OPEN),
            new Bet(18, 100, BetStatus.OPEN),
            new Bet(19, 100, BetStatus.OPEN),
            new Bet(20, 100, BetStatus.OPEN),
            new Bet(3, 100, BetStatus.WINNER),
            new Bet(7, 100, BetStatus.LOSER),
            new Bet(1, 100, BetStatus.WINNER),
            new Bet(15, 100, BetStatus.LOSER),
            new Bet(11, 100, BetStatus.WINNER),
            new Bet(4, 100, BetStatus.LOSER),
            new Bet(9, 100, BetStatus.WINNER),
            new Bet(20, 100, BetStatus.LOSER),
            new Bet(6, 100, BetStatus.WINNER),
            new Bet(2, 100, BetStatus.LOSER),
            new Bet(1, 100, BetStatus.LOSER),
            new Bet(5, 100, BetStatus.WINNER),
            new Bet(8, 100, BetStatus.LOSER),
            new Bet(17, 100, BetStatus.WINNER),
            new Bet(16, 100, BetStatus.LOSER),
            new Bet(12, 100, BetStatus.WINNER),
            new Bet(19, 100, BetStatus.LOSER),
            new Bet(14, 100, BetStatus.WINNER),
            new Bet(13, 100, BetStatus.LOSER),
            new Bet(10, 100, BetStatus.WINNER),
            new Bet(4, 100, BetStatus.WINNER),
            new Bet(3, 100, BetStatus.LOSER),
            new Bet(9, 100, BetStatus.LOSER),
            new Bet(18, 100, BetStatus.WINNER),
            new Bet(11, 100, BetStatus.LOSER),
            new Bet(7, 100, BetStatus.WINNER),
            new Bet(6, 100, BetStatus.LOSER),
            new Bet(2, 100, BetStatus.WINNER),
            new Bet(20, 100, BetStatus.WINNER),
            new Bet(5, 100, BetStatus.LOSER),
            new Bet(10, 100, BetStatus.LOSER),
            new Bet(14, 100, BetStatus.LOSER),
            new Bet(15, 100, BetStatus.WINNER),
            new Bet(12, 100, BetStatus.LOSER),
            new Bet(19, 100, BetStatus.WINNER),
            new Bet(8, 100, BetStatus.WINNER),
            new Bet(16, 100, BetStatus.WINNER),
            new Bet(13, 100, BetStatus.WINNER),
            new Bet(18, 100, BetStatus.LOSER),
            new Bet(17, 100, BetStatus.LOSER),
            new Bet(3, 100, BetStatus.WINNER),
            new Bet(7, 100, BetStatus.WINNER),
            new Bet(4, 100, BetStatus.LOSER),
            new Bet(9, 100, BetStatus.WINNER),
            new Bet(2, 100, BetStatus.LOSER),
            new Bet(6, 100, BetStatus.WINNER),
            new Bet(20, 100, BetStatus.LOSER),
            new Bet(14, 100, BetStatus.WINNER),
            new Bet(15, 100, BetStatus.LOSER),
            new Bet(11, 100, BetStatus.WINNER),
            new Bet(1, 100, BetStatus.WINNER),
            new Bet(5, 100, BetStatus.WINNER),
            new Bet(8, 100, BetStatus.LOSER),
            new Bet(12, 100, BetStatus.WINNER),
            new Bet(19, 100, BetStatus.LOSER),
            new Bet(16, 100, BetStatus.LOSER),
            new Bet(13, 100, BetStatus.WINNER),
            new Bet(10, 100, BetStatus.LOSER),
            new Bet(18, 100, BetStatus.WINNER),
            new Bet(17, 100, BetStatus.WINNER),
            new Bet(3, 100, BetStatus.LOSER),
            new Bet(7, 100, BetStatus.LOSER),
            new Bet(20, 100, BetStatus.WINNER),
            new Bet(14, 100, BetStatus.LOSER),
            new Bet(4, 100, BetStatus.WINNER),
            new Bet(1, 100, BetStatus.LOSER),
            new Bet(11, 100, BetStatus.WINNER),
            new Bet(9, 100, BetStatus.LOSER),
            new Bet(15, 100, BetStatus.WINNER),
            new Bet(6, 100, BetStatus.LOSER)
        };
        foreach(Bet bet in betArray)
        {
            addBetThreadManager.Add(bet);
        }
    }
}
