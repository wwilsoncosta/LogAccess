using System.ComponentModel.DataAnnotations;

namespace AccessLogs.Domain.Entities
{
    public class Usuarios
    {
        public int UsuarioId { get; set; }

        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Admin { get; set; }
        public int IsAdmin { get; set; }

        public Usuarios()
        {
        }

        public Usuarios(int usuarioId, string nome, string login, string senha, string admin, int isAdmin)
        {
            UsuarioId = usuarioId;
            Nome = nome;
            Login = login;
            Senha = senha;
            IsAdmin = isAdmin;
            Admin = admin;
        }

    }
}