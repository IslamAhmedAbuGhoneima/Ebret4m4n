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
            .HasMaxLength(150);

        builder.Property(p => p.Governorate)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.City)
            .HasMaxLength(150);

        builder.Property(p => p.Village)
            .HasMaxLength(150);

        builder.Property(p => p.FirstDay)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(p => p.SecondDay)
            .HasConversion<string>()
            .IsRequired();

        builder.HasIndex(p => p.HealthCareCenterName);


        //List<HealthCareCenter> healthCareCenters = [
        //    new ()
        //    {
        //        HealthCareCenterName = "الوحده المحليه بقريه ابوجرج",
        //        Governorate = "المنيا",
        //        City = "بني مزار",
        //        Village = "ابوجرج",
        //        FirstDay = DayOfWeek.Monday,
        //        SecondDay = DayOfWeek.Tuesday
        //    },
        //    new ()
        //    {
        //        HealthCareCenterName = "الوحده المحليه بقريه صفط ابوجرج",
        //        Governorate = "المنيا",
        //        City = "بني مزار",
        //        Village = "صفط ابوجرج",
        //        FirstDay = DayOfWeek.Sunday,
        //        SecondDay = DayOfWeek.Wednesday
        //    },
        //    new ()
        //    {
        //        HealthCareCenterName = "الوحده المحليه بقريه البرجايه",
        //        Governorate = "المنيا",
        //        City = "المنيا",
        //        Village = "البرجايه",
        //        FirstDay = DayOfWeek.Saturday,
        //        SecondDay = DayOfWeek.Tuesday
        //    },
        //    new ()
        //    {
        //        HealthCareCenterName = "الوحده المحليه بقريه دهمرو",
        //        Governorate = "المنيا",
        //        City = "مغاغا",
        //        Village = "دهمرو",
        //        FirstDay = DayOfWeek.Sunday,
        //        SecondDay = DayOfWeek.Wednesday
        //    },
        //    new ()
        //    {
        //        HealthCareCenterName = "الوحده المحليه بالعدوي",
        //        Governorate = "المنيا",
        //        City = "العدوي",
        //        Village = "العدوي",
        //        FirstDay = DayOfWeek.Sunday,
        //        SecondDay = DayOfWeek.Wednesday
        //    },
        //    new ()
        //    {
        //        HealthCareCenterName = "الوحده المحليه بالجيزه",
        //        Governorate = "الجيزه",
        //        City = "الجيزه",
        //        Village = "منيل الروضه",
        //        FirstDay = DayOfWeek.Sunday,
        //        SecondDay = DayOfWeek.Wednesday
        //    },
        //    new ()
        //    {
        //        HealthCareCenterName = "الوحده المحليه بالقاهره",
        //        Governorate = "القاهره",
        //        City = "عين شمس",
        //        Village = "عين شمس",
        //        FirstDay = DayOfWeek.Sunday,
        //        SecondDay = DayOfWeek.Tuesday
        //    }
        //];

        //builder.HasData(healthCareCenters);

    }
}
