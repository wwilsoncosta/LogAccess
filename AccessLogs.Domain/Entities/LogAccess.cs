using System;

namespace AccessLogs.Domain.Entities
{
    public class LogAccess
    {
        public int LogAcessoId { get; set; }
        public int UsuarioId { get; set; }
        public string EnderecoIp { get; set; }
        public string Nome { get; set; }
        public string ipAcesso { get; set; }
        public DateTime DataHoraAcesso { get; set; }
    }
}