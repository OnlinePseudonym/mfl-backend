using System.Collections.Generic;

namespace MFL.Services.League.Models
{
    public class RostersDTO
    {
        public string count { get; set; }
        public IEnumerable<FranchiseDTO> franchise { get; set; } = new List<FranchiseDTO>();
    }
}
