using System.Data;
using System.Data.Common;
using System.Xml;

namespace AdoNet.Fluent;

/// <summary>
/// Base class for operations on relational databases.
/// </summary>
public abstract class DataObject<TConnection, TCommand, TParameter, TException> : IDataObject
    where TConnection : DbConnection, new()
    where TCommand : DbCommand, new()
    where TParameter : DbParameter
    where TException : DbException
{
    private const string _parameterReturn = "RETURN_VALUE";
    private const int _fixedSize = 10;

    private readonly ConnectionMode _connectionMode;

    private bool _disposedValue;

    ///<summary>
    /// Class constructor.
    ///</summary>
    /// <param name="connectionString">Database connection string.</param>
    /// <param name="mode">Connection mode (normal, with transactios or using MARS).</param>
    protected DataObject(string connectionString, ConnectionMode mode)
    {
        _connectionMode = mode;
        Connection = new()
        {
            ConnectionString = connectionString
        };
    }

    #region Protected/Private Members

    /// <summary>
    /// Creates input/output parameter of boolean type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <param name="direction">Parameter <seealso cref="System.Data.ParameterDirection">direction</seealso>.</param>
    protected abstract void AddInOutParameter(string parameterName, bool value, ParameterDirection direction);

    /// <summary>
    /// Creates input/output parameter of decimal type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="scale">Parameter scale.</param>
    /// <param name="precision">Parameter precision.</param>
    /// <param name="direction">Parameter <seealso cref="System.Data.ParameterDirection">direction</seealso>.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    protected abstract void AddInOutParameter(string parameterName, byte precision, byte scale, ParameterDirection direction);

    /// <summary>
    /// Creates input/output parameter of byte type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <param name="direction">Parameter <seealso cref="System.Data.ParameterDirection">direction</seealso>.</param>
    protected abstract void AddInOutParameter(string parameterName, byte? value, ParameterDirection direction);

    /// <summary>
    /// Creates input/output parameter of <see cref="System.DateTime"/> type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <param name="direction">Parameter <seealso cref="System.Data.ParameterDirection">direction</seealso>.</param>
    protected abstract void AddInOutParameter(string parameterName, DateTime? value, ParameterDirection direction);

    /// <summary>
    /// Creates input/output parameter of decimal type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <param name="scale">Parameter scale.</param>
    /// <param name="precision">Parameter precision.</param>
    /// <param name="direction">Parameter <seealso cref="System.Data.ParameterDirection">direction</seealso>.</param>
    protected abstract void AddInOutParameter(string parameterName, decimal? value, byte precision, byte scale, ParameterDirection direction);

    /// <summary>
    /// Creates input/output parameter of double type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <param name="direction">Parameter <seealso cref="System.Data.ParameterDirection">direction</seealso>.</param>
    protected abstract void AddInOutParameter(string parameterName, double? value, ParameterDirection direction);

    /// <summary>
    /// Creates input/output parameter of short type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <param name="direction">Parameter <seealso cref="System.Data.ParameterDirection">direction</seealso>.</param>
    protected abstract void AddInOutParameter(string parameterName, short? value, ParameterDirection direction);

    /// <summary>
    /// Creates input/output parameter of int type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <param name="direction">Parameter <seealso cref="System.Data.ParameterDirection">direction</seealso>.</param>
    protected abstract void AddInOutParameter(string parameterName, int? value, ParameterDirection direction);

    /// <summary>
    /// Creates input/output parameter of long type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <param name="direction">Parameter <seealso cref="System.Data.ParameterDirection">direction</seealso>.</param>
    protected abstract void AddInOutParameter(string parameterName, long? value, ParameterDirection direction);

    /// <summary>
    /// Creates input/output parameter of <see cref="AdoNet.Fluent.NumericType"> type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="type">Parameter type.</param>
    /// <param name="direction">Parameter <seealso cref="System.Data.ParameterDirection">direction</seealso>.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    /// <exception cref="System.ArgumentNullException" />
    protected void AddInOutParameter(string parameterName, NumericType type, ParameterDirection direction)
    {
        CheckParameter(parameterName);
        Command.Parameters.Add(CreateParameter(parameterName, type, direction));
    }

    /// <summary>
    /// Creates input/output parameter of single type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <param name="direction">Parameter <seealso cref="System.Data.ParameterDirection">direction</seealso>.</param>
    protected abstract void AddInOutParameter(string parameterName, float? value, ParameterDirection direction);

    /// <summary>
    /// Creates input/output parameter of string type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="size">Maximum size of string.</param>
    /// <param name="variable">Flag for variable size.</param>
    /// <param name="direction">Parameter <seealso cref="System.Data.ParameterDirection">direction</seealso>.</param>
    /// <exception cref="System.ArgumentNullException" />
    /// <exception cref="System.ArgumentOutOfRangeException" />
    protected void AddInOutParameter(string parameterName, int size, bool variable, ParameterDirection direction)
    {
        CheckParameter(parameterName);
        Command.Parameters.Add(CreateParameter(parameterName, direction, size, variable));
    }

    /// <summary>
    /// Creates input/output parameter of string type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <param name="size">Maximum size of string.</param>
    /// <param name="variable">Flag for variable size.</param>
    /// <param name="direction">Parameter <seealso cref="System.Data.ParameterDirection">direction</seealso>.</param>
    protected abstract void AddInOutParameter(string parameterName, string? value, int size, bool variable, ParameterDirection direction);

    /// <summary>
    /// Command used to execute instructions in database.
    /// </summary>
    protected TCommand Command { get; private set; } = new TCommand();

    /// <summary>
    /// Checks if command is valid.
    /// </summary>
    /// <exception cref="System.InvalidOperationException" />
    protected void CheckCommand()
    {
        if (string.IsNullOrEmpty(Command.CommandText))
        {
            throw new InvalidOperationException(ResourcesFacade.GetString("NoCommand"));
        }

        if (Command.CommandType == CommandType.Text && Command.Parameters.Count == 0)
        {
            throw new InvalidOperationException(ResourcesFacade.GetString("NoParameters"));
        }
    }

    /// <summary>
    /// Checks if parameter is valid.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <exception cref="System.ArgumentNullException" />
    protected void CheckParameter(string parameterName)
    {
        if (string.IsNullOrEmpty(parameterName))
        {
            throw new ArgumentNullException(nameof(parameterName));
        }
    }

    /// <summary>
    /// Checks if parameter is valid.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="scale">Parameter scale.</param>
    /// <param name="precision">Parameter precision.</param>
    /// <exception cref="System.ArgumentNullException" />
    /// <exception cref="System.ArgumentOutOfRangeException" />
    protected void CheckParameter(string parameterName, byte precision, byte scale)
    {
        CheckParameter(parameterName);
        if (precision < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(precision), ResourcesFacade.GetString("PrecisionOutOfRange"));
        }
        if (scale < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(scale), ResourcesFacade.GetString("ScaleOutOfRange"));
        }
    }

    /// <summary>
    /// Checks if parameter is valid.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="size">Maximum size of string.</param>
    /// <exception cref="System.ArgumentNullException" />
    /// <exception cref="System.ArgumentOutOfRangeException" />
    protected void CheckParameter(string parameterName, int size)
    {
        CheckParameter(parameterName);
        if (size <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(size), ResourcesFacade.GetString("SizeOutOfRange"));
        }
    }

    /// <summary>
    /// Closes database connection.
    /// </summary>
    protected void CloseConnection()
    {
        if (Mode == ConnectionMode.Normal)
        {
            Connection.Close();
        }
    }

    /// <summary>
    /// Asynchronously closes database connection.
    /// </summary>
    protected async Task CloseConnectionAsync()
    {
        if (Mode == ConnectionMode.Normal)
        {
            await Connection.CloseAsync();
        }
    }

    protected TConnection Connection { get; set; }

    /// <summary>
    /// Cria parâmetro numérico.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="type"><see cref="AdoNet.Fluent.NumericType"/> parameter.</param>
    /// /// <param name="direction">Parameter <seealso cref="System.Data.ParameterDirection">direction</seealso>.</param>
    /// <returns>Parameter instance numérico</returns>
    protected abstract TParameter CreateParameter(string parameterName, NumericType type, ParameterDirection direction);

    /// <summary>
    /// Creates parameter of type decimal.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// /// <param name="direction">Parameter <seealso cref="System.Data.ParameterDirection">direction</seealso>.</param>
    /// <param name="scale">Parameter scale.</param>
    /// <param name="precision">Parameter precision.</param>
    /// <returns>Parameter instance of type decimal.</returns>
    protected abstract TParameter CreateParameter(string parameterName, ParameterDirection direction, byte precision, byte scale);

    /// <summary>
    /// Creates parameter of type string.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="direction">Parameter direction ("input", "output" ou "inputoutput").</param>
    /// <param name="size">Maximum size of string.</param>
    /// <param name="variable">Flag for variable size.</param>
    /// <returns>Parameter instance of type string.</returns>
    protected abstract TParameter CreateParameter(string parameterName, ParameterDirection direction, int size, bool variable);

    /// <summary>
    /// Error code for trying to insert existing key.
    /// </summary>
    protected int DuplicateKeyError { get; set; }

    private object? ExecuteScalar()
    {
        CheckCommand();

        object? returnValue = null;

        try
        {
            OpenConnection();
            returnValue = Command.ExecuteScalar();
        }
        catch (TException ex)
        {
            HandleException(ex);
        }
        finally
        {
            CloseConnection();
        }

        return returnValue;
    }

    private async Task<object?> ExecuteScalarAsync(CancellationToken token)
    {
        CheckCommand();

        object? returnValue = null;

        try
        {
            await OpenConnectionAsync(token);
            returnValue = await Command.ExecuteScalarAsync(token);
        }
        catch (TException ex)
        {
            HandleException(ex);
        }
        finally
        {
            await CloseConnectionAsync();
        }

        return returnValue;
    }

    /// <summary>
    /// Disposes resources.
    /// </summary>
    /// <param name="disposing">Flag for dispose in execution.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing && !(Connection == null || Connection.State == ConnectionState.Closed))
            {
                Connection.Close();
            }

            Command?.Dispose();
            Connection?.Dispose();

            _disposedValue = true;
        }
    }

    /// <summary>
    /// Error code for foreign key violation.
    /// </summary>
    protected int ForeignKeyError { get; set; }

    /// <summary>
    /// Returns parameter from <see cref="System.Data.IDbCommand.Parameters"/> collection.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Parameter instance.</returns>
    protected abstract TParameter GetParameter(string parameterName);

    private static string? GetScalarString(object value)
    {
        return value?.ToString();
    }

    private void HandleException(ConstraintErrorEventArgs e, TException ex)
    {
        OnConstraintError?.Invoke(this, e);

        throw new ConstraintException(e.Message, ex);
    }

    /// <summary>
    /// Handles exception thrown in database command execution.
    /// </summary>
    /// <param name="ex">Exception thrown in database command execution.</param>
    protected abstract void HandleException(TException ex);

    /// <summary>
    /// Handles exception thrown in database command execution.
    /// </summary>
    /// <param name="errorCode">Error code returned by database.</param>
    /// <param name="ex">Exception thrown in database command execution.</param>
    /// <exception cref="System.Data.ConstraintException" />
    protected void HandleException(int errorCode, TException ex)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        if (errorCode == DuplicateKeyError)
        {
            HandleException(new ConstraintErrorEventArgs(ConstraintError.DuplicateKey, ResourcesFacade.GetString("DuplicateKeyError")), ex);
        }
        else if (errorCode == ForeignKeyError)
        {
            HandleException(new ConstraintErrorEventArgs(ConstraintError.ForeignKey, ResourcesFacade.GetString("ForeignKeyError")), ex);
        }
        else if (errorCode == PrimaryKeyError)
        {
            HandleException(new ConstraintErrorEventArgs(ConstraintError.PrimaryKey, ResourcesFacade.GetString("PrimaryKeyError")), ex);
        }
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Opens connection with database.
    /// </summary>
    /// <exception cref="System.Data.Common.DbException" />
    protected virtual void OpenConnection()
    {
        Connection ??= new TConnection();

        if (Connection.State != ConnectionState.Open) Connection.Open();

        Command.Connection = Connection;
    }

    /// <summary>
    /// Asynchronously opens connection with database.
    /// </summary>
    protected virtual async Task OpenConnectionAsync(CancellationToken cancellationToken)
    {
        Connection ??= new TConnection();

        if (Connection.State != ConnectionState.Open) await Connection.OpenAsync(cancellationToken);

        Command.Connection = Connection;
    }

    /// <summary>
    /// Error code for primary key violation.
    /// </summary>
    protected int PrimaryKeyError { get; set; }

    /// <summary>
    /// Set command to be executed in database.
    /// </summary>
    /// <param name="cmdText">Instruction to execute in database.</param>
    /// <param name="type"><see cref="System.Data.CommandType"/>.</param>
    /// <exception cref="System.NotSupportedException" />
    /// <remarks>If the command is of type text and has no parameters, an exception will be thrown when trying to execute it.</remarks>
    protected void SetCommand(string cmdText, CommandType type)
    {
        if (string.IsNullOrEmpty(cmdText))
        {
            throw new ArgumentNullException(nameof(cmdText));
        }

        if (type == CommandType.TableDirect)
        {
            throw new NotSupportedException(ResourcesFacade.GetString("NoTableDirect"));
        }

        Command.CommandText = cmdText;
        Command.CommandType = type;
        Command.Parameters.Clear();
    }

    #endregion

    #region IDatabase Members

    /// <summary>
    /// Creates input/output parameter of boolean type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <exception cref="System.ArgumentNullException" />
    public IDataObject AddInOutParameter(string parameterName, bool value)
    {
        AddInOutParameter(parameterName, value, ParameterDirection.InputOutput);
        return this;
    }

    /// <summary>
    /// Creates input/output parameter of decimal type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="scale">Parameter scale.</param>
    /// <param name="precision">Parameter precision.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    /// <exception cref="System.ArgumentNullException" />
    public IDataObject AddInOutParameter(string parameterName, byte precision, byte scale)
    {
        AddInOutParameter(parameterName, precision, scale, ParameterDirection.InputOutput);
        return this;
    }

    /// <summary>
    /// Creates input/output parameter of byte type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <exception cref="System.ArgumentNullException" />
    public IDataObject AddInOutParameter(string parameterName, byte? value)
    {
        AddInOutParameter(parameterName, value, ParameterDirection.InputOutput);
        return this;
    }

    /// <summary>
    /// Creates input/output parameter of <see cref="System.DateTime"/> type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <exception cref="System.ArgumentNullException" />
    public IDataObject AddInOutParameter(string parameterName, DateTime? value)
    {
        AddInOutParameter(parameterName, value, ParameterDirection.InputOutput);
        return this;
    }

    /// <summary>
    /// Creates input/output parameter of decimal type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <param name="scale">Parameter scale.</param>
    /// <param name="precision">Parameter precision.</param>
    /// <exception cref="System.ArgumentNullException" />
    public IDataObject AddInOutParameter(string parameterName, decimal? value, byte precision, byte scale)
    {
        AddInOutParameter(parameterName, value, precision, scale, ParameterDirection.InputOutput);
        return this;
    }

    /// <summary>
    /// Creates input/output parameter of double type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <exception cref="System.ArgumentNullException" />
    public IDataObject AddInOutParameter(string parameterName, double? value)
    {
        AddInOutParameter(parameterName, value, ParameterDirection.InputOutput);
        return this;
    }

    /// <summary>
    /// Creates input/output parameter of short type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <exception cref="System.ArgumentNullException" />
    public IDataObject AddInOutParameter(string parameterName, short? value)
    {
        AddInOutParameter(parameterName, value, ParameterDirection.InputOutput);
        return this;
    }

    /// <summary>
    /// Creates input/output parameter of int type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <exception cref="System.ArgumentNullException" />
    public IDataObject AddInOutParameter(string parameterName, int? value)
    {
        AddInOutParameter(parameterName, value, ParameterDirection.InputOutput);
        return this;
    }

    /// <summary>
    /// Creates input/output parameter of string type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="size">Maximum size of string.</param>
    /// <param name="variable">Flag for variable size.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    /// <exception cref="System.ArgumentNullException" />
    public IDataObject AddInOutParameter(string parameterName, int size, bool variable)
    {
        AddInOutParameter(parameterName, size, variable, ParameterDirection.InputOutput);
        return this;
    }

    /// <summary>
    /// Creates input/output parameter of long type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <exception cref="System.ArgumentNullException" />
    public IDataObject AddInOutParameter(string parameterName, long? value)
    {
        AddInOutParameter(parameterName, value, ParameterDirection.InputOutput);
        return this;
    }

    /// <summary>
    /// Creates input/output parameter of <see cref="AdoNet.Fluent.NumericType"> type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="type">Parameter type.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    public IDataObject AddInOutParameter(string parameterName, NumericType type)
    {
        AddInOutParameter(parameterName, type, ParameterDirection.InputOutput);
        return this;
    }

    /// <summary>
    /// Creates input/output parameter of single type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <exception cref="System.ArgumentNullException" />
    public IDataObject AddInOutParameter(string parameterName, float? value)
    {
        AddInOutParameter(parameterName, value, ParameterDirection.InputOutput);
        return this;
    }

    /// <summary>
    /// Creates input/output parameter of string type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <param name="size">Maximum size of string.</param>
    /// <exception cref="System.ArgumentNullException" />
    /// <exception cref="System.ArgumentOutOfRangeException" />
    public IDataObject AddInOutParameter(string parameterName, string? value, int size)
    {
        AddInOutParameter(parameterName, value, size, (size > _fixedSize));
        return this;
    }

    /// <summary>
    /// Creates input/output parameter of string type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <param name="size">Maximum size of string.</param>
    /// <param name="variable">Flag for variable size.</param>
    public abstract IDataObject AddInOutParameter(string parameterName, string? value, int size, bool variable);

    /// <summary>
    /// Creates XML input parameter for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <remarks>For use in creating XML type parameter.</remarks>
    /// <exception cref="System.NotSupportedException" />
    public virtual IDataObject AddInParameter(string parameterName)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Creates input parameter of boolean type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <exception cref="System.ArgumentNullException" />
    public IDataObject AddInParameter(string parameterName, bool value)
    {
        AddInOutParameter(parameterName, value, ParameterDirection.Input);
        return this;
    }

    /// <summary>
    /// Creates input parameter of decimal type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="scale">Parameter scale.</param>
    /// <param name="precision">Parameter precision.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    /// <exception cref="System.ArgumentNullException" />
    /// <exception cref="System.ArgumentOutOfRangeException" />
    public IDataObject AddInParameter(string parameterName, byte precision, byte scale)
    {
        CheckParameter(parameterName, precision, scale);
        AddInParameter(parameterName, null, precision, scale);
        return this;
    }

    /// <summary>
    /// Creates input parameter of byte type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <exception cref="System.ArgumentNullException" />
    public IDataObject AddInParameter(string parameterName, byte? value)
    {
        AddInOutParameter(parameterName, value, ParameterDirection.Input);
        return this;
    }

    /// <summary>
    /// Creates input parameter of binary data for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    public abstract IDataObject AddInParameter(string parameterName, byte[] value);

    /// <summary>
    /// Creates input parameter of <see cref="System.DateTime"/> for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <exception cref="System.ArgumentNullException" />
    public IDataObject AddInParameter(string parameterName, DateTime? value)
    {
        AddInOutParameter(parameterName, value, ParameterDirection.Input);
        return this;
    }

    /// <summary>
    /// Creates input parameter of decimal type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <param name="scale">Parameter scale.</param>
    /// <param name="precision">Parameter precision.</param>
    /// <exception cref="System.ArgumentNullException" />
    public IDataObject AddInParameter(string parameterName, decimal? value, byte precision, byte scale)
    {
        AddInOutParameter(parameterName, value, precision, scale, ParameterDirection.Input);
        return this;
    }

    /// <summary>
    /// Creates input parameter of double type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <exception cref="System.ArgumentNullException" />
    public IDataObject AddInParameter(string parameterName, double? value)
    {
        AddInOutParameter(parameterName, value, ParameterDirection.Input);
        return this;
    }

    /// <summary>
    /// Creates input parameter of short type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <exception cref="System.ArgumentNullException" />
    public IDataObject AddInParameter(string parameterName, short? value)
    {
        AddInOutParameter(parameterName, value, ParameterDirection.Input);
        return this;
    }

    /// <summary>
    /// Creates input parameter of string type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="size">Maximum size of string.</param>
    /// <param name="variable">Flag for variable size.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    /// <exception cref="System.ArgumentNullException" />
    /// <exception cref="System.ArgumentOutOfRangeException" />
    public IDataObject AddInParameter(string parameterName, int size, bool variable)
    {
        AddInOutParameter(parameterName, size, variable, ParameterDirection.Input);
        return this;
    }

    /// <summary>
    /// Creates input parameter of int type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <exception cref="System.ArgumentNullException" />
    public IDataObject AddInParameter(string parameterName, int? value)
    {
        AddInOutParameter(parameterName, value, ParameterDirection.Input);
        return this;
    }

    /// <summary>
    /// Creates input parameter of long type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <exception cref="System.ArgumentNullException" />
    public IDataObject AddInParameter(string parameterName, long? value)
    {
        AddInOutParameter(parameterName, value, ParameterDirection.Input);
        return this;
    }

    /// <summary>
    /// Creates input parameter of <see cref="AdoNet.Fluent.NumericType"> type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="type">Parameter type.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    /// <exception cref="System.ArgumentNullException" />
    public IDataObject AddInParameter(string parameterName, NumericType type)
    {
        CheckParameter(parameterName);
        Command.Parameters.Add(CreateParameter(parameterName, type, ParameterDirection.Input));
        return this;
    }

    /// <summary>
    /// Creates input parameter of single type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <exception cref="System.ArgumentNullException" />
    public IDataObject AddInParameter(string parameterName, float? value)
    {
        AddInOutParameter(parameterName, value, ParameterDirection.Input);
        return this;
    }

    /// <summary>
    /// Creates input parameter for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="parameterTypeName">Database user-defined type.</param>
    /// <param name="dt">"<see cref="System.Data.DataTable"/> with data collection.</param>
    public abstract IDataObject AddInParameter(string parameterName, string parameterTypeName, DataTable dt);

    /// <summary>
    /// Creates input parameter of string type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <param name="size">Maximum size of string.</param>
    /// <exception cref="System.ArgumentNullException" />
    /// <exception cref="System.ArgumentOutOfRangeException" />
    public IDataObject AddInParameter(string parameterName, string? value, int size)
    {
        AddInOutParameter(parameterName, value, size, (size > _fixedSize), ParameterDirection.Input);
        return this;
    }

    /// <summary>
    /// Creates input parameter of string type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <param name="size">Maximum size of string.</param>
    /// <param name="variable">Flag for variable size.</param>
    public abstract IDataObject AddInParameter(string parameterName, string? value, int size, bool variable);

    /// <summary>
    /// Creates input parameter of XML for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">XmlReader with XML content to be used as parameter value.</param>
    /// <exception cref="System.NotSupportedException" />
    public virtual IDataObject AddInParameter(string parameterName, XmlReader value)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Creates output parameter of decimal type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="scale">Parameter scale.</param>
    /// <param name="precision">Parameter precision.</param>
    /// <exception cref="System.ArgumentNullException" />
    /// <exception cref="System.ArgumentOutOfRangeException" />
    public IDataObject AddOutParameter(string parameterName, byte precision, byte scale)
    {
        CheckParameter(parameterName, precision, scale);
        Command.Parameters.Add(CreateParameter(parameterName, ParameterDirection.Output, precision, scale));
        return this;
    }

    /// <summary>
    /// Creates output parameter of string type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="size">Maximum size of string.</param>
    /// <exception cref="System.ArgumentNullException" />
    /// <exception cref="System.ArgumentOutOfRangeException" />
    public IDataObject AddOutParameter(string parameterName, int size)
    {
        AddOutParameter(parameterName, size, (size > _fixedSize));
        return this;
    }

    /// <summary>
    /// Creates output parameter of string type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="size">Maximum size of string.</param>
    /// <param name="variable">Flag for variable size.</param>
    /// <exception cref="System.ArgumentNullException" />
    /// <exception cref="System.ArgumentOutOfRangeException" />
    public IDataObject AddOutParameter(string parameterName, int size, bool variable)
    {
        CheckParameter(parameterName, size);

        TParameter par = CreateParameter(parameterName, ParameterDirection.Output, size, variable);
        Command.Parameters.Add(par);

        return this;
    }

    /// <summary>
    /// Creates output parameter of <see cref="AdoNet.Fluent.NumericType"> type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="type">Parameter type.</param>
    /// <exception cref="System.ArgumentNullException" />
    public IDataObject AddOutParameter(string parameterName, NumericType type)
    {
        CheckParameter(parameterName);
        Command.Parameters.Add(CreateParameter(parameterName, type, ParameterDirection.Output));

        return this;
    }

    /// <summary>
    /// Creates output parameter of XML for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <exception cref="System.NotSupportedException" />
    public virtual IDataObject AddOutParameter(string parameterName)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Creates return parameter for database statement.
    /// </summary>
    /// <exception cref="System.InvalidOperationException" />
    public IDataObject AddReturnParameter()
    {
        if (Command.Parameters.Count > 0)
        {
            throw new InvalidOperationException(ResourcesFacade.GetString("ReturnParameterFirst"));
        }
        Command.Parameters.Add(CreateParameter(_parameterReturn, NumericType.Int32, ParameterDirection.ReturnValue));

        return this;
    }

    /// <summary>
    /// Disposes resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Performs operation to modify rows in database table.
    /// </summary>
    /// <returns>Number of rows affected.</returns>
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.ConstraintException" />
    /// <exception cref="System.Data.Common.DbException" />
    public int Execute()
    {
        CheckCommand();

        int returnValue = -1;

        try
        {
            OpenConnection();
            returnValue = Command.ExecuteNonQuery();
        }
        catch (TException ex)
        {
            HandleException(ex);
        }
        finally
        {
            CloseConnection();
        }

        return returnValue;
    }

    /// <summary>
    /// Performs assynchronous operation to modify rows in database table.
    /// </summary>
    /// <returns>Number of rows affected.</returns>	
    public async Task<int> ExecuteAsync()
    {
        return await ExecuteAsync(CancellationToken.None);
    }

    /// <summary>
    /// Performs assynchronous operation to modify rows in database table.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for request.</param>
    /// <returns>Number of rows affected.</returns>	
    public async Task<int> ExecuteAsync(CancellationToken cancellationToken)
    {
        CheckCommand();

        int returnValue = -1;

        try
        {
            await OpenConnectionAsync(cancellationToken);
            returnValue = await Command.ExecuteNonQueryAsync(cancellationToken);
        }
        catch (TException ex)
        {
            HandleException(ex);
        }
        finally
        {
            await CloseConnectionAsync();
        }

        return returnValue;
    }

    /// <summary>
    /// Returns output parameter value in binary data.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    public abstract byte[]? GetBinary(string parameterName);

    /// <summary>
    /// Returns output parameter value of boolean type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    public abstract bool GetBoolean(string parameterName);

    /// <summary>
    /// Returns output parameter value of byte type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    public abstract byte GetByte(string parameterName);

    /// <summary>
    /// Returns output parameter value of type <see cref="System.DateTime"/>.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    public abstract DateTime GetDateTime(string parameterName);

    /// <summary>
    /// Returns output parameter value of decimal type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    public abstract decimal GetDecimal(string parameterName);

    /// <summary>
    /// Returns output parameter value of double type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    public abstract double GetDouble(string parameterName);

    /// <summary>
    /// Returns output parameter value of short type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    public abstract short GetInt16(string parameterName);

    /// <summary>
    /// Returns output parameter value of int type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    public abstract int GetInt32(string parameterName);

    /// <summary>
    /// Returns output parameter value of long type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    public abstract long GetInt64(string parameterName);

    /// <summary>
    /// Returns output parameter value of boolean type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    public abstract bool? GetBooleanOrNull(string parameterName);

    /// <summary>
    /// Returns output parameter value of byte type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    public abstract byte? GetByteOrNull(string parameterName);

    /// <summary>
    /// Returns output parameter value of type <see cref="System.DateTime"/>.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    public abstract DateTime? GetDateTimeOrNull(string parameterName);

    /// <summary>
    /// Returns output parameter value of decimal type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    public abstract decimal? GetDecimalOrNull(string parameterName);

    /// <summary>
    /// Returns output parameter value of double type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    public abstract double? GetDoubleOrNull(string parameterName);

    /// <summary>
    /// Returns output parameter value of short type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    public abstract short? GetInt16OrNull(string parameterName);

    /// <summary>
    /// Returns output parameter value of int type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    public abstract int? GetInt32OrNull(string parameterName);

    /// <summary>
    /// Returns output parameter value of long type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    public abstract long? GetInt64OrNull(string parameterName);

    /// <summary>
    /// Return execution value.
    /// </summary>
    /// <returns>Return parameter value.</returns>
    public int GetReturn()
    {
        return GetInt32(_parameterReturn);
    }

    /// <summary>
    /// Returns output parameter value of single type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    public abstract float GetSingle(string parameterName);

    /// <summary>
    /// Returns output parameter value of string type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    public abstract string GetString(string parameterName);

    /// <summary>
    /// Returns output parameter value XML.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>XmlReader with output parameter Xml content.</returns>
    public virtual XmlReader? GetXml(string parameterName)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Returns output parameter value of single type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    public abstract float? GetSingleOrNull(string parameterName);

    /// <summary>
    /// <see cref="AdoNet.Fluent.ConnectionMode"/>
    /// </summary>
    public ConnectionMode Mode
    {
        get { return _connectionMode; }
    }

    /// <summary>
    /// Event triggered when an error occurs due to a violation of referential integrity.
    /// </summary>
    public event EventHandler<ConstraintErrorEventArgs>? OnConstraintError;

    /// <summary>
    /// Prepares database statement for repeated execution.
    /// </summary>
    /// <exception cref="System.Data.Common.DbException" />
    public void Prepare()
    {
        OpenConnection();
        Command.Prepare();
    }

    /// <summary>
    /// Prepares asynchronously database statement for repeated execution.
    /// </summary>
    /// <exception cref="System.Data.Common.DbException" />
    public async Task PrepareAsync()
    {
        await PrepareAsync(CancellationToken.None);
    }

    /// <summary>
    /// Prepares asynchronously database statement for repeated execution.
    /// <param name="cancellationToken">Cancellation token for request.</param>
    /// </summary>
    /// <exception cref="System.Data.Common.DbException" />
    public async Task PrepareAsync(CancellationToken token)
    {
        await OpenConnectionAsync(token);
        Command.Prepare();
    }

    /// <summary>
    /// Executes statement to query rows in database table.
    /// </summary>
    /// <param name="setter">Delegate to set order of columns in query result.</param>
    /// <param name="filler">Delegate to fill data obtained in query.</param>
    public void Read(SetOrdinal setter, Fill filler)
    {
        Read(setter, filler, CommandBehavior.Default);
    }

    /// <summary>
    /// Executes statement to query rows in database table.
    /// </summary>
    /// <param name="setter">Delegate to set order of columns in query result.</param>
    /// <param name="filler">Delegate to fill data obtained in query.</param>
    /// <param name="behavior"><see cref="System.Data.CommandBehavior"/> of query.</param>
    public virtual void Read(SetOrdinal setter, Fill filler, CommandBehavior behavior)
    {
        ArgumentNullException.ThrowIfNull(setter);
        ArgumentNullException.ThrowIfNull(filler);

        CheckCommand();

        if (_connectionMode == ConnectionMode.Normal && ((behavior & CommandBehavior.CloseConnection) == 0))
        {
            behavior |= CommandBehavior.CloseConnection;
        }

        try
        {
            OpenConnection();

            using DbDataReader reader = Command.ExecuteReader(behavior);
            setter(reader);

            while (reader.Read())
            {
                filler(reader);
            }
        }
        catch (TException ex)
        {
            HandleException(ex);
        }
    }

    /// <summary>
    /// Executes asynchronously statement to query rows in database table.
    /// </summary>
    /// <param name="setter">Delegate to set order of columns in query result.</param>
    /// <param name="filler">Delegate to fill data obtained in query.</param>
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public async Task ReadAsync(SetOrdinal setter, Fill filler)
    {
        await ReadAsync(setter, filler, CommandBehavior.Default, CancellationToken.None);
    }

    /// <summary>
    /// Executes asynchronously statement to query rows in database table.
    /// </summary>
    /// <param name="setter">Delegate to set order of columns in query result.</param>
    /// <param name="filler">Delegate to fill data obtained in query.</param>
    /// <param name="cancellationToken">Cancellation token for request.</param>    /// 
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public async Task ReadAsync(SetOrdinal setter, Fill filler, CancellationToken cancellationToken)
    {
        await ReadAsync(setter, filler, CommandBehavior.Default, cancellationToken);
    }

    /// <summary>
    /// Executes asynchronously statement to query rows in database table.
    /// </summary>
    /// <param name="setter">Delegate to set order of columns in query result.</param>
    /// <param name="filler">Delegate to fill data obtained in query.</param>
    /// <param name="cancellationToken">Cancellation token for request.</param>
    public virtual async Task ReadAsync(SetOrdinal setter, Fill filler, CommandBehavior behavior, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(setter);
        ArgumentNullException.ThrowIfNull(filler);

        CheckCommand();

        if (_connectionMode == ConnectionMode.Normal && ((behavior & CommandBehavior.CloseConnection) == 0))
        {
            behavior |= CommandBehavior.CloseConnection;
        }

        try
        {
            await OpenConnectionAsync(cancellationToken);

            using DbDataReader reader = await Command.ExecuteReaderAsync(behavior, cancellationToken);
            setter(reader);

            while (await reader.ReadAsync(cancellationToken))
            {
                filler(reader);
            }
        }
        catch (TException ex)
        {
            HandleException(ex);
        }
    }

    /// <summary>
    /// Executes asynchronously statement to query rows in database table.
    /// </summary>
    /// <param name="setter">Delegate to set order of columns in query result.</param>
    /// <param name="filler">Delegate to fill data obtained in query.</param>
    /// <param name="cancellationToken">Cancellation token for request.</param>
    public async Task ReadAsync(SetOrdinal setter, Fill filler, CommandBehavior behavior)
    {
        await ReadAsync(setter, filler, behavior, CancellationToken.None);
    }

    /// <summary>
    /// Executes database statement for scalar query that returns value in binary format.
    /// </summary>
    /// <returns>Scalar query value.</returns>
    public byte[]? ScalarBinary()
    {
        return ExecuteScalar() as byte[];
    }

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns value in binary format.
    /// </summary>
    /// <returns>Scalar query value.</returns>
    public async Task<byte[]?> ScalarBinaryAsync()
    {
        return await ExecuteScalarAsync(CancellationToken.None) as byte[];
    }

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns value in binary format.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for request.</param>
    /// <returns>Scalar query value.</returns>
    public async Task<byte[]?> ScalarBinaryAsync(CancellationToken token)
    {
        return await ExecuteScalarAsync(token) as byte[];
    }

    /// <summary>
    /// Executes database statement for scalar query that returns value of boolean type.
    /// </summary>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public bool? ScalarBoolean() => Parameter<bool>.GetScalar(ExecuteScalar()!);

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns value of boolean type.
    /// </summary>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
