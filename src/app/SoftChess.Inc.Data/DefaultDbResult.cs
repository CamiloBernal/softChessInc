namespace SoftChess.Inc.Data
{
    /// <summary>
    ///     Represents a default DbResult class.
    /// </summary>
    public class DefaultDbResult : IDbResult
    {
        /// <summary>
        ///     Default constructor of class.
        /// </summary>
        public DefaultDbResult()
        {
            //Default CTOR
        }

        /// <summary>
        ///     Creates an instance of class indicating if operation is success
        /// </summary>
        /// <param name="isSuccess"></param>
        public DefaultDbResult(bool isSuccess)
        {
            Success = isSuccess;
        }

        /// <summary>
        ///     Returns if operation is sucess.
        /// </summary>
        public bool Success { get; set; }
    }
}