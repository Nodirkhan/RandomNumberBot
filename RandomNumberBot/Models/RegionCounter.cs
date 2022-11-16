using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomNumberBot.Entity
{
    public class RegionCounter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int PositionNumber { get; set; }
    }
    public class DefaultRegionCounter : IEntityTypeConfiguration<RegionCounter>
    {
        public void Configure(EntityTypeBuilder<RegionCounter> builder)
        {
            builder.HasData(
                new RegionCounter
                {
                    Id = 1,
                    PositionNumber = 1,
                }
                );
        }
    }
}
