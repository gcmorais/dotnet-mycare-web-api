using Microsoft.AspNetCore.Mvc;
using MyCare.Application.Services.Medicine;
using MyCare.Application.Services.User;
using MyCare.Communication.Requests;
using MyCare.Communication.Responses;
using MyCare.Infrastructure.Entities;

namespace MyCare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineInterface _medicineInterface;

        public MedicineController(IMedicineInterface medicineInterface)
        {
            _medicineInterface = medicineInterface;
        }

        [HttpGet("ListMedicines")]
        public async Task<ActionResult<ResponseModel<List<MedicineModel>>>> ListMedicines()
        {
            var medicines = await _medicineInterface.ListMedicines();
            return Ok(medicines);
        }

        [HttpPost("CreateMedicine")]
        public async Task<ActionResult<ResponseModel<List<MedicineModel>>>> CreateMedicament(RequestRegisterMedicineJson requestRegisterMedicineJson)
        {
            var medicines = await _medicineInterface.CreateMedicament(requestRegisterMedicineJson);
            return Ok(medicines);
        }

        [HttpPut("EditMedicine")]
        public async Task<ActionResult<ResponseModel<List<MedicineModel>>>> EditMedicament(RequestEditMedicineJson requestEditMedicineJson)
        {
            var medicines = await _medicineInterface.EditMedicament(requestEditMedicineJson);
            return Ok(medicines);
        }

        [HttpGet("GetMedById")]
        public async Task<ActionResult<ResponseModel<MedicineModel>>> GetMedicamentById(int id)
        {
            var medicines = await _medicineInterface.GetMedicamentById(id);
            return Ok(medicines);
        }
    }
}
