using Abp.Domain.Entities;

namespace Proposal.Domain.OutboxAggregate
{
    public class Outbox : Entity<Guid>
    {
        public string Type { get; set; }
        public string Content { get; set; }
        public DateTime OccuredOn { get; set; }
        public DateTime? ProcessedOn { get; set; }

        public Outbox()
        {
            
        }

        public Outbox(string type, string content)
        {
            Type = type;
            Content = content;
            OccuredOn = DateTime.UtcNow;
            ProcessedOn = null;
        }
    }
}
