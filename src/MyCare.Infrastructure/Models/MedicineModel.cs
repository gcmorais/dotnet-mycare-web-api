namespace MyCare.Infrastructure.Entities
{
    public class MedicineModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ActivePrinciple { get; set; } = string.Empty;

        public string Class { get; set; } = string.Empty;

        public string Reference { get; set; } = string.Empty;

        public string Prescription { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string TherapeuticCategory { get; set; } = string.Empty;

        public string Manufacturer { get; set; } = string.Empty;

        public UserModel User { get; set; }
    }
}
