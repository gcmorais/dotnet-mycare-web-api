using Azure.Core;
using Microsoft.EntityFrameworkCore;
using MyCare.Application.Services.Medicine;
using MyCare.Communication.Requests;
using MyCare.Communication.Responses;
using MyCare.Exception;
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

    public async Task<ResponseModel<List<MedicineModel>>> CreateMedicament(RequestRegisterMedicineJson requestRegisterMedicineJson)
    {
        ResponseModel<List<MedicineModel>> response = new();

        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(userData => userData.Id == requestRegisterMedicineJson.User.Id);


            Validate(
                requestRegisterMedicineJson.Name,
                requestRegisterMedicineJson.Category,
                requestRegisterMedicineJson.Manufacturer,
                requestRegisterMedicineJson.Reference
            );

            if (user == null)
            {
                response.Mensagem = ResourceErrorMessages.ID_ERROR;
                return response;
            }

            var medicament = new MedicineModel()
            {
                Name = requestRegisterMedicineJson.Name,
                ActivePrinciple = requestRegisterMedicineJson.ActivePrinciple,
                Category = requestRegisterMedicineJson.Category,
                Class = requestRegisterMedicineJson.Class,
                Manufacturer = requestRegisterMedicineJson.Manufacturer,
                Prescription = requestRegisterMedicineJson.Prescription,
                Reference = requestRegisterMedicineJson.Reference,
                TherapeuticCategory = requestRegisterMedicineJson.TherapeuticCategory,
                User = user,
            };

            _context.Add(medicament);
            await _context.SaveChangesAsync();

            response.Dados = await _context.Medicines.Include(a => a.User).ToListAsync();
            response.Mensagem = ResourceSuccessMessages.CREATE_MED_SUCCESS_MESSAGE;

            return response;
        }
        catch (MyCareException ex)
        {
            response.Mensagem = ex.Message;
            response.Status = false;

            return response;
        }
    }

    public async Task<ResponseModel<List<MedicineModel>>> EditMedicament(RequestEditMedicineJson requestEditMedicineJson)
    {
        ResponseModel<List<MedicineModel>> response = new();

        try
        {

            var medicament = await _context.Medicines
                .Include(u => u.User)
                .FirstOrDefaultAsync(medData => medData.Id == requestEditMedicineJson.Id);

            var user = await _context.Users.FirstOrDefaultAsync(userData => userData.Id == requestEditMedicineJson.User.Id);

            if (medicament == null)
            {
                response.Mensagem = ResourceErrorMessages.NO_REGISTRY;
                return response;
            }

            if (user == null)
            {
                response.Mensagem = ResourceErrorMessages.ID_ERROR;
                return response;
            }

            Validate(
                requestEditMedicineJson.Name,
                requestEditMedicineJson.Category,
                requestEditMedicineJson.Manufacturer,
                requestEditMedicineJson.Reference
            );

            medicament.Name = requestEditMedicineJson.Name;
            medicament.ActivePrinciple = requestEditMedicineJson.ActivePrinciple;
            medicament.Class = requestEditMedicineJson.Class;
            medicament.Reference = requestEditMedicineJson.Reference;
            medicament.Prescription = requestEditMedicineJson.Prescription;
            medicament.Category = requestEditMedicineJson.Category;
            medicament.TherapeuticCategory = requestEditMedicineJson.TherapeuticCategory;
            medicament.Manufacturer = requestEditMedicineJson.Manufacturer;

            _context.Update(medicament);
            await _context.SaveChangesAsync();

            response.Dados = await _context.Medicines.ToListAsync();
            response.Mensagem = ResourceSuccessMessages.EDIT_MED_SUCCESS_MESSAGE;

            return response;

        }
        catch (MyCareException ex)
        {
            response.Mensagem = ex.Message;
            response.Status = false;

            return response;
        }
    }

    public async Task<ResponseModel<MedicineModel>> GetMedicamentById(int id)
    {
        ResponseModel<MedicineModel> response = new();

        try
        {
            var medicines = await _context.Medicines.FirstOrDefaultAsync(med => med.Id == id);

            if(medicines == null)
            {
                response.Mensagem = ResourceErrorMessages.NO_REGISTRY;
                return response;
            }

            response.Dados = medicines;
            response.Mensagem = ResourceSuccessMessages.LIST_MED_SUCCESS_MESSAGE;

            return response;
        }
        catch (MyCareException ex)
        {
            response.Mensagem = ex.Message;
            response.Status = false;

            return response;
        }
    }

    public async Task<ResponseModel<List<MedicineModel>>> ListMedicines()
    {
        ResponseModel<List<MedicineModel>> resposta = new();

        try
        {
            var medicament = await _context.Medicines.Include(item => item.User).ToListAsync();

            resposta.Dados = medicament;
            resposta.Mensagem = ResourceSuccessMessages.LIST_MED_SUCCESS_MESSAGE;

            return resposta;
        }
        catch (MyCareException ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;

            return resposta;
        }
    }

    private void Validate(string Name, string Category, string Manufacturer, string Reference)
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            throw new MyCareException(ResourceErrorMessages.NAME_EMPTY);
        }

        if (string.IsNullOrWhiteSpace(Category))
        {
            throw new MyCareException(ResourceErrorMessages.CATEGORY_EMPTY);
        }

        if (string.IsNullOrWhiteSpace(Manufacturer))
        {
            throw new MyCareException(ResourceErrorMessages.MANUFACTURER_EMPTY);
        }

        if (string.IsNullOrWhiteSpace(Reference))
        {
            throw new MyCareException(ResourceErrorMessages.REFERENCE_EMPTY);
        }
    }
}