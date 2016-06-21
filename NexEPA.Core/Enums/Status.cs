namespace NexEPA.Core.Enums
{
    using System.ComponentModel;

    /// <summary>
    /// The ENUM Status.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Open items.
        /// </summary>
        [Description("Open")]
        Open = 0,

        /// <summary>
        /// Closed item.
        /// </summary>
        [Description("Closed")]
        Closed = 1,
    }
}
