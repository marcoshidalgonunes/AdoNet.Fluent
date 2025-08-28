namespace AdoNet.Fluent;

/// <summary>
/// Provides data for referential integrity violation error event.
/// </summary>
public sealed class ConstraintErrorEventArgs : EventArgs
{
    private readonly ConstraintError errorType = ConstraintError.None;

    /// <summary>
    /// Class constructor.
    /// </summary>
    /// <param name="errorType">Type of integrity violation.</param>
    /// <param name="message">Integrity error message.</param>
    internal ConstraintErrorEventArgs(ConstraintError errorType, string message)
    {
        this.errorType = errorType;
        Message = message;
    }

    /// <summary>
    /// Type of integrity violation.
    /// </summary>
    public ConstraintError ErrorType
    {
        get { return errorType; }
    }

    /// <summary>
    /// Integrity error message.
    /// </summary>
    public string Message { get; set; }
}
