using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoramentoClima.API.Data;
using MonitoramentoClima.API.Models;

namespace MonitoramentoClima.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Station>>> GetStations()
        {
            // Busca as estações E INCLUI as leituras (Join)
            var stations = await _context.Stations
                .Include(s => s.Readings)
                .ToListAsync();

            return stations;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Station>> GetStation(string id)
        {
            var station = await _context.Stations
                .Include(s => s.Readings)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (station == null)
            {
                return NotFound(); // Retorna erro 404 se não achar
            }

            return station;
        }

        public async Task<ActionResult<Reading>> PostReading([FromBody] CreateReadingDto dto)
        {
            // 1. Verifica se a estação existe
            var station = await _context.Stations.FindAsync(dto.StationId);

            if (station == null)
            {
                // Se a estação não existe, cria ela automaticamente (Auto-Discovery)
                // Isso facilita muito para o Arduino!
                station = new Station
                {
                    Id = dto.StationId,
                    Name = $"Estação {dto.StationId}", // Nome provisório
                    LastSeen = DateTime.Now
                };
                _context.Stations.Add(station);
            }
            else
            {
                // Se já existe, só atualiza o "visto por último"
                station.LastSeen = DateTime.Now;
            }

            // 2. Cria a nova leitura
            var reading = new Reading
            {
                StationId = dto.StationId,
                Temperature = dto.Temperature,
                Humidity = dto.Humidity,
                Timestamp = DateTime.Now // O servidor define a hora, não o Arduino (mais seguro)
            };

            // 3. Salva no Banco
            _context.Readings.Add(reading);
            await _context.SaveChangesAsync();

            // Retorna sucesso (201 Created)
            return CreatedAtAction(nameof(GetStation), new { id = reading.StationId }, reading);
        }

    }
}
