using Microsoft.EntityFrameworkCore;
using MyCare.Application.Services.Medicine;
using MyCare.Communication.Responses;
using MyCare.Exception.ExceptionsBase;
using MyCare.Infrastructure;
using MyCare.Infrastructure.Entities;

namespace MyCare.Application.UseCases.Medicine;
public class MedicineService : IMedicineInterface
{
    private readonly MyCareDbContext _context;

    public MedicineService(MyCareDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<List<MedicineModel>>> ListMedicines()
    {
        ResponseModel<List<MedicineModel>> resposta = new();

        try
        {
            var medicament = await _context.Medicines.Include(item => item.User).ToListAsync();

            resposta.Dados = medicament;
            resposta.Mensagem = "Visualizando todos os registros!";

            return resposta;
        }
        catch (MyCareException ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;

            return resposta;
        }
    }
}