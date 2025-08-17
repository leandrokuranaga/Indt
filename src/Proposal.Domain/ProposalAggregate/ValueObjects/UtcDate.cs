using Abp.Domain.Values;

namespace Proposal.Domain.Proposal.Proposal.ValueObjects
{
    public class UtcDate : ValueObject
    {
        public DateTime Value { get; }

        public UtcDate(DateTime value) => Value = value;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static implicit operator DateTime(UtcDate d) => d.Value;
        public override string ToString() => Value.ToString("O");
    }
}
