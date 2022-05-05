using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "dd1e62af-58e3-4b47-8d15-7a5244d91b11",
                    UserId = "d2d822af-f98c-424c-914b-6ea285f8b847"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "b19ffcf8-0541-4f1d-9b2a-adb238c7adbc",
                    UserId = "e044ad7f-282c-4c76-aa30-379350a39142"
                }
                );
        }
    }
}
