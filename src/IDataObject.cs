using System.Data;
using System.Data.Common;
using System.Xml;

namespace AdoNet.Fluent;

/// <summary>
/// Delegate to fill data obtained in query.
/// </summary>
public delegate void Fill(DbDataReader reader);

/// <summary>
/// Delegate to fill data obtained in query in XML format.
/// </summary>
public delegate void FillXml(XmlReader reader);

/// <summary>
/// Delegate to asynchronously fill data obtained in query in XML format.
/// </summary>
public delegate Task FillXmlAsync(XmlReader reader);

/// <summary>
/// Delegate to set order of columns in query result.
/// </summary>
public delegate void SetOrdinal(IDataRecord reader);

/// <summary>
/// Interface for operations on relational databases.
/// </summary>
public interface IDataObject : IDisposable
{
    /// <summary>
    /// Creates input/output parameter of boolean type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    IDataObject AddInOutParameter(string parameterName, bool value);

    /// <summary>
    /// Creates input/output parameter of decimal type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="scale">Parameter scale.</param>
    /// <param name="precision">Parameter precision.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    IDataObject AddInOutParameter(string parameterName, byte precision, byte scale);

    /// <summary>
    /// Creates input/output parameter of byte type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    IDataObject AddInOutParameter(string parameterName, byte? value);

    /// <summary>
    /// Creates input/output parameter of <see cref="System.DateTime"/> type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    IDataObject AddInOutParameter(string parameterName, DateTime? value);

    /// <summary>
    /// Creates input/output parameter of decimal type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <param name="scale">Parameter scale.</param>
    /// <param name="precision">Parameter precision.</param>
    IDataObject AddInOutParameter(string parameterName, decimal? value, byte precision, byte scale);

    /// <summary>
    /// Creates input/output parameter of double type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    IDataObject AddInOutParameter(string parameterName, double? value);

    /// <summary>
    /// Creates input/output parameter of short type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    IDataObject AddInOutParameter(string parameterName, short? value);

    /// <summary>
    /// Creates input/output parameter of string type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="size">Maximum size of string.</param>
    /// <param name="variable">Verdadeiro se string tem tamanho variável.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    IDataObject AddInOutParameter(string parameterName, int size, bool variable);

    /// <summary>
    /// Creates input/output parameter of int type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    IDataObject AddInOutParameter(string parameterName, int? value);

    /// <summary>
    /// Creates input/output parameter of long type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    IDataObject AddInOutParameter(string parameterName, long? value);

    /// <summary>
    /// Creates input/output parameter of <see cref="AdoNet.Fluent.NumericType"> type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="type">Parameter type.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    IDataObject AddInOutParameter(string parameterName, NumericType type);

    /// <summary>
    /// Creates input/output parameter of single type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    IDataObject AddInOutParameter(string parameterName, float? value);

    /// <summary>
    /// Cria parâmetro bidirecional do tipo "String" para instrução de banco de dados.
    /// </summary>
    /// <param name="parameterName">Nome do parâmetro.</param>
    /// <param name="value">Valor do parâmetro.</param>
    /// <param name="size">Tamanho máximo da string.</param>
    IDataObject AddInOutParameter(string parameterName, string? value, int size);

    /// <summary>
    /// Creates input/output parameter of string type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <param name="size">Maximum size of string.</param>
    /// <param name="variable">Flag for variable size.</param>
    IDataObject AddInOutParameter(string parameterName, string? value, int size, bool variable);

    /// <summary>
    /// Creates XML input parameter for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <remarks>For use in creating XML type parameter.</remarks>
    IDataObject AddInParameter(string parameterName);

    /// <summary>
    /// Creates input parameter of boolean type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    IDataObject AddInParameter(string parameterName, bool value);

    /// <summary>
    /// Creates input parameter of decimal type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="scale">Parameter scale.</param>
    /// <param name="precision">Parameter precision.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    IDataObject AddInParameter(string parameterName, byte precision, byte scale);

    /// <summary>
    /// Creates input parameter of byte type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    IDataObject AddInParameter(string parameterName, byte? value);

    /// <summary>
    /// Creates input parameter of binary data for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    IDataObject AddInParameter(string parameterName, byte[] value);