#pragma warning disable CS8604 // Possible null reference argument.
    public async Task<bool?> ScalarBooleanAsync() => Parameter<bool>.GetScalar(await ExecuteScalarAsync(CancellationToken.None));
#pragma warning restore CS8604 // Possible null reference argument.

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns value of boolean type.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for request.</param>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public async Task<bool?> ScalarBooleanAsync(CancellationToken token)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<bool>.GetScalar(await ExecuteScalarAsync(token))!;
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes database statement for scalar query that returns value of byte type.
    /// </summary>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public byte? ScalarByte()
    {
        return Parameter<byte>.GetScalar(ExecuteScalar()!);
    }

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns value of byte type.
    /// </summary>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public async Task<byte?> ScalarByteAsync()
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<byte>.GetScalar(await ExecuteScalarAsync(CancellationToken.None));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns value of byte type.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for request.</param>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public async Task<byte?> ScalarByteAsync(CancellationToken token)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<byte>.GetScalar(await ExecuteScalarAsync(token));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes database statement for scalar query that returns value of <see cref="System.DateTime"/>.
    /// </summary>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public DateTime? ScalarDateTime()
    {
        return Parameter<DateTime>.GetScalar(ExecuteScalar()!);
    }

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns value of <see cref="System.DateTime"/>.
    /// </summary>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public async Task<DateTime?> ScalarDateTimeAsync()
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<DateTime>.GetScalar(await ExecuteScalarAsync(CancellationToken.None));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns value of <see cref="System.DateTime"/>.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for request.</param>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public async Task<DateTime?> ScalarDateTimeAsync(CancellationToken token)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<DateTime>.GetScalar(await ExecuteScalarAsync(token)!);
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes database statement for scalar query that returns value of decimal type.
    /// </summary>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public decimal? ScalarDecimal()
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<decimal>.GetScalar(ExecuteScalar());
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns value of decimal type.
    /// </summary>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public async Task<decimal?> ScalarDecimalAsync()
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<decimal>.GetScalar(await ExecuteScalarAsync(CancellationToken.None));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns value of decimal type.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for request.</param>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public async Task<decimal?> ScalarDecimalAsync(CancellationToken token)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<decimal>.GetScalar(await ExecuteScalarAsync(token));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes database statement for scalar query that returns value of double type.
    /// </summary>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public double? ScalarDouble()
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<double>.GetScalar(ExecuteScalar());
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns value of double type.
    /// </summary>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public async Task<double?> ScalarDoubleAsync()
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<double>.GetScalar(await ExecuteScalarAsync(CancellationToken.None));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns value of double type.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for request.</param>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public async Task<double?> ScalarDoubleAsync(CancellationToken token)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<double>.GetScalar(await ExecuteScalarAsync(token));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes database statement for scalar query that returns value of short type.
    /// </summary>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public short? ScalarInt16()
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<short>.GetScalar(ExecuteScalar());
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns value of short type.
    /// </summary>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public async Task<short?> ScalarInt16Async()
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<short>.GetScalar(await ExecuteScalarAsync(CancellationToken.None));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns value of short type.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for request.</param>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public async Task<short?> ScalarInt16Async(CancellationToken token)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<short>.GetScalar(await ExecuteScalarAsync(token));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes database statement for scalar query that returns value of int type.
    /// </summary>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public int? ScalarInt32()
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<int>.GetScalar(ExecuteScalar());
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns value of int type.
    /// </summary>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public async Task<int?> ScalarInt32Async()
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<int>.GetScalar(await ExecuteScalarAsync(CancellationToken.None));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns value of int type.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for request.</param>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public async Task<int?> ScalarInt32Async(CancellationToken token)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<int>.GetScalar(await ExecuteScalarAsync(token));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes database statement for scalar query that returns value of long type.
    /// </summary>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public long? ScalarInt64()
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<long>.GetScalar(ExecuteScalar());
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns value of long type.
    /// </summary>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public async Task<long?> ScalarInt64Async()
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<long>.GetScalar(await ExecuteScalarAsync(CancellationToken.None));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns value of long type.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for request.</param>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public async Task<long?> ScalarInt64Async(CancellationToken token)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<long>.GetScalar(await ExecuteScalarAsync(token));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes database statement for scalar query that returns value of single type.
    /// </summary>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public float? ScalarSingle()
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<float>.GetScalar(ExecuteScalar());
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns value of single type.
    /// </summary>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public async Task<float?> ScalarSingleAsync()
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<float>.GetScalar(await ExecuteScalarAsync(CancellationToken.None));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns value of single type.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for request.</param>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public async Task<float?> ScalarSingleAsync(CancellationToken token)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Parameter<float>.GetScalar(await ExecuteScalarAsync(token));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes database statement for scalar query that returns value of string type.
    /// </summary>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public string? ScalarString()
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return DataObject<TConnection, TCommand, TParameter, TException>.GetScalarString(ExecuteScalar());
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns value of string type.
    /// </summary>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public async Task<string?> ScalarStringAsync()
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return DataObject<TConnection, TCommand, TParameter, TException>.GetScalarString(await ExecuteScalarAsync(CancellationToken.None));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns value of string type.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for request.</param>
    /// <returns>Scalar query value.</returns>
    /// <exception cref="System.InvalidCastException" />
    /// <exception cref="System.InvalidOperationException" />
    /// <exception cref="System.Data.Common.DbException" />
    public async Task<string?> ScalarStringAsync(CancellationToken token)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return DataObject<TConnection, TCommand, TParameter, TException>.GetScalarString(await ExecuteScalarAsync(token));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Executes database statement for scalar query that returns XML.
    /// </summary>
    /// <param name="filler">Delegate to fill data in XML format retrieved in query.</param>
    /// <exception cref="System.NotSupportedException" />
    public virtual void ScalarXml(FillXml filler)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Executes asynchronously database statement for scalar query that returns XML.
    /// </summary>
    /// <param name="filler">Delegate to fill data in XML format retrieved in query.</param>
    /// <exception cref="System.NotSupportedException" />
    public virtual Task ScalarXmlAsync(FillXmlAsync filler)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Executa instrução assíncrona para consulta escalar em banco de dados que retorna valor XML.
    /// </summary>
    /// <param name="filler">Delegate to fill data in XML format retrieved in query.</param>
    /// <param name="cancellationToken">Cancellation token for request.</param>
    /// <returns>XmlReader com o conteúdo da consulta escalar.</returns>
    /// <exception cref="System.NotSupportedException" />
    public virtual Task ScalarXmlAsync(FillXmlAsync filler, CancellationToken cancellationToken)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Set value for parameter of boolean type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    public abstract IDataObject SetParameter(string parameterName, bool? value);

    /// <summary>
    /// Set value for parameter of byte type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    public abstract IDataObject SetParameter(string parameterName, byte? value);

    /// <summary>
    /// Set value for parameter of <see cref="System.DateTime"/>.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    public abstract IDataObject SetParameter(string parameterName, DateTime? value);

    /// <summary>
    /// Set value for parameter of decimal type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    public abstract IDataObject SetParameter(string parameterName, decimal? value);

    /// <summary>
    /// Set value for parameter of double type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    public abstract IDataObject SetParameter(string parameterName, double? value);

    /// <summary>
    /// Set value for parameter of short type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    public abstract IDataObject SetParameter(string parameterName, short? value);

    /// <summary>
    /// Set value for parameter of int type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    public abstract IDataObject SetParameter(string parameterName, int? value);

    /// <summary>
    /// Set value for parameter of long type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    public abstract IDataObject SetParameter(string parameterName, long? value);

    /// <summary>
    /// Set value for parameter of single type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    public abstract IDataObject SetParameter(string parameterName, float? value);

    /// <summary>
    /// Set value for parameter of string type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    public abstract IDataObject SetParameter(string parameterName, string? value);

    /// <summary>
    /// Set value for XML parameter.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">XmlReader with parameter value content.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    /// <exception cref="System.NotSupportedException" />
    public virtual IDataObject SetParameter(string parameterName, XmlReader value)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Set SQL statement to execute in database.
    /// </summary>
    /// <param name="sql">Statement to execute in database.</param>
    public IDataObject SetSql(string sql)
    {
        SetCommand(sql, CommandType.Text);
        return this;
    }

    /// <summary>
    /// Set stored procedure to execute in database.
    /// </summary>
    /// <param name="command">Stored procedure to execute in database.</param>
    public IDataObject SetStoredProcedure(string command)
    {
        SetCommand(command, CommandType.StoredProcedure);
        return this;
    }

    #endregion
}