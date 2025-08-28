using System.Data.Common;

namespace AdoNet.Fluent;

/// <summary>
/// Static class for conversion methods from relational databases.
/// </summary>
/// <typeparam name="T">Numeric structure type.</typeparam>
public static class Parameter<T>
    where T : struct
{
    /// <summary>
    /// Converts received value in scalar query.
    /// </summary>
    /// <param name="value">Valor retornado na consulta escalar.</param>
    /// <returns>Converted value.</returns>
    public static T? GetScalar(object value)
    {
        T? nulo = null;
        return value == null ? nulo : (T)Convert.ChangeType(value, typeof(T));
    }
}

/// <summary>
/// Static class for conversion methods from relational databases.
/// </summary>
/// <typeparam name="P">Execution parameter type in relational database.</typeparam>
/// <typeparam name="T">Numeric structure type.</typeparam>
#pragma warning disable CS8605 // Unboxing a possibly null value. 
public static class Parameter<P, T>
    where P : DbParameter
    where T : struct
{
    /// <summary>
    /// Converts received value in execution parameter in relational database.
    /// </summary>
    /// <param name="par">Execution parameter type in relational database.</param>
    /// <returns>Converted value.</returns>
    public static T? GetValue(P par)
    {
        T? nulo = null;

        return Convert.IsDBNull(par.Value) ? nulo : (T)par.Value;
    }

    /// <summary>
    /// Converte valor recebido em parâmetro de execução em banco de dados relacional.
    /// </summary>
    /// <param name="par">Execution parameter type in relational database.</param>
    /// <param name="defaultValue">Default value in case of null parameter value.</param>
    /// <returns>Converted value or initialized default value.</returns>
    public static T GetValue(P par, T defaultValue)
    {
        return Convert.IsDBNull(par.Value) ? defaultValue : (T)par.Value;
    }
#pragma warning restore CS8605 // Unboxing a possibly null value.
}