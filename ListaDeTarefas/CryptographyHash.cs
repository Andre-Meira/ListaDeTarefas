using System.Security.Cryptography;
using System.IO;
using System;
using System.Text;

namespace ListaDeTarefas
{
    public class CryptographyHash 
    {        
        public string CriptografarSenha(string senha)
        {
            SHA512 _algorithm = SHA512.Create();

            var encodedValue = Encoding.UTF8.GetBytes(senha);
            var encryptedPassword = _algorithm.ComputeHash(encodedValue);

            var sb = new StringBuilder();
            foreach (var caracter in encryptedPassword)
            {
                sb.Append(caracter.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
