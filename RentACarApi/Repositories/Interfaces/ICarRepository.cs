using RentACarApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACarApi.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAvailableCarsAsync();
    }
}
