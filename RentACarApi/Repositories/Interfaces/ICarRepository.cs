using RentACarApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACarApi.Repositories.Interfaces
{
    public interface ICarRepository
    {
        // Task is used because we are making asynchronous database calls
        Task<IEnumerable<Car>> GetAvailableCarsAsync();
    }
}
