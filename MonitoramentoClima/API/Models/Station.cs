using System.ComponentModel.DataAnnotations;

namespace MonitoramentoClima.API.Models
{
    public class Station
    {
        [Key]
        [StringLength(50)]
        public String Id { get; set; }

        [Required]
        [StringLength(100)]
        public String Name { get; set; }

        public String? Location { get; set; }

        public DateTime? LastSeen { get; set; }

        public List<Reading> Readings { get; set; } = new List<Reading>();
    }
}
