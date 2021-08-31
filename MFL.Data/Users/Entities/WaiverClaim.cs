using MFL.Data.Players.Entities;
using MFL.Data.SeedWork;
using System;

namespace MFL.Data.Users.Entities
{
    public class WaiverClaim : IUpdatable, IEntity
    {
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public string DropIds { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
