namespace AdoNet.Fluent;

/// <summary>
/// Interface for bulding database statement execution.
/// </summary>
/// <typeparam name="TDataObject">Type of <see cref="AdoNet.Fluent.IDataObject"/> to execute in database.</typeparam>
public interface IDataObjectBuilder<TDataObject>
    where TDataObject : IDataObject
{
    /// <summary>
    /// Builds statement to execute in database.
    /// </summary>
    /// <returns>Type of <see cref="AdoNet.Fluent.IDataObject"/> to execute in database.<</returns>
    TDataObject Build();
}
