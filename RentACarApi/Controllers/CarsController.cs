using Microsoft.AspNetCore.Mvc;
using RentACarApi.Repositories.Interfaces;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetCars([FromQuery] string status)
        {
            if (status == "available")
            {
                var availableCars = await _carRepository.GetAvailableCarsAsync();
                return Ok(availableCars); 
            }

            return BadRequest("Lütfen geçerli bir durum belirtin (örn: ?status=available).");
        }
    }
}
