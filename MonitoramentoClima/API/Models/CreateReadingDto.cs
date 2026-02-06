namespace MonitoramentoClima.API.Models
{
    public class CreateReadingDto
    {
        public String StationId { get; set; }
        public Decimal Temperature { get; set; }
        public Decimal Humidity { get; set; }
    }
}
