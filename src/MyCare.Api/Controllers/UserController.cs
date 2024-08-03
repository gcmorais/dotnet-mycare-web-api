using Microsoft.AspNetCore.Mvc;
using MyCare.Application.Services.User;
using MyCare.Communication.Requests;
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

        [HttpGet("GetById/{userId}")]
        public async Task<ActionResult<ResponseModel<UserModel>>> GetUserById(Guid userId)
        {
            var users = await _userInterface.GetUserById(userId);
            return Ok(users);
        }

        [HttpGet("GetUserByMedId/{medId}")]
        public async Task<ActionResult<ResponseModel<UserModel>>> GetUserByMedId(int medId)
        {
            var users = await _userInterface.GetUserByMedId(medId);
            return Ok(users);
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<ResponseModel<List<UserModel>>>> CreateUser(RequestRegisterUserJson requestRegisterUserJson)
        {
            var users = await _userInterface.CreateUser(requestRegisterUserJson);
            return Ok(users);
        }


        [HttpPut("EditUser")]
        public async Task<ActionResult<ResponseModel<List<UserModel>>>> EditUser(RequestEditUserJson requestEditUserJson)
        {
            var users = await _userInterface.EditUser(requestEditUserJson);
            return Ok(users);
        }
    }
}
