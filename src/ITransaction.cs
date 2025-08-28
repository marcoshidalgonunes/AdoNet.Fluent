namespace AdoNet.Fluent;

/// <summary>
/// Inteface for transactions execution in relational databases.
/// </summary>
public interface ITransaction : IDataObject
{
    /// <summary>
    /// Performs transaction commit.
    /// </summary>
    void Commit();

    /// <summary>
    /// Performs transaction rollback.
    /// </summary>
    void Rollback();
}
