namespace MyCare.Application.Services.Password
{
    public interface IPasswordInterface
    {
        void CreateHashPassword(string password, out byte[] hashPassword, out byte[] saltPassword);
    }
}
