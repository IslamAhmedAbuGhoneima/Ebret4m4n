using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebret4m4n.Repository.Configuration
{
    internal class SideEffectConfiguration : IEntityTypeConfiguration<SideEffect>
    {
        public void Configure(EntityTypeBuilder<SideEffect> builder)
        {
            
            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(350);

        }
    }
}
