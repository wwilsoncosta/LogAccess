using System.Collections.Generic;

namespace AccessLogs.Domain.Entities
{
    public class Empregados
    {
        public string status { get; set; }
        public List<DadosEmpregado> data { get; set; }
        public string message { get; set; }
    }
}
