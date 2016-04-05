using System;
using System.Threading.Tasks;
using System.Web.Http;
using SoftChess.Inc.Core.DataContracts;
using SoftChess.Inc.Data.DataProxies;

namespace SoftChess.Inc.Api.Web.Controllers
{
    public class HistoricalController : ApiController
    {
        private readonly IHistoricalPersistence _historicalPersistence;

        public HistoricalController(IHistoricalPersistence historicalPersistence)
        {
            if (historicalPersistence == null) throw new ArgumentNullException(nameof(historicalPersistence));
            _historicalPersistence = historicalPersistence;
        }

        [HttpPost]
        public async Task Post([FromBody] HistoricalMovement movement)
        {
            if (movement == null) throw new ArgumentNullException(nameof(movement));
            await _historicalPersistence.RegisterPieceMovementAsync(movement).ConfigureAwait(false);
        }
    }
}