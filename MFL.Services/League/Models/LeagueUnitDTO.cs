using MFL.Services.Players.Models;
using System.Collections.Generic;
using System.Linq;

namespace MFL.Services.League.Models
{
    public class LeagueUnitDTO
    {
        public string unit { get; set; }
        public IEnumerable<PlayerDTO> player { get; set; } = Enumerable.Empty<PlayerDTO>();
    }
}
