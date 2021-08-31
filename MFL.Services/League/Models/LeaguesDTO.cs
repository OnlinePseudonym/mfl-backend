using System.Collections.Generic;
using System.Linq;

namespace MFL.Services.League.Models
{
    public class LeaguesDTO
    {
        public IEnumerable<LeagueInstanceDTO> league { get; set; } = Enumerable.Empty<LeagueInstanceDTO>();
    }
}
