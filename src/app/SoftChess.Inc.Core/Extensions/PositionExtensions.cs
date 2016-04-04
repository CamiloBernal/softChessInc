using SoftChess.Inc.Core.DataContracts;

namespace SoftChess.Inc.Core.Extensions
{
    public static class PositionExtensions
    {
        public static bool IsInLimits(this Position position) => position.X <= Constants.XAxisMaxLimit &&
                                                                 position.X >= Constants.XAxisMinLimit &&
                                                                 position.Y <= Constants.YAxisMaxLimit &&
                                                                 position.Y >= Constants.YAxisMinLimit;
    }
}