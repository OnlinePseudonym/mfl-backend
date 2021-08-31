using System.Collections.Generic;
using System.Linq;

namespace MFL.Services.League.Models
{
    public class League
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Url { get; set; }
        public string BaseUrl { get; set; }
        public string MyFranchiseName { get; set; }
        public IEnumerable<Franchise> Franchises { get; set; } = Enumerable.Empty<Franchise>();
        public IEnumerable<GenericModel> Divisions { get; set; } = Enumerable.Empty<GenericModel>();
    }
}
