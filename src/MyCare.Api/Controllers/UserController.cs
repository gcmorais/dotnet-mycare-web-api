using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCare.Application.UseCases.User.Register;
using MyCare.Communication.Requests;
using MyCare.Exception;
using MyCare.Exception.ExceptionsBase;

namespace MyCare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public IActionResult Register([FromBody] RequestRegisterUserJson request)
        {
            try
            {
                var UseCase = new RegisterUserUseCase();

                UseCase.Execute(request);

                return Created();
            }
            catch (MyCareException ex)
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
