namespace NexEPA.Core.Enums
{
    using System.ComponentModel;

    /// <summary>
    /// The ENUM Policy.
    /// </summary>
    public enum Policy
    {
        /// <summary>
        /// None policy.
        /// </summary>
        [Description("None")]
        None = 0,

        /// <summary>
        /// Read policy.
        /// </summary>
        [Description("Read")]
        Read = 1,

        /// <summary>
        /// Host policy.
        /// </summary>
        [Description("Host")]
        Host = 2,
    }
}
