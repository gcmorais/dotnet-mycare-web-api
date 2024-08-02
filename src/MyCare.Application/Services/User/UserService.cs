using MyCare.Communication.Requests;
using MyCare.Exception.ExceptionsBase;
using MyCare.Exception;
using MyCare.Infrastructure;
using MyCare.Application.Services.User;
using MyCare.Communication.Responses;
using MyCare.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace MyCare.Application.UseCases.User
{
    public class UserService : IUserInterface
    {
        private readonly MyCareDbContext _context;

        public UserService(MyCareDbContext context)
        {
            _context = context;
        }
        public void Execute(RequestRegisterUserJson request)
        {
            Validate(request);
        }

        public async Task<ResponseModel<List<UserModel>>> ListUsers()
        {
            ResponseModel<List<UserModel>> resposta = new();
            try
            {
                var users = await _context.Users.ToListAsync();

                resposta.Dados = users;
                resposta.Mensagem = ResourceSuccessMessages.LIST_USERS_SUCCESS_MESSAGE;

                return resposta;
            }
            catch (MyCareException ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;

                return resposta;
            }
        }

        private void Validate(RequestRegisterUserJson request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new MyCareException(ResourceErrorMessages.NAME_EMPTY);
            }
        }
    }
}
