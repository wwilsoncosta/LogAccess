using AccessLogs.Domain.Entities;
using System.Collections.Generic;

namespace AccessLogs.Domain.Interfaces
{
    public interface ILogAccessRepository
    {
        void LoginAccess(int usuarioId, string ipMaquina);
        List<LogAccess> GetLogAccess();
        List<LogAccess> GetLogAccessId(int? usuarioId);
        List<LogAccessCount> GetAccessHour(List<LogAccess> logAccesses);
    }
}