    /// <summary>
    /// Creates input parameter of <see cref="System.DateTime"/> for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    IDataObject AddInParameter(string parameterName, DateTime? value);

    /// <summary>
    /// Creates input parameter of decimal type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <param name="scale">Parameter scale.</param>
    /// <param name="precision">Parameter precision.</param>
    IDataObject AddInParameter(string parameterName, decimal? value, byte precision, byte scale);

    /// <summary>
    /// Creates input parameter of double type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</
    IDataObject AddInParameter(string parameterName, double? value);

    /// <summary>
    /// Creates input parameter of short type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    IDataObject AddInParameter(string parameterName, short? value);

    /// <summary>
    /// Creates input parameter of string type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="size">Maximum size of string.</param>
    /// <param name="variable">Flag for variable size.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    IDataObject AddInParameter(string parameterName, int size, bool variable);

    /// <summary>
    /// Creates input parameter of int type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    IDataObject AddInParameter(string parameterName, int? value);

    /// <summary>
    /// Creates input parameter of long type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    IDataObject AddInParameter(string parameterName, long? value);

    /// Creates input parameter of <see cref="AdoNet.Fluent.NumericType"> type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="type">Parameter type.</param>
    /// <remarks>For <seealso cref="System.Data.IDbCommand.Prepare">prepared</seealso> instructions.</remarks>
    IDataObject AddInParameter(string parameterName, NumericType type);

    /// <summary>
    /// Creates input parameter of single type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    IDataObject AddInParameter(string parameterName, float? value);

    /// <summary>
    /// Creates input parameter for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="parameterTypeName">Database user-defined type.</param>
    /// <param name="dt">"<see cref="System.Data.DataTable"/> with data collection.</param>
    IDataObject AddInParameter(string parameterName, string parameterTypeName, DataTable dt);

    /// <summary>
    /// Creates input parameter of string type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <param name="size">Maximum size of string.</param>
    IDataObject AddInParameter(string parameterName, string? value, int size);

    /// <summary>
    /// Creates input parameter of string type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <param name="size">Maximum size of string.</param>
    /// <param name="variable">Flag for variable size.</param>
    IDataObject AddInParameter(string parameterName, string? value, int size, bool variable);

    /// <summary>
    /// Creates input parameter of XML for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="value">XmlReader with XML content to be used as parameter value.</param>
    IDataObject AddInParameter(string parameterName, XmlReader value);

    /// <summary>
    /// Creates output parameter of decimal type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="scale">Parameter scale.</param>
    /// <param name="precision">Parameter precision.</param>
    IDataObject AddOutParameter(string parameterName, byte precision, byte scale);

    /// <summary>
    /// Creates output parameter of string type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="size">Maximum size of string.</param>
    IDataObject AddOutParameter(string parameterName, int size);

    /// <summary>
    /// Creates output parameter of string type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="size">Maximum size of string.</param>
    /// <param name="variable">Flag for variable size.</param>
    IDataObject AddOutParameter(string parameterName, int size, bool variable);

    /// <summary>
    /// Creates output parameter of <see cref="AdoNet.Fluent.NumericType"> type for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="type">Parameter type.</param>
    IDataObject AddOutParameter(string parameterName, NumericType type);

    /// <summary>
    /// Creates output parameter of XML for database statement.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    IDataObject AddOutParameter(string parameterName);

    /// <summary>
    /// Creates return parameter for database statement.
    /// </summary>
    IDataObject AddReturnParameter();

    /// <summary>
    /// Performs operation to modify rows in database table.
    /// </summary>
    /// <returns>Number of rows affected.</returns>
    int Execute();

    /// <summary>
    /// Performs assynchronous operation to modify rows in database table.
    /// </summary>
    /// <returns>Number of rows affected.</returns>	
    Task<int> ExecuteAsync();

    /// <summary>
    /// Performs assynchronous operation to modify rows in database table.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for request.</param>
    /// <returns>Number of rows affected.</returns>	
    Task<int> ExecuteAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Returns output parameter value in binary data.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    byte[]? GetBinary(string parameterName);

    /// <summary>
    /// Returns output parameter value of boolean type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    bool GetBoolean(string parameterName);

