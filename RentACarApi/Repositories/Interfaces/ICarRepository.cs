using RentACarApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentACarApi.DTOs;

namespace RentACarApi.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAvailableCarsAsync();
        Task<IEnumerable<Car>> GetAllCarsAsync();
        Task<IEnumerable<Car>> GetRentedCarsAsync();
        Task<Car?> GetCarByPlateAsync(string plaka);

        Task<bool> AddCarAsync(AddCarDTO car);
    }
}
