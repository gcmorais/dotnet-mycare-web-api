using MyCare.Communication.Requests;
using MyCare.Exception;
using MyCare.Exception.ExceptionsBase;

namespace MyCare.Application.UseCases.Medicine.Register;
public class RegisterMedicineUseCase
{
    public void Execute(RequestRegisterMedicineJson request)
    {
        Validate(request);
    }

    private void Validate(RequestRegisterMedicineJson request)
    {
        if(string.IsNullOrWhiteSpace(request.Name))
        {
            throw new MyCareException(ResourceErrorMessages.NAME_EMPTY);
        }

        if (string.IsNullOrWhiteSpace(request.Description))
        {
            throw new MyCareException(ResourceErrorMessages.DESCRIPTION_EMPTY);
        }

        if (string.IsNullOrWhiteSpace(request.Status))
        {
            throw new MyCareException(ResourceErrorMessages.STATUS_EMPTY);
        }
    }
}