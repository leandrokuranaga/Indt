using Microsoft.EntityFrameworkCore;

namespace Proposal.Infra;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
        Database.Migrate();
    }
    
    public DbSet<Domain.Proposal.Proposal> Proposals { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
    }
}