namespace SoftChess.Inc.Data
{
    /// <summary>
    ///     Represents a result of Db operations.
    /// </summary>
    public interface IDbResult
    {
        /// <summary>
        ///     Returns if operation is sucess.
        /// </summary>
        bool Success { get; set; }
    }
}