    /// <summary>
    /// Returns output parameter value of byte type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    byte GetByte(string parameterName);

    /// <summary>
    /// Returns output parameter value of type <see cref="System.DateTime"/>.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    DateTime GetDateTime(string parameterName);

    /// <summary>
    /// Returns output parameter value of decimal type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    decimal GetDecimal(string parameterName);

    /// <summary>
    /// Returns output parameter value of double type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    double GetDouble(string parameterName);

    /// <summary>
    /// Returns output parameter value of short type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    short GetInt16(string parameterName);

    /// <summary>
    /// Returns output parameter value of int type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    int GetInt32(string parameterName);

    /// <summary>
    /// Returns output parameter value of long type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    long GetInt64(string parameterName);

    /// <summary>
    /// Returns output parameter value of boolean type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    bool? GetBooleanOrNull(string parameterName);

    /// <summary>
    /// Returns output parameter value of byte type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    byte? GetByteOrNull(string parameterName);

    /// <summary>
    /// Returns output parameter value of type <see cref="System.DateTime"/>.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    DateTime? GetDateTimeOrNull(string parameterName);

    /// <summary>
    /// Returns output parameter value of decimal type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    decimal? GetDecimalOrNull(string parameterName);

    /// <summary>
    /// Returns output parameter value of double type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    double? GetDoubleOrNull(string parameterName);

    /// <summary>
    /// Returns output parameter value of short type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    short? GetInt16OrNull(string parameterName);

    /// <summary>
    /// Returns output parameter value of int type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    int? GetInt32OrNull(string parameterName);

    /// <summary>
    /// Returns output parameter value of long type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    long? GetInt64OrNull(string parameterName);

    /// <summary>
    /// Return execution value.
    /// </summary>
    /// <returns>Return parameter value.</returns>
    int GetReturn();

    /// <summary>
    /// Returns output parameter value of single type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    float GetSingle(string parameterName);

    /// <summary>
    /// Returns output parameter value of string type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    string GetString(string parameterName);

    /// <summary>
    /// Returns output parameter value XML.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>XmlReader with output parameter Xml content.</returns>
    XmlReader? GetXml(string parameterName);

    /// <summary>
    /// Returns output parameter value of single type.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <returns>Output parameter value.</returns>
    float? GetSingleOrNull(string parameterName);

    /// <summary>
    /// Evento disparado na ocorrência de erro por violação de integridade referencial.
    /// </summary>
    event EventHandler<ConstraintErrorEventArgs>? OnConstraintError;

    /// <summary>
    /// Prepara instrução de banco de dados para execuções repetidas.
    /// </summary>
    void Prepare();

    /// <summary>
    /// Prepara instrução assíncrona de banco de dados para execuções repetidas.
    /// </summary>
    Task PrepareAsync();

    /// <summary>
    /// Prepara instrução assíncrona de banco de dados para execuções repetidas.
    /// <param name="cancellationToken">Token para monitorar pedidos de cancelamento.</param>
    /// </summary>
    Task PrepareAsync(CancellationToken token);

    /// <summary>
    /// Executa instrução para consultar linhas em tabelas de banco de dados.
    /// </summary>
    /// <param name="setter">"Delegate" para definir posição das colunas de dados da consulta.</param>
    /// <param name="filler">"Delegate" para preencher dados obtidos na consulta.</param>
    /// <param name="behaviour">Comportamento da consulta.</param>
    void Read(SetOrdinal setter, Fill filler);

    /// <summary>
    /// Executa instrução para consultar linhas em tabelas de banco de dados.
    /// </summary>
    /// <param name="setter">"Delegate" para definir posição das colunas de dados da consulta.</param>
    /// <param name="filler">"Delegate" para preencher dados obtidos na consulta.</param>
    /// <param name="behaviour">Comportamento da consulta.</param>
    void Read(SetOrdinal setter, Fill filler, CommandBehavior behaviour);

    /// <summary>
    /// Executa instrução assíncrona para consultar linhas em tabelas de banco de dados.
    /// </summary>
    /// <param name="setter">"Delegate" para definir posição das colunas de dados da consulta.</param>
    /// <param name="filler">"Delegate" para preencher dados obtidos na consulta.</param>
    Task ReadAsync(SetOrdinal setter, Fill filler);

