using RentACarApi.DTOs;
using RentACarApi.Models;
using System;
using System.Threading.Tasks;


namespace RentACarApi.Repositories.Interfaces
{
    public interface IRentalRepository
    {
        Task<bool> IsCarAvailableForDatesAsync(int aracId, DateTime startDate, DateTime endTime);
        Task<bool> RentCarWithTransactionAsync(CreateRentalRequest request);
        Task<bool> IsCarExist(int id);
        Task<IEnumerable<Rental>> GetAllRentalsAsync();

    }
}
