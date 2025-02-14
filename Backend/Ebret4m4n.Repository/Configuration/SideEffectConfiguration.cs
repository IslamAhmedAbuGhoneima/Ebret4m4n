using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Ebret4m4n.Repository.Configuration
{
    public class SideEffectConfiguration : IEntityTypeConfiguration<SideEffect>
    {
        public void Configure(EntityTypeBuilder<SideEffect> builder)
        {
            
            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(350);

        }
    }
}
