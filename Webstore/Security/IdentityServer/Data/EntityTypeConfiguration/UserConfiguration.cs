using IdentityServer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Data.EntityTypeConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // svaki put kada dohvatim nekog usera, on ce dohvatiti i ovu navigaciju
            // to je ovde lista refresh tokena
            builder.Navigation(u => u.RefreshTokens).AutoInclude();

        }
    }
}
