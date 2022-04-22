using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Identity
{
    public class CleanArchitectureIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public CleanArchitectureIdentityDbContext(
            DbContextOptions<CleanArchitectureIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ModelBuilder.ApplyConfiguration(new RoleConfiguration());
            ModelBuilder.ApplyConfiguration(new UserConfiguration());
            ModelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}