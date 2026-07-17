using Microsoft.AspNetCore.Mvc;
using RentACarApi.DTOs;
using RentACarApi.Repositories.Interfaces;
using System.Threading.Tasks;

namespace RentACarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController: ControllerBase
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalsController(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRental([FromBody] CreateRentalRequest request)
        {
            bool isAvailable = await _rentalRepository.IsCarAvailableForDatesAsync(
                request.ID, request.Baslangic_Tarihi, request.Bitis_Tarihi);

            if (!isAvailable)
            {
                return Conflict(new { message = "Seçilen tarihler arasında bu araç zaten kiralanmış." });
            }

            bool success = await _rentalRepository.RentCarWithTransactionAsync(request);

            if (success)
            {
                return Ok(new { message = "Kiralama işlemi başarıyla tamamlandıve araç durumu güncellendi. " });
            }

            return StatusCode(500, "İşlem sırasında bir hata oluştu.");
        }
    }
}
