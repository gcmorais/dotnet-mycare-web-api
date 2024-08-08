using Azure;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using MyCare.Application.Services.Password;
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
        private readonly IPasswordInterface _passwordInterface;

        public UserService(MyCareDbContext context, IPasswordInterface passwordInterface)
        {
            _context = context;
            _passwordInterface = passwordInterface;
        }

        public async Task<ResponseModel<List<UserModel>>> CreateUser(RequestRegisterUserJson requestRegisterUserJson)
        {
            ResponseModel<List<UserModel>> resposta = new();

            try
            {

                Validate(
                    null,
                    requestRegisterUserJson.Name, 
                    requestRegisterUserJson.Email, 
                    requestRegisterUserJson.Password, 
                    requestRegisterUserJson.ConfirmPassword
                );

                _passwordInterface.CreateHashPassword(requestRegisterUserJson.Password, out byte[] hashPassword, out byte[] saltPassword);

                var user = new UserModel()
                {
                    Name = requestRegisterUserJson.Name,
                    Email = requestRegisterUserJson.Email,
                    HashPasswrod = hashPassword,
                    SaltPassword = saltPassword
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
            ResponseModel<List<UserModel>> response = new();

            try
            {
                _passwordInterface.CreateHashPassword(requestEditUserJson.Password, out byte[] hashPassword, out byte[] saltPassword);
                var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == requestEditUserJson.Id);

                if(user == null)
                {
                    response.Mensagem = ResourceErrorMessages.NO_REGISTRY;

                    return response;
                }

                Validate(
                    requestEditUserJson.Id,
                    requestEditUserJson.Name,
                    requestEditUserJson.Email,
                    requestEditUserJson.Password,
                    requestEditUserJson.Password
                );

                user.Name = requestEditUserJson.Name;
                user.Email = requestEditUserJson.Email;
                user.SaltPassword = saltPassword;
                user.HashPasswrod = hashPassword;

                _context.Update(user);
                await _context.SaveChangesAsync();

                response.Dados = await _context.Users.ToListAsync();
                response.Mensagem = ResourceSuccessMessages.EDIT_USER_SUCCESS_MESSAGE;

                return response;
            }
            catch (MyCareException ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;

                return response;
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

        private void Validate(Guid? Id, string Name, string Email, string Password, string ConfirmPassword)
        {

            var email = _context.Users.FirstOrDefault(x => x.Email == Email && x.Id != Id);

            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new MyCareException(ResourceErrorMessages.NAME_EMPTY);
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                throw new MyCareException(ResourceErrorMessages.EMAIL_EMPTY);
            }

            if (email != null)
            {
                throw new MyCareException(ResourceErrorMessages.EMAIL_ALREADY_REGISTERED);
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                throw new MyCareException(ResourceErrorMessages.PASSWORD_EMPTY);
            }

            if(ConfirmPassword != Password)
            {
                throw new MyCareException(ResourceErrorMessages.PASSWORD_CONFIRM_ERROR);
            }
        }
    }
}
