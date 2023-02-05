using Items.API.Services.UsersServices;
using Microsoft.AspNetCore.Mvc;

namespace Items.API.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public ActionResult Get([FromQuery]string role)
        {
            if (role != "user" && role != "admin")
            {
                return BadRequest("No role recognized");
            }

            var token = _usersService.Authenticate(role);
            return Ok(token);
        }
    }
}
