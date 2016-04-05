using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SoftChess.Inc.Core.DataContracts;
using SoftChess.Inc.Data.DataProxies;

namespace SoftChess.Inc.Api.Web.Controllers
{
    public class RulesController : ApiController
    {
        private readonly IRulePersistence _ruleDbContext;

        public RulesController(IRulePersistence ruleDbContext)
        {
            if (ruleDbContext == null) throw new ArgumentNullException(nameof(ruleDbContext));
            _ruleDbContext = ruleDbContext;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Get([FromBody] RuleValidationRequest validationRequest)
        {
            var pieceRuleSet =
                await _ruleDbContext.GetPieceRulesetAsync(validationRequest.PieceType).ConfigureAwait(false);
            var validationResult = pieceRuleSet.MovementIsValid(validationRequest.CurrentPosition,
                validationRequest.NextPosition);
            if (validationResult.Item1)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new RuleValidationResponse
                {
                    Message = "Ok"
                });
            }
            var forbidenResponse = new RuleValidationResponse
            {
                Message = validationResult.Item2
            };
            forbidenResponse.AvailableMovements.AddRange(
                pieceRuleSet.GetAvailableMovements(validationRequest.CurrentPosition));

            return Request.CreateResponse(HttpStatusCode.Forbidden, forbidenResponse);
        }
    }
}