using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingExercise_SportRadar.Domain.Entities;

namespace CodingExercise_SportRadar.Domain.Repository;

public interface IBetRepository
{
    Task<Bet> AddAsync(Bet bet);
    Task<Bet> GetByIdAsync(int id);
    Task<Bet> UpdateAsync(Bet bet);
}