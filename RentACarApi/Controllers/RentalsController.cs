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
            if(request.Arac_ID == 0)
            {
                return BadRequest("Lütfen geçerli bir araç id'si giriniz.");
            }
            if(request.Toplam_Tutar <= 0)
            {
                return BadRequest("Lütfen geçerli bir tutar girniz.");
            }
            if(request.Musteri_Adi == "")
            {
                return BadRequest("Müşteri adı boş bırakılamaz.");
            }
            if(request.Baslangic_Tarihi >= request.Bitis_Tarihi)
            {
                return BadRequest("Başlangıç tarihi bitiş tarihinden ileride olamaz.");
            }
            if(request.Baslangic_Tarihi == null)
            {
                return BadRequest("Lütfen bir başlangıç tarihi giriniz.");
            }
            if(request.Bitis_Tarihi == null)
            {
                return BadRequest("Lütfen bir bitiş tarihi giriniz.");
            }
            if (await _rentalRepository.IsCarExist(request.Arac_ID) == false)
            {
                return BadRequest("Lütfen geçerli bir araç id'si giriniz.");
            }

            bool isAvailable = await _rentalRepository.IsCarAvailableForDatesAsync(
                request.Arac_ID, request.Baslangic_Tarihi, request.Bitis_Tarihi);

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
