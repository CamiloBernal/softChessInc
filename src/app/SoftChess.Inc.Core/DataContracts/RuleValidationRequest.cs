namespace SoftChess.Inc.Core.DataContracts
{
    public class RuleValidationRequest
    {
        public PieceType PieceType { get; set; }
        public Position CurrentPosition { get; set; }

        public Position NextPosition { get; set; }
    }
}