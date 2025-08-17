using Microsoft.EntityFrameworkCore;

namespace Contract.Infra;

public class Context: DbContext 
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
        Database.Migrate();
    }
    
    public DbSet<ContractAggregate.Domain.Contract> Contracts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
    }
}