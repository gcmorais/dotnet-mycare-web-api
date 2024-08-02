using Microsoft.AspNetCore.Mvc;
using MyCare.Application.Services.User;
using MyCare.Communication.Responses;
using MyCare.Infrastructure.Entities;

namespace MyCare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userInterface;
        public UserController(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }


        [HttpGet("ListUsers")]
        public async Task<ActionResult<ResponseModel<List<UserModel>>>> ListUsers()
        {
            var users = await _userInterface.ListUsers();
            return Ok(users);
        }
    }
}
