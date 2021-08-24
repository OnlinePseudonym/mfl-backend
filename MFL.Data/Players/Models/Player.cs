using MFL.Data.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;

namespace MFL.Data.Models
{
    public class Player : IUpdatable, IIdentifiable
    {
        [Key]
        public int Id { get; set; }
        public int RotoworldId { get; set; }
        public int StatsId { get; set; }
        public int StatsGlobalId { get; set; }
        public Guid SportsdataId { get; set; }
        public int RotowireId { get; set; }
        public int CbsId { get; set; }
        [MaxLength(50)]
        public string NflId { get; set; }
        public int EspnId { get; set; }
        public int FleaflickerId { get; set; }
        public int DraftYear { get; set; }
        public int DraftPick { get; set; }
        public int DraftRound { get; set; }
        [MaxLength(50)]
        public string DraftTeam { get; set; }
        public int Jersey { get; set; }
        public string FullName { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public DateTime Birthdate { get; set; }
        [MaxLength(50)]
        public string Position { get; set; }
        [MaxLength(50)]
        public string Team { get; set; }
        [MaxLength(50)]
        public string Status { get; set; }
        public int? TeamId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
