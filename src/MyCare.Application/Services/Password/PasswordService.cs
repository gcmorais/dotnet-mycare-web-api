using System.Security.Cryptography;

namespace MyCare.Application.Services.Password
{
    public class PasswordService : IPasswordInterface
    {
        public void CreateHashPassword(string password, out byte[] hashPassword, out byte[] saltPassword)
        {
            using (var HMAC = new HMACSHA512())
            {
                saltPassword = HMAC.Key;
                hashPassword = HMAC.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool PasswordVerify(string password, byte[] hashPassword, byte[] saltPassword)
        {
            using (var HMAC = new HMACSHA512(saltPassword))
            {
                var computedHash = HMAC.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(hashPassword);
            }
        }
    }
}
