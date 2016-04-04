namespace SoftChess.Inc.Data.DataProxies
{
    /// <summary>
    ///     Represents an element that allows database persistence operations.
    /// </summary>
    public interface IDataProxy
    {
        /// <summary>
        ///     Indicatis if dataproxy is initialized.
        /// </summary>
        bool Initialized { get; }

        /// <summary>
        ///     Method to initialize dataproxy... serialize data, prepare context, etc...
        /// </summary>
        void Initialize();
    }
}