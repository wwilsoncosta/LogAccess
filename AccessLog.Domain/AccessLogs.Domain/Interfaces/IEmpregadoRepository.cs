using System;
using System.Collections.Generic;
using System.Text;

namespace AccessLogs.Domain.Interfaces
{
    public interface IEmpregadoRepository
    {
        IEnumerable<Entities.Empregado> SelecionaEmpregados(string dados);
    }
}
