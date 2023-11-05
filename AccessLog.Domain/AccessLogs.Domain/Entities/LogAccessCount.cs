namespace AccessLogs.Domain.Entities
{
    public class LogAccessCount
    {
        public LogAccessCount(int quantidade, string hora)
        {
            this.hora = hora;
            this.quantidade = quantidade;
        }
        public string hora { get; set; }
        public int quantidade { get; set; }

    }
}
