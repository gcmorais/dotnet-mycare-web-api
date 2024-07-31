using System.Text.Json.Serialization;

namespace MyCare.Infrastructure.Entities
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<MedicineModel> Medicine { get; set; }

    }
}
