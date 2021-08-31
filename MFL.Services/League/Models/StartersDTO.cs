using System.Collections.Generic;

namespace MFL.Services.League.Models
{
    public class StartersDTO
    {
        public string count { get; set; }
        public IEnumerable<PositionLimitDTO> position { get; set; }
        public string idp_starters { get; set; }
    }
}