    /// <summary>
    /// Executa instrução assíncrona para consultar linhas em tabelas de banco de dados.
    /// </summary>
    /// <param name="setter">"Delegate" para definir posição das colunas de dados da consulta.</param>
    /// <param name="filler">"Delegate" para preencher dados obtidos na consulta.</param>
    /// <param name="cancellationToken">Token para monitorar pedidos de cancelamento.</param>
    Task ReadAsync(SetOrdinal setter, Fill filler, CancellationToken cancellationToken);

    /// <summary>
    /// Executa instrução assíncrona para consultar linhas em tabelas de banco de dados.
    /// </summary>
    /// <param name="setter">"Delegate" para definir posição das colunas de dados da consulta.</param>
    /// <param name="filler">"Delegate" para preencher dados obtidos na consulta.</param>
    /// <param name="behaviour">Comportamento da consulta.</param>
    /// <param name="cancellationToken">Token para monitorar pedidos de cancelamento.</param>
    Task ReadAsync(SetOrdinal setter, Fill filler, CommandBehavior behaviour, CancellationToken cancellationToken);

    /// <summary>
    /// Executa instrução assíncrona para consultar linhas em tabelas de banco de dados.
    /// </summary>
    /// <param name="setter">"Delegate" para definir posição das colunas de dados da consulta.</param>
    /// <param name="filler">"Delegate" para preencher dados obtidos na consulta.</param>
    /// <param name="behaviour">Comportamento da consulta.</param>
    Task ReadAsync(SetOrdinal setter, Fill filler, CommandBehavior behaviour);

    /// <summary>
    /// Executa instrução em banco de dados para consulta escalar que retorna valor em formato binário.
    /// </summary>
    /// <returns>Valor da consulta escalar.</returns>
    byte[]? ScalarBinary();

    /// <summary>
    /// Executa instrução assíncrona  em banco de dados para consulta escalar que retorna valor em formato binário.
    /// </summary>
    /// <returns>Valor da consulta escalar.</returns>
    Task<byte[]?> ScalarBinaryAsync();

    /// <summary>
    /// Executa instrução assíncrona  em banco de dados para consulta escalar que retorna valor em formato binário.
    /// </summary>
    /// <param name="cancellationToken">Token para monitorar pedidos de cancelamento.</param>
    /// <returns>Valor da consulta escalar.</returns>
    Task<byte[]?> ScalarBinaryAsync(CancellationToken token);

    /// <summary>
    /// Executa instrução em banco de dados para consulta escalar que retorna valor do tipo "Boolean".
    /// </summary>
    /// <returns>Valor da consulta escalar.</returns>
    bool? ScalarBoolean();

    /// <summary>
    /// Executa instrução assíncrona  em banco de dados para consulta escalar que retorna valor do tipo "Boolean".
    /// </summary>
    /// <returns>Valor da consulta escalar.</returns>
    Task<bool?> ScalarBooleanAsync();

    /// <summary>
    /// Executa instrução assíncrona  em banco de dados para consulta escalar que retorna valor do tipo "Boolean".
    /// </summary>
    /// <param name="cancellationToken">Token para monitorar pedidos de cancelamento.</param>
    /// <returns>Valor da consulta escalar.</returns>
    Task<bool?> ScalarBooleanAsync(CancellationToken token);

    /// <summary>
    /// Executa instrução em banco de dados para consulta escalar que retorna valor do tipo "Byte".
    /// </summary>
    /// <returns>Valor da consulta escalar.</returns>
    byte? ScalarByte();

    /// <summary>
    /// Executa instrução assíncrona  em banco de dados para consulta escalar que retorna valor do tipo "Byte".
    /// </summary>
    /// <returns>Valor da consulta escalar.</returns>
    Task<byte?> ScalarByteAsync();

    /// <summary>
    /// Executa instrução assíncrona  em banco de dados para consulta escalar que retorna valor do tipo "Byte".
    /// </summary>
    /// <param name="cancellationToken">Token para monitorar pedidos de cancelamento.</param>
    /// <returns>Valor da consulta escalar.</returns>
    Task<byte?> ScalarByteAsync(CancellationToken token);

