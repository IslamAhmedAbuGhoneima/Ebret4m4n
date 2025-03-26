using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Ebret4m4n.Repository.Configuration;


namespace Ebret4m4n.Repository;

public class EbretAmanDbContext : IdentityDbContext<ApplicationUser>
{
    public EbretAmanDbContext(DbContextOptions<EbretAmanDbContext> options)
        : base(options) { }

    public DbSet<Child> Children { get; set; }

    public DbSet<HealthCareCenter> HealthCareCenters { get; set; }

    public DbSet<Appointment> Appointments { get; set; }

    public DbSet<Notification> Notifications { get; set; }

    public DbSet<SideEffect> SideEffects { get; set; }

    public DbSet<Vaccine> Vaccines { get; set; }

    public DbSet<Complaint> Complaints { get; set; }

    public DbSet<Chat> Chats { get; set; }

    public DbSet<Certificate> Certificates { get; set; }

    public DbSet<HealthReportFile> HealthReportFiles { get; set; }

    public DbSet<Inventory> Inventories { get; set; }

    public DbSet<GovernorateAdminStaff> GovernorateAdminStaff { get; set; }

    public DbSet<CityAdminStaff> CityAdminStaff { get; set; }   

    public DbSet<MainInventory> MainInventory { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(RolesConfiguration).Assembly);
        base.OnModelCreating(builder);
    }

}
