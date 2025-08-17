using Proposal.Domain.OutboxAggregate;

namespace TestProject1._3._Domain_Layer_Tests.Entities
{

    public class OutboxDomainTest
    {
        [Fact]
        public void OutboxDomainSuccess()
        {
            #region Arrange

            var mockOutbox = new Outbox
            {
                Type = "object",
                Content = "Select * from Outbox",
                OccuredOn = DateTime.UtcNow,
                ProcessedOn = DateTime.UtcNow
            };

            #endregion

            #region Act

            var mockOutboxDomainAct = new Outbox(
                mockOutbox.Type,
                mockOutbox.Content,
                mockOutbox.OccuredOn,
                mockOutbox.ProcessedOn
            );

            #endregion

            #region Assert

            Assert.Equal(mockOutbox.Type, mockOutboxDomainAct.Type);
            Assert.Equal(mockOutbox.Content, mockOutboxDomainAct.Content);
            Assert.Equal(mockOutbox.OccuredOn, mockOutboxDomainAct.OccuredOn);
            Assert.Equal(mockOutbox.ProcessedOn, mockOutboxDomainAct.ProcessedOn);
            #endregion
        }
    }
}
