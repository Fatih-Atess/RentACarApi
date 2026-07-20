using RentACarApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentACarApi.DTOs;

namespace RentACarApi.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAvailableCarsAsync();
        Task<bool> AddCarAsync(AddCarDTO car);
    }
}
