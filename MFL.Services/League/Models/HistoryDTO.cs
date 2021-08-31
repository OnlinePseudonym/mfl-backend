using System.Collections.Generic;

namespace MFL.Services.League.Models
{
    public class HistoryDTO
    {
        public IEnumerable<LeagueInstanceDTO> league { get; set; }
    }
}
