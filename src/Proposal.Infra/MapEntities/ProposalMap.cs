using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proposal.Infra.MapEntities.Seeds;

namespace Proposal.Infra.MapEntities;

public class ProposalMap: IEntityTypeConfiguration<Domain.Proposal.Proposal>
{
    public void Configure(EntityTypeBuilder<Domain.Proposal.Proposal> builder)
    {
        builder.ToTable("Proposals");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.CreationDate)
                .HasConversion(
                       v => v.Value,
                       v => new UtcDate(v))
                .IsRequired();
        
        builder.Property(c => c.ProposalStatus)
            .IsRequired()
            .HasConversion<string>();
        
        builder.Property(c => c.InsuranceType)
            .IsRequired()
            .HasConversion<string>();

        builder.HasData(ProposalSeed.Proposals());

    }
}