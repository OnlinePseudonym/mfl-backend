using System.Collections.Generic;

namespace MFL.Services.Players.Models
{
    public class PlayersDTO
    {
        public string timestamp { get; set; }
        public IEnumerable<PlayerDTO> player { get; set; }
    }
}
