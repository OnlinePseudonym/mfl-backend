using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MFL.Services.Players.Models
{
    public class Player
    {
        public int Id { get; set; }
        public IEnumerable<PlayerNews> News { get; set; }
        public int DraftYear { get; set; }
        public int DraftPick { get; set; }
        public int DraftRound { get; set; }
        public string DraftTeam { get; set; }
        public int Jersey { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public DateTime Birthdate { get; set; }
        public string Position { get; set; }
        public string Team { get; set; }
        public string Status { get; set; }
        public int? TeamId { get; set; }
    }
}
