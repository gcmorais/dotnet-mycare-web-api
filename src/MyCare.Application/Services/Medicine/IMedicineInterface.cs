using MyCare.Communication.Requests;
using MyCare.Communication.Responses;
using MyCare.Infrastructure.Entities;

namespace MyCare.Application.Services.Medicine
{
    public interface IMedicineInterface
    {
        Task<ResponseModel<List<MedicineModel>>> ListMedicines();

        Task<ResponseModel<MedicineModel>> GetMedicamentById(int id);

        Task<ResponseModel<List<MedicineModel>>> CreateMedicament(RequestRegisterMedicineJson requestRegisterMedicineJson);

        Task<ResponseModel<List<MedicineModel>>> EditMedicament(RequestEditMedicineJson requestEditMedicineJson);
        Task<ResponseModel<List<MedicineModel>>> DeleteMedicament(int id);
    }
}
