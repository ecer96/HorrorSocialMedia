
using Konscious.Security.Cryptography;
using System.Text;

namespace HSStories.Controllers
{
    
    public class HashPasswords 
    {
        public static string HashPassword(string password)
        {
            // Configura los parámetros de Argon2id
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                DegreeOfParallelism = 8,
                MemorySize = 1024 * 1024, // 1 GB
                Iterations = 4
            };

            // Hashea la contraseña y devuelve la cadena hasheada
            var hashBytes = argon2.GetBytes(32);
            var hashedPassword = Convert.ToBase64String(hashBytes);
            return hashedPassword;
        }

        public static bool VerifyPassword(string password,string hashedPassword) 
        {
            // Decodifica la cadena hasheada en un arreglo de bytes
            var hashBytes = Convert.FromBase64String(hashedPassword);

            // Configura los parámetros de Argon2id
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                DegreeOfParallelism = 8,
                MemorySize = 1024 * 1024, // 1 GB
                Iterations = 4
            };

            // Hashea la contraseña proporcionada y compara el resultado con la cadena hasheada
            var hashBytesToCheck = argon2.GetBytes(32);
            return hashBytesToCheck.SequenceEqual(hashBytes);
        }

    }
}
