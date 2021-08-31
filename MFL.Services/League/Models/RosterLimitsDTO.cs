using System.Collections.Generic;

namespace MFL.Services.League.Models
{
    public class RosterLimitsDTO
    {
        public IEnumerable<PositionLimitDTO> position { get; set; }
    }
}
