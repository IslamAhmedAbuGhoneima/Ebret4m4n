using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Ebret4m4n.Repository.Configuration;


namespace Ebret4m4n.Repository;

public class EbretAmanDbContext : IdentityDbContext<ApplicationUser>
{
    public EbretAmanDbContext(DbContextOptions<EbretAmanDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(RolesConfiguration).Assembly);
        base.OnModelCreating(builder);
    }

}
