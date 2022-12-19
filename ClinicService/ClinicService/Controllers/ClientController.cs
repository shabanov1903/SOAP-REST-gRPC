using ClinicService.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        #region Serives

        private readonly ClinicServiceDbContext _dbContext;

        #endregion

        #region Constructors
        
        public ClientController(ClinicServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        #endregion

        #region Public Methods

        [HttpGet("get-all")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public IActionResult GetAllClients()
        {
            return Ok(_dbContext.Clients);
        }

        #endregion
    }
}
