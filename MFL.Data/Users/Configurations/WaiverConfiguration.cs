using MFL.Data.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MFL.Data.Users.Configurations
{
    public class WaiverConfiguration : IEntityTypeConfiguration<WaiverClaim>
    {
        public void Configure(EntityTypeBuilder<WaiverClaim> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(w => w.Player);
            builder.Property(w => w.LeagueId).IsRequired();
        }
    }
}
