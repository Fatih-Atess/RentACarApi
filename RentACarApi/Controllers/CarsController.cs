using Microsoft.AspNetCore.Mvc;
using RentACarApi.Repositories.Interfaces;
using System.Threading.Tasks;
using RentACarApi.DTOs;

namespace RentACarApi.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarRepository _carRepository;

        public CarsController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars([FromQuery] string? status)
        {
            if (status == "mevcut")
            {
                var availableCars = await _carRepository.GetAvailableCarsAsync();
                return Ok(availableCars); 
            }
            if (string.IsNullOrWhiteSpace(status))
            {
                var allCars = await _carRepository.GetAllCarsAsync();
                return Ok(allCars);
            }
            if(status == "kirada")
            {
                var rentedCars = await _carRepository.GetRentedCarsAsync();
                return Ok(rentedCars);
            }

            return BadRequest("Lütfen geçerli bir durum belirtin (örn: ?status=mevcut).");
        }

        [HttpGet("{plaka}")]
        public async Task<IActionResult> GetCarByPlate(string plaka)
        {
            var car = await _carRepository.GetCarByPlateAsync(plaka);

            if(car == null)
            {
                return NotFound(new { message = $"Sistemde '{plaka}' plakalı araç bulunamadı." });
            }
            return Ok(car);
        }


        [HttpPost]
        public async Task<IActionResult> AddCar(AddCarDTO car)
        {
            bool success = await _carRepository.AddCarAsync(car);

            if (success)
            {
                return Ok(new {message = "Araç başarıyla eklendi"});
            }

            return StatusCode(500, "İşlem sırasında bir hata oluştu.");
        }

    }
}
