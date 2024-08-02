using Microsoft.AspNetCore.Mvc;
using MyCare.Communication.Requests;
using MyCare.Exception;
using MyCare.Exception.ExceptionsBase;

namespace MyCare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        [HttpPost]
        public IActionResult Register([FromBody] RequestRegisterMedicineJson request)
        {
            try
            {
                

            

                return Created();
            }
            catch(MyCareException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResourceErrorMessages.UNKNOWN_ERROR);
            }
        }
    }
}
