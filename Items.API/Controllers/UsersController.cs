using Items.API.Services.UsersServices;
using Microsoft.AspNetCore.Mvc;

namespace Items.API.Controllers
{
    /// <summary>
    /// This is just a basic jwt implementation
    /// so I can authorize requests by roles in other controllers.
    /// There are no user accounts, you just request a token with 
    /// desired role "user" or "admin". Proper user accounts and roles management
    /// would be in some other microservice, not here.
    /// </summary>
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
        public ActionResult Get([FromQuery] string role)
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
