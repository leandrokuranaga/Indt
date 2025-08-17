using Proposal.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace Proposal.Api.SwaggerExamples.Proposals
{
    public class EProposalStatusRequestExample : IExamplesProvider<EProposalStatus>
    {
        public EProposalStatus GetExamples()
        {
            return EProposalStatus.Approved;
        }
    }
}
