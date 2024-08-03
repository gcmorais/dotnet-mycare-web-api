using Microsoft.EntityFrameworkCore;
using MyCare.Application.Services.User;
using MyCare.Communication.Requests;
using MyCare.Communication.Responses;
using MyCare.Exception;
using MyCare.Exception.ExceptionsBase;
using MyCare.Infrastructure;
using MyCare.Infrastructure.Entities;

namespace MyCare.Application.UseCases.User
{
    public class UserService : IUserInterface
    {
        private readonly MyCareDbContext _context;

        public UserService(MyCareDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<UserModel>>> CreateUser(RequestRegisterUserJson requestRegisterUserJson)
        {
            ResponseModel<List<UserModel>> resposta = new();

            try
            {

                Validate(requestRegisterUserJson);

                var user = new UserModel()
                {
                    Name = requestRegisterUserJson.Name,
                    Email = requestRegisterUserJson.Email,
                    Password = requestRegisterUserJson.Password,
                };

                _context.Add(user);

                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Users.ToListAsync();
                resposta.Mensagem = ResourceSuccessMessages.CREATE_USER_MESSAGE_SUCCESS;

                return resposta;
            }
            catch (MyCareException ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;

                return resposta;
            }
        }

        public async Task<ResponseModel<List<UserModel>>> EditUser(RequestEditUserJson requestEditUserJson)
        {
            ResponseModel<List<UserModel>> resposta = new();

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == requestEditUserJson.Id);

                if(user == null)
                {
                    resposta.Mensagem = ResourceErrorMessages.NO_REGISTRY;

                    return resposta;
                }

                user.Name = requestEditUserJson.Name;
                user.Email = requestEditUserJson.Email;
                user.Password = requestEditUserJson.Password;

                _context.Update(user);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Users.ToListAsync();
                resposta.Mensagem = ResourceSuccessMessages.EDIT_USER_SUCCESS_MESSAGE;

                return resposta;
            }
            catch (MyCareException ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;

                return resposta;
            }
        }

        public async Task<ResponseModel<UserModel>> GetUserById(Guid userId)
        {
            ResponseModel<UserModel> resposta = new();
            try
            {
                var users = await _context.Users.FirstOrDefaultAsync(user => user.Id == userId);

                if(users == null)
                {
                    resposta.Mensagem = ResourceErrorMessages.NO_REGISTRY;
                    return resposta;
                }

                resposta.Dados = users;
                resposta.Mensagem = ResourceSuccessMessages.GET_USER_SUCCESS_MESSAGE;

                return resposta;
            }
            catch (MyCareException ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;

                return resposta;
            }

        }

        public async Task<ResponseModel<UserModel>> GetUserByMedId(int medId)
        {
            ResponseModel<UserModel> resposta = new();
            try
            {
                var medicament = await _context.Medicines
                    .Include(item => item.User)
                    .FirstOrDefaultAsync(itemBanco => itemBanco.Id == medId);

                if (medicament == null)
                {
                    resposta.Mensagem = ResourceErrorMessages.NO_REGISTRY;
                    return resposta;
                }

                resposta.Dados = medicament.User;
                resposta.Mensagem = ResourceSuccessMessages.GET_USER_SUCCESS_MESSAGE;

                return resposta;
            }
            catch (MyCareException ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;

                return resposta;
            }

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

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new MyCareException(ResourceErrorMessages.EMAIL_EMPTY);
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                throw new MyCareException(ResourceErrorMessages.PASSWORD_EMPTY);
            }
        }
    }
}
