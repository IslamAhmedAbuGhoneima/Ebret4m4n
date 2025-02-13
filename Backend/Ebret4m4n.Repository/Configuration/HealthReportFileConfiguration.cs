using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ebret4m4n.Repository.Configuration;

public class HealthReportFileConfiguration : IEntityTypeConfiguration<HealthReportFile>
{
    public void Configure(EntityTypeBuilder<HealthReportFile> builder)
    {
        builder.HasKey(H => H.FilePath);
        
        builder.Property(H => H.UploadedOn)
            .HasDefaultValueSql("GETDATE()");
    }
}
