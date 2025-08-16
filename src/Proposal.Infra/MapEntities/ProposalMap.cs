using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proposal.Infra.MapEntities.Seeds;

namespace Proposal.Infra.MapEntities;

public class ProposalMap: IEntityTypeConfiguration<Domain.Proposal>
{
    public void Configure(EntityTypeBuilder<Domain.Proposal> builder)
    {
        builder.ToTable("Proposals");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.CreationDate)
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