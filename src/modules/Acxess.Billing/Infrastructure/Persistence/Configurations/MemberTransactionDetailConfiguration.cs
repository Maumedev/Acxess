using Acxess.Billing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acxess.Billing.Infrastructure.Persistence.Configurations;

public class MemberTransactionDetailConfiguration : IEntityTypeConfiguration<MemberTransactionDetail>
{
    public void Configure(EntityTypeBuilder<MemberTransactionDetail> builder)
    {
        builder.ToTable("MemberTransactionDetails");

        builder.HasKey(t => t.IdMemberTransactionDetail);

        builder.Property(t => t.IdMemberTransactionDetail)
            .UseIdentityColumn();

        builder.Property(t => t.IdMemberTransaction)
            .IsRequired();

        builder.Property(t => t.IdSubscription);
        builder.Property(t => t.IdItem);

        builder.Property(t => t.ItemTransactionType)
            .IsRequired()
            .HasColumnType("tinyint");

        builder.Property(t => t.Description)
            .HasMaxLength(500);

        builder.Property(t => t.Amount)
            .IsRequired();

        builder.Property(t => t.Price)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(t => t.Total)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasIndex(t => t.IdMemberTransaction);
    }
}
