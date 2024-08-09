using MyCare.Communication.Requests;
using MyCare.Communication.Responses;
using MyCare.Infrastructure.Entities;

namespace MyCare.Application.Services.User
{
    public interface IUserInterface
    {
        Task<ResponseModel<List<UserModel>>> ListUsers();
        Task<ResponseModel<UserModel>> GetUserById(Guid userId);
        Task<ResponseModel<UserModel>> GetUserByMedId(int medId);

        Task<ResponseModel<List<UserModel>>> CreateUser(RequestRegisterUserJson requestRegisterUserJson);
        Task<ResponseModel<List<UserModel>>> EditUser(RequestEditUserJson requestEditUserJson);

        Task<ResponseModel<List<UserModel>>> DeleteUser(Guid userId);

        Task<ResponseModel<UserModel>> Login(RequestUserLogin requestUserLogin);
    }
}