    /// <summary>
    /// Executa instrução em banco de dados para consulta escalar que retorna valor do tipo "DateTime".
    /// </summary>
    /// <returns>Valor da consulta escalar.</returns>
    DateTime? ScalarDateTime();

    /// <summary>
    /// Executa instrução assíncrona  em banco de dados para consulta escalar que retorna valor do tipo "DateTime".
    /// </summary>
    /// <returns>Valor da consulta escalar.</returns>
    Task<DateTime?> ScalarDateTimeAsync();

    /// <summary>
    /// Executa instrução assíncrona  em banco de dados para consulta escalar que retorna valor do tipo "DateTime".
    /// </summary>
    /// <param name="cancellationToken">Token para monitorar pedidos de cancelamento.</param>
    /// <returns>Valor da consulta escalar.</returns>
    Task<DateTime?> ScalarDateTimeAsync(CancellationToken token);

    /// <summary>
    /// Executa instrução em banco de dados para consulta escalar que retorna valor do tipo "Decimal".
    /// </summary>
    /// <returns>Valor da consulta escalar.</returns>
    decimal? ScalarDecimal();

    /// <summary>
    /// Executa instrução assíncrona  em banco de dados para consulta escalar que retorna valor do tipo "Decimal".
    /// </summary>
    /// <returns>Valor da consulta escalar.</returns>
    Task<decimal?> ScalarDecimalAsync();

    /// <summary>
    /// Executa instrução assíncrona  em banco de dados para consulta escalar que retorna valor do tipo "Decimal".
    /// </summary>
    /// <param name="cancellationToken">Token para monitorar pedidos de cancelamento.</param>
    /// <returns>Valor da consulta escalar.</returns>
    Task<decimal?> ScalarDecimalAsync(CancellationToken token);

    /// <summary>
    /// Executa instrução em banco de dados para consulta escalar que retorna valor do tipo "Double".
    /// </summary>
    /// <returns>Valor da consulta escalar.</returns>
    double? ScalarDouble();

    /// <summary>
    /// Executa instrução assíncrona  em banco de dados para consulta escalar que retorna valor do tipo "Double".
    /// </summary>
    /// <returns>Valor da consulta escalar.</returns>
    Task<double?> ScalarDoubleAsync();

    /// <summary>
    /// Executa instrução assíncrona  em banco de dados para consulta escalar que retorna valor do tipo "Double".
    /// </summary>
    /// <param name="cancellationToken">Token para monitorar pedidos de cancelamento.</param>
    /// <returns>Valor da consulta escalar.</returns>
    Task<double?> ScalarDoubleAsync(CancellationToken token);

    /// <summary>
    /// Executa instrução em banco de dados para consulta escalar que retorna valor do tipo "Int16".
    /// </summary>
    /// <returns>Valor da consulta escalar.</returns>
    short? ScalarInt16();

    /// <summary>
    /// Executa instrução assíncrona  em banco de dados para consulta escalar que retorna valor do tipo "Int16".
    /// </summary>
    /// <returns>Valor da consulta escalar.</returns>
    Task<short?> ScalarInt16Async();

    /// <summary>
    /// Executa instrução assíncrona  em banco de dados para consulta escalar que retorna valor do tipo "Int16".
    /// </summary>
    /// <param name="cancellationToken">Token para monitorar pedidos de cancelamento.</param>
    /// <returns>Valor da consulta escalar.</returns>
    Task<short?> ScalarInt16Async(CancellationToken token);

    /// <summary>
    /// Executa instrução em banco de dados para consulta escalar que retorna valor do tipo "Int32".
    /// </summary>
    /// <returns>Valor da consulta escalar.</returns>
    int? ScalarInt32();

    /// <summary>
    /// Executa instrução assíncrona  em banco de dados para consulta escalar que retorna valor do tipo "Int32".
    /// </summary>
    /// <returns>Valor da consulta escalar.</returns>
    Task<int?> ScalarInt32Async();

    /// <summary>
    /// Executa instrução assíncrona  em banco de dados para consulta escalar que retorna valor do tipo "Int32".
    /// </summary>
    /// <param name="cancellationToken">Token para monitorar pedidos de cancelamento.</param>
    /// <returns>Valor da consulta escalar.</returns>
    Task<int?> ScalarInt32Async(CancellationToken token);

