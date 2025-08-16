// src/Contract.Infra/MapEntities/ContractMap.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contract.Infra.MapEntities;

public class ContractMap : IEntityTypeConfiguration<Domain.Contract.Contract>
{
    public void Configure(EntityTypeBuilder<Domain.Contract.Contract> builder)
    {
        builder.ToTable("Contracts");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.ContractDate).IsRequired();
        builder.Property(c => c.ProposalId).IsRequired();
        builder.Property(c => c.InsuranceNameHolder).IsRequired();
        builder.Property(c => c.ProposalStatus)
            .IsRequired()
            .HasConversion<string>();

        builder.HasData(Seeds.ContractSeed.RootContracts());

        builder.OwnsOne(c => c.CPF, cpf =>
        {
            cpf.Property(p => p.Value)
                .HasColumnName("CPF")
                .HasMaxLength(11)
                .IsRequired();

            cpf.HasData(Seeds.ContractSeed.CpfOwned());
        });

        builder.OwnsOne(c => c.MonthlyBill, mb =>
        {
            mb.Property(p => p.Value)
                .HasPrecision(18, 2)
                .IsRequired();

            mb.Property(p => p.Currency)
                .HasMaxLength(3)
                .IsRequired();

            mb.HasData(Seeds.ContractSeed.MonthlyBillOwned());
        });
    }
}