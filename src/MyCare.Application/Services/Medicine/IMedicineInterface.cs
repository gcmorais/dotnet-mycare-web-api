using MyCare.Communication.Responses;
using MyCare.Infrastructure.Entities;

namespace MyCare.Application.Services.Medicine
{
    public interface IMedicineInterface
    {
        Task<ResponseModel<List<MedicineModel>>> ListMedicines();
    }
}
