using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Ebret4m4n.Repository.Configuration;

public class HealthCareCenterConfiguration : IEntityTypeConfiguration<HealthCareCenter>
{
    public void Configure(EntityTypeBuilder<HealthCareCenter> builder)
    {
        builder.Property(p=>p.HealthCareCenterId).IsRequired();

        builder.Property(p => p.HealthCareCenterName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Governorate)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(p => p.City)
            .HasMaxLength(30);

        builder.Property(p => p.Village)
            .HasMaxLength(30);

        builder.Property(p => p.FirstDay)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(p => p.SecondDay)
            .HasConversion<string>()
            .IsRequired();

        builder.HasIndex(p => p.HealthCareCenterName);


        List<HealthCareCenter> healthCareCenters = [
            new ()
            {
                HealthCareCenterName = "الوحده المحليه بقريه ابوجرج",
                Governorate = "المنيا",
                City = "بني مزار",
                Village = "ابوجرج",
                FirstDay = DayOfWeek.Monday,
                SecondDay = DayOfWeek.Tuesday
            },
            new ()
            {
                HealthCareCenterName = "الوحده المحليه بقريه البرجايه",
                Governorate = "المنيا",
                City = "المنيا",
                Village = "البرجايه",
                FirstDay = DayOfWeek.Saturday,
                SecondDay = DayOfWeek.Tuesday
            },
            new ()
            {
                HealthCareCenterName = "الوحده المحليه بقريه دهمرو",
                Governorate = "المنيا",
                City = "مغاغا",
                Village = "دهمرو",
                FirstDay = DayOfWeek.Sunday,
                SecondDay = DayOfWeek.Wednesday
            },
        ];

        builder.HasData(healthCareCenters);

    }
}
