using MyCare.Communication.Requests;
using MyCare.Exception.ExceptionsBase;
using MyCare.Exception;

namespace MyCare.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        public void Execute(RequestRegisterUserJson request)
        {
            Validate(request);
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
