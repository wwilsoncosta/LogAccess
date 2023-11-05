using AccessLogs.Domain.Interfaces;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AccesLogs.Service.Service
{
    public class ValidaSenhaRepository : IValidaSenha
    {
        public string CalcHashSenha(string senha)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(senha);
            byte[] hash = md5.ComputeHash(inputBytes);

            // Converter byte array para string hexadecimal
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public bool SenhaValida(string senha)
        {
            if (senha.Length < 10)
                return false;

            if (!senha.Any(x => char.IsDigit(x)))
                return false;

            if (!senha.Any(x => char.IsUpper(x)))
                return false;

            if (!senha.Any(x => char.IsLower(x)))
                return false;

            var itemRepetido = 0;

            var ultimoDigito = '\0';

            foreach (var item in senha)
            {
                if (item == ultimoDigito)
                    itemRepetido++;
                else
                    itemRepetido = 0;

                if (itemRepetido == 2)
                    return false;
            }


            return true;
        }
    }
}