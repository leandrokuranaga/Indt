using Proposal.Domain.Proposal.Proposal.ValueObjects;

namespace TestProject1._3._Domain_Layer_Tests.ValueObjects
{
    public class UtcDateTest
    {
        [Fact]
        public void Value_IsAssigned()
        {
            var now = DateTime.UtcNow;
            var d = new UtcDate(now);

            Assert.Equal(now, d.Value);
        }

        [Fact]
        public void ToString_UsesRoundTripFormat()
        {
            var dt = new DateTime(2024, 01, 02, 03, 04, 05, DateTimeKind.Utc);
            var d = new UtcDate(dt);

            Assert.Equal(dt.ToString("O"), d.ToString());
        }

        [Fact]
        public void ImplicitOperator_ReturnsUnderlyingDateTime()
        {
            var dt = DateTime.UtcNow;
            UtcDate value = new(dt);

            DateTime asDateTime = value;

            Assert.Equal(dt, asDateTime);
        }
    }
}
