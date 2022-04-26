using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "d2d822af-f98c-424c-914b-6ea285f8b847",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "admin@localhost.com",
                    Nombre = "David",
                    Apellidos = "Amador",
                    UserName = "davidamador",
                    NormalizedUserName = "davidamador",
                    PasswordHash = hasher.HashPassword(null, "david123"),
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "e044ad7f-282c-4c76-aa30-379350a39142",
                    Email = "juan@localhost.com",
                    NormalizedEmail = "juan@localhost.com",
                    Nombre = "Juan",
                    Apellidos = "Perez",
                    UserName = "juanperez",
                    NormalizedUserName = "juanperez",
                    PasswordHash = hasher.HashPassword(null, "david123"),
                    EmailConfirmed = true
                }
            );
        }
    }
}
