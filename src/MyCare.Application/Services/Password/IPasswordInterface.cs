namespace MyCare.Application.Services.Password
{
    public interface IPasswordInterface
    {
        void CreateHashPassword(string password, out byte[] hashPassword, out byte[] saltPassword);

        bool PasswordVerify(string password, byte[] hashPassword, byte[] saltPassword);
    }
}