    /// <summary>
    /// Executa instrução em banco de dados para consulta escalar que retorna valor do tipo "Int64".
    /// </summary>
    /// <returns>Valor da consulta escalar.</returns>
    long? ScalarInt64();

    /// <summary>
    /// Executa instrução assíncrona  em banco de dados para consulta escalar que retorna valor do tipo "Int64".
    /// </summary>
    /// <returns>Valor da consulta escalar.</returns>
    Task<long?> ScalarInt64Async();

    /// <summary>
    /// Executa instrução assíncrona  em banco de dados para consulta escalar que retorna valor do tipo "Int64".
    /// </summary>
    /// <param name="cancellationToken">Token para monitorar pedidos de cancelamento.</param>
    /// <returns>Valor da consulta escalar.</returns>
    Task<long?> ScalarInt64Async(CancellationToken token);

    /// <summary>
    /// Executa instrução para consulta escalar em banco de dados que retorna valor do tipo "Single".
    /// </summary>
    /// <returns>Valor da consulta escalar.</returns>
    float? ScalarSingle();

    /// <summary>
    /// Executa instrução assíncrona  para consulta escalar em banco de dados que retorna valor do tipo "Single".
    /// </summary>
    /// <returns>Valor da consulta escalar.</returns>
    Task<float?> ScalarSingleAsync();

    /// <summary>
    /// Executa instrução assíncrona  para consulta escalar em banco de dados que retorna valor do tipo "Single".
    /// </summary>
    /// <param name="cancellationToken">Token para monitorar pedidos de cancelamento.</param>
    /// <returns>Valor da consulta escalar.</returns>
    Task<float?> ScalarSingleAsync(CancellationToken token);

    /// <summary>
    /// Executa instrução para consulta escalar em banco de dados que retorna valor do tipo "String".
    /// </summary>
    /// <returns>Valor da consulta escalar.</returns>
    string? ScalarString();

    /// <summary>
    /// Executa instrução assíncrona  para consulta escalar em banco de dados que retorna valor do tipo "String".
    /// </summary>
    /// <returns>Valor da consulta escalar.</returns>
    Task<string?> ScalarStringAsync();

    /// <summary>
    /// Executa instrução assíncrona  para consulta escalar em banco de dados que retorna valor do tipo "String".
    /// </summary>
    /// <param name="cancellationToken">Token para monitorar pedidos de cancelamento.</param>
    /// <returns>Valor da consulta escalar.</returns>
    Task<string?> ScalarStringAsync(CancellationToken token);

    /// <summary>
    /// Executa instrução para consulta escalar em banco de dados que retorna valor do tipo "Xml".
    /// </summary>
    /// <param name="filler">"Delegate" para preencher dados em formato XML obtidos na consulta.</param>
    void ScalarXml(FillXml filler);

    /// <summary>
    /// Executa instrução assíncrona para consulta escalar em banco de dados que retorna valor do tipo "Xml".
    /// </summary>
    /// <param name="filler">"Delegate" para preencher dados em formato XML obtidos na consulta.</param>
    Task ScalarXmlAsync(FillXmlAsync filler);

    /// <summary>
    /// Executa instrução assíncrona para consulta escalar em banco de dados que retorna valor do tipo "Xml".
    /// </summary>
    /// <param name="filler">"Delegate" para preencher dados em formato XML obtidos na consulta.</param>
    /// <param name="cancellationToken">Token para monitorar pedidos de cancelamento.</param>
    Task ScalarXmlAsync(FillXmlAsync filler, CancellationToken cancellationToken);

    /// <summary>
    /// Define o valor para parâmetro do tipo "Boolean".
    /// </summary>
    /// <param name="parameterName">Nome do parâmetro.</param>
    /// <param name="value">Valor do parâmetro.</param>
    /// <remarks>Para execução de instruções <seealso cref="Prepare">preparadas</seealso>.</remarks>
    IDataObject SetParameter(string parameterName, bool? value);

