using Acxess.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acxess.Identity.Infrastructure.Persistence.Configurations;

public class TenantsUsersConfiguration: IEntityTypeConfiguration<TenantsUsers>
{
    public void Configure(EntityTypeBuilder<TenantsUsers> builder)
    {
        builder.HasKey(tu => new { tu.IdTenant, tu.UserNumber });

        builder.HasOne(tu => tu.Tenant)
            .WithMany(t => t.TenantsUsers)
            .HasForeignKey(tu => tu.IdTenant)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(tu => tu.User)
            .WithMany(u => u.TenantsUsers)
            .HasForeignKey(tu => tu.UserNumber)
            .HasPrincipalKey(u => u.UserNumber)
            .OnDelete(DeleteBehavior.Cascade);
    }
}