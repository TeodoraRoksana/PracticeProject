using System.Security.Cryptography;
using System.Text;

namespace Practice.Services.HashService
{
    public class HashService : IHashService
    {
        public string HashPassword(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash
                (
                    Encoding.UTF8.GetBytes(password)
                );

            return Convert.ToBase64String(hash);
        }
    }
}
