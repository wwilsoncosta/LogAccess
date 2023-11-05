using AccessLogs.Domain.Entities;
using System.Collections.Generic;

namespace AccessLogs.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuarios> GetUsers();
        bool LogInto(string ipMaquina, Usuarios usuarios);
        void Save(Usuarios usuarios);
        void Delete(int UsuarioId);
        Usuarios GetUserId(int usuarioId);
    }
}