using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proposal.Domain.OutboxAggregate;

namespace Proposal.Infra.MapEntities
{
    public class OutboxMessageMap : IEntityTypeConfiguration<Outbox>
    {
        public void Configure(EntityTypeBuilder<Outbox> builder)
        {
            builder.ToTable("Outbox");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Content)
                .IsRequired();

            builder.Property(x => x.OccuredOn)
                .IsRequired();

            builder.Property(x => x.ProcessedOn)
                .IsRequired(false);
        }
    }
}
