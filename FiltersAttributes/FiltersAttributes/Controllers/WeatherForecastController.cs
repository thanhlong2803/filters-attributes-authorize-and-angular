using FiltersAttributes.Entities;
using FiltersAttributes.Services;
using Microsoft.AspNetCore.Mvc;

namespace FiltersAttributes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IUserService  _userService;
        private readonly ILogger<WeatherForecastController> _logger;
        public WeatherForecastController(ILogger<WeatherForecastController> logger , IUserService userService)
        {
            _userService = userService;
            _logger = logger;
        }    
        
        [HttpGet]
        public ActionResult<List<User>> GetAllUser()
        {
            return Ok(_userService.GetAllUser());
        }
    }
}