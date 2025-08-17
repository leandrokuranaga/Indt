using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proposal.Domain.Proposal.Proposal.ValueObjects;
using Proposal.Infra.MapEntities.Seeds;

namespace Proposal.Infra.MapEntities;

public class ProposalMap: IEntityTypeConfiguration<Domain.ProposalAggregate.Proposal>
{
    public void Configure(EntityTypeBuilder<Domain.ProposalAggregate.Proposal> builder)
    {
        builder.ToTable("Proposals");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.CreationDate)
                .HasConversion(
                       v => v.Value,
                       v => new UtcDate(v))
                .IsRequired();

        builder.OwnsOne(c => c.CPF, cpf =>
        {
            cpf.Property(p => p.Value)
                .HasColumnName("CPF")
                .HasMaxLength(11)
                .IsRequired();

            cpf.HasData(Seeds.ProposalSeed.CpfOwned());
        });

        builder.Property(c => c.InsuranceNameHolder).IsRequired();


        builder.Property(c => c.ProposalStatus)
            .IsRequired()
            .HasConversion<string>();
        
        builder.Property(c => c.InsuranceType)
            .IsRequired()
            .HasConversion<string>();

        builder.HasData(ProposalSeed.Proposals());

    }
}