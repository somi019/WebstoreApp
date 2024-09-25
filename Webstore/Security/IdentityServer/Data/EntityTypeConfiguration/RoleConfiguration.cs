using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Data.EntityTypeConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = "Buyer",
                    NormalizedName = "BUYER"
                },
                new IdentityRole
                {   
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }
            );


        }
    }
}
