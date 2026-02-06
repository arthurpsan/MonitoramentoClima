using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitoramentoClima.API.Models
{
    public class Reading
    {
        [Key]
        public Int64 Id { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(5,2)")]
        public decimal Temperature { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Humidity { get; set; }

        // Chave estrangeira para Station
        [Required]
        [StringLength(50)]
        public String StationId { get; set; }

        // Propriedade de navegação
        [ForeignKey("StationId")]
        public Station Station { get; set; }
    }
}
