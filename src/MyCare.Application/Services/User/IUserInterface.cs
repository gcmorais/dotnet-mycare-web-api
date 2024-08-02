using MyCare.Communication.Responses;
using MyCare.Infrastructure.Entities;

namespace MyCare.Application.Services.User
{
    public interface IUserInterface
    {
        Task<ResponseModel<List<UserModel>>> ListUsers();
    }
}
