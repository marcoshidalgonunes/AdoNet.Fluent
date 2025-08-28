using Microsoft.Extensions.Configuration;

namespace AdoNet.Fluent;

/// <summary>
/// Base class for bulding database statement execution.
/// </summary>
/// <typeparam name="TDataObject">Type of <see cref="AdoNet.Fluent.IDataObject"/> to execute in database.</typeparam>
/// <param name="configuration"><see cref="Microsoft.Extensions.Configuration.IConfiguration"/> containing connection string of database.</param>
/// <param name="connectionName">Name of element containing connection string.</param>
/// <param name="connectionMode"><see cref="AdoNet.Fluent.ConnectionMode"/> of connection.</param>
public abstract class DataObjectBuilder<TDataObject>(IConfiguration configuration, string connectionName, ConnectionMode connectionMode) : IDataObjectBuilder<TDataObject>
    where TDataObject : IDataObject
{
    /// <summary>
    /// <see cref="AdoNet.Fluent.ConnectionMode"/> of connection.
    /// </summary>
    protected ConnectionMode ConnectionMode { get; set; } = connectionMode;

    /// <summary>
    /// Builds statement to execute in database.
    /// </summary>
    /// <param name="connectionString">Database connection string.</param>
    /// <param name="mode"><see cref="AdoNet.Fluent.ConnectionMode"/> of connection.</param>
    /// <returns>Type of <see cref="AdoNet.Fluent.IDataObject"/> to execute in database.</returns>
    protected abstract TDataObject Build(string? connectionString, ConnectionMode mode);

    /// <summary>
    /// Builds statement to execute in database.
    /// </summary>
    /// <returns>Type of <see cref="AdoNet.Fluent.IDataObject"/> to execute in database.<</returns>
    /// <exception cref="System.ArgumentNullException" />
    public TDataObject Build()
    {
        ArgumentNullException.ThrowIfNull(configuration);
        return Build(configuration.GetConnectionString(connectionName), ConnectionMode);
    }
}
