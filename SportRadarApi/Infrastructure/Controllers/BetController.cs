using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportRadarApi.Application;
using SportRadarApi.Domain.Entities;
using SportRadarApi.Domain.Enums;
using SportRadarApi.Infrastructure;
using SportRadarApi.Infrastructure.Repository;

[ApiController]
[Route("Create")]
public class BetController : ControllerBase
{
    
    [HttpPost("AddBet")]
    public async Task<IActionResult> AddBet([FromBody]BetDto bet){
        AddBetThreadManager.GetInstance().Add(new Bet(bet.Id, bet.Ammount, bet.Status));
        return Ok("OK");
    }
    
    [HttpGet("Bet")]
    public async Task<IActionResult> GetCalendar()
    {
        BetDto betDto = new BetDto();
        betDto.Id = 666;
        betDto.Ammount = 100;
        betDto.Status = "OPEN";
        return Ok(betDto);
    }
}