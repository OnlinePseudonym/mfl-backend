using MFL.Services.SeedWork;
using System.Collections.Generic;
using System.Linq;

namespace MFL.Services.League.Models
{
    public class DivisionsDTO
    {
        public string count { get; set; }
        public IEnumerable<GenericDTO> division { get; set; } = Enumerable.Empty<GenericDTO>();
    }
}
