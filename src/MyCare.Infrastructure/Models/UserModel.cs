using System.Text.Json.Serialization;

namespace MyCare.Infrastructure.Entities
{
    public class UserModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public byte[] HashPasswrod { get; set; }

        public byte[] SaltPassword { get; set; }

        [JsonIgnore]
        public ICollection<MedicineModel> Medicine { get; set; } = [];

    }
}
