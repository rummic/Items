using Items.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Items.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly ItemsDbContext _dbContext;
        public ColorsController(ItemsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetAll()
        {
            return Ok(_dbContext.Colors.ToList());
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Add([FromBody] string colorName)
        {
            var guid = Guid.NewGuid();
            _dbContext.Colors.Add(new Data.Model.Color() {Guid = guid, Name = colorName });
            _dbContext.SaveChanges();
            return Ok(_dbContext.Colors.Single(x => x.Guid == guid));
        }
    }

}
