namespace MyCare.Communication.Requests
{
    public class RequestRegisterMedicineJson
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;

    }
}