    /// <summary>
    /// Define o valor para parâmetro do tipo "Byte".
    /// </summary>
    /// <param name="parameterName">Nome do parâmetro.</param>
    /// <param name="value">Valor do parâmetro.</param>
    /// <remarks>Para execução de instruções <seealso cref="Prepare">preparadas</seealso>.</remarks>
    IDataObject SetParameter(string parameterName, byte? value);

    /// <summary>
    /// Define o valor para parâmetro do tipo "DateTime".
    /// </summary>
    /// <param name="parameterName">Nome do parâmetro.</param>
    /// <param name="value">Valor do parâmetro.</param>
    /// <remarks>Para execução de instruções <seealso cref="Prepare">preparadas</seealso>.</remarks>
    IDataObject SetParameter(string parameterName, DateTime? value);

    /// <summary>
    /// Define o valor para parâmetro do tipo "Decimal".
    /// </summary>
    /// <param name="parameterName">Nome do parâmetro.</param>
    /// <param name="value">Valor do parâmetro.</param>
    /// <remarks>Para execução de instruções <seealso cref="Prepare">preparadas</seealso>.</remarks>
    IDataObject SetParameter(string parameterName, decimal? value);

    /// <summary>
    /// Define o valor para parâmetro do tipo "Double".
    /// </summary>
    /// <param name="parameterName">Nome do parâmetro.</param>
    /// <param name="value">Valor do parâmetro.</param>
    /// <remarks>Para execução de instruções <seealso cref="Prepare">preparadas</seealso>.</remarks>
    IDataObject SetParameter(string parameterName, double? value);

    /// <summary>
    /// Define o valor para parâmetro do tipo "Int16".
    /// </summary>
    /// <param name="parameterName">Nome do parâmetro.</param>
    /// <param name="value">Valor do parâmetro.</param>
    /// <remarks>Para execução de instruções <seealso cref="Prepare">preparadas</seealso>.</remarks>
    IDataObject SetParameter(string parameterName, short? value);

    /// <summary>
    /// Define o valor para parâmetro do tipo "Int32".
    /// </summary>
    /// <param name="parameterName">Nome do parâmetro.</param>
    /// <param name="value">Valor do parâmetro.</param>
    /// <remarks>Para execução de instruções <seealso cref="Prepare">preparadas</seealso>.</remarks>
    IDataObject SetParameter(string parameterName, int? value);

    /// <summary>
    /// Define o valor para parâmetro do tipo "Int64".
    /// </summary>
    /// <param name="parameterName">Nome do parâmetro.</param>
    /// <param name="value">Valor do parâmetro.</param>
    /// <remarks>Para execução de instruções <seealso cref="Prepare">preparadas</seealso>.</remarks>
    IDataObject SetParameter(string parameterName, long? value);

    /// <summary>
    /// Define o valor para parâmetro do tipo "Single".
    /// </summary>
    /// <param name="parameterName">Nome do parâmetro.</param>
    /// <param name="value">Valor do parâmetro.</param>
    /// <remarks>Para execução de instruções <seealso cref="Prepare">preparadas</seealso>.</remarks>
    IDataObject SetParameter(string parameterName, float? value);

    /// <summary>
    /// Define o valor para parâmetro do tipo "String".
    /// </summary>
    /// <param name="parameterName">Nome do parâmetro.</param>
    /// <param name="value">Valor do parâmetro.</param>
    /// <remarks>Para execução de instruções <seealso cref="Prepare">preparadas</seealso>.</remarks>
    IDataObject SetParameter(string parameterName, string? value);

    /// <summary>
    /// Define o valor para parâmetro do tipo "Xml".
    /// </summary>
    /// <param name="parameterName">Nome do parâmetro.</param>
    /// <param name="value">XmlReader com o contédo do parâmetro.</param>
    /// <remarks>Para execução de instruções <seealso cref="Prepare">preparadas</seealso>.</remarks>
    IDataObject SetParameter(string parameterName, XmlReader value);

    /// <summary>
    /// Define instrução SQL para executar no banco de dados.
    /// </summary>
    /// <param name="cmdText">Instrução a executar no banco de dados.</param>
    IDataObject SetSql(string sql);

    /// <summary>
    /// Define stored procedure para executar no banco de dados.
    /// </summary>
    /// <param name="cmdText">Instrução a executar no banco de dados.</param>
    IDataObject SetStoredProcedure(string command);
}