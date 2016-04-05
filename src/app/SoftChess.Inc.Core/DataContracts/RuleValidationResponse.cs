using System.Collections.Generic;

namespace SoftChess.Inc.Core.DataContracts
{
    public class RuleValidationResponse
    {
        public string Message { get; set; }

        public List<Position> AvailableMovements { get; } = new List<Position>();
    }
}