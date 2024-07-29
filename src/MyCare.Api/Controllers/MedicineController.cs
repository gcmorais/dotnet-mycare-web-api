using Microsoft.AspNetCore.Mvc;
using MyCare.Application.UseCases.Medicine.Register;
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
                var useCase = new RegisterMedicineUseCase();

                useCase.Execute(request);

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
