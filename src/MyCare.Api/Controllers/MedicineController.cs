using Microsoft.AspNetCore.Mvc;
using MyCare.Application.Services.Medicine;
using MyCare.Application.Services.User;
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
    }
}
