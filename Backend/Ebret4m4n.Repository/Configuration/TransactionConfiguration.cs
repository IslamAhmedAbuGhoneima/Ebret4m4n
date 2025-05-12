using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ebret4m4n.Repository.Configuration;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(key => new { key.ParentId, key.ChildId });

        builder.HasOne(prop => prop.Child)
            .WithOne(prop => prop.Transaction)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(builder => builder.Parent)
            .WithMany(builder => builder.Transactions)
            .OnDelete(DeleteBehavior.NoAction);


        builder.HasIndex(idx => idx.SessionId);
    }
}
