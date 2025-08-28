using System.Data;

namespace AdoNet.Fluent;

/// <summary>
/// Connection modes.
/// </summary>
[Flags]
public enum ConnectionMode
{
    /// <summary>
    /// Connection normal, without transactions or MARS
    /// </summary>
    Normal,
    /// <summary>
    /// Connection using transaction.
    /// </summary>
    Transational,
    /// <summary>
    /// Connection with multiple active result set (MARS) support.
    /// </summary>
    MultipleResultsets
}

/// <summary>
/// Referencial integrity violation types.
/// </summary>
public enum ConstraintError
{
    /// <summary>
    /// No integrity violation
    /// </summary>
    None,
    /// <summary>
    /// Primary key violation
    /// </summary>
    PrimaryKey,
    /// <summary>
    /// Foreign key violation.
    /// </summary>
    ForeignKey,
    /// <summary>
    /// Duplicate key violation.
    /// </summary>
    DuplicateKey
}

/// <summary>
/// Numeric types to set execution parameters.
/// </summary>
public enum NumericType
{
    /// <summary>
    /// Nones.
    /// </summary>
    None = 0,
    /// <summary>
    /// Boolean representation.
    /// </summary>
    Boolean = DbType.Boolean,
    /// <summary>
    /// 8-bit integer representation (byte).
    /// </summary>
    Byte = DbType.Byte,
    /// <summary>
    /// Date/time representation.
    /// </summary>
    DateTime = DbType.DateTime,
    /// <summary>
    /// Floating point representation with 15-16 digits precision.
    /// </summary>
    Double = DbType.Double,
    /// <summary>
    /// 16-bit integer representation (single).
    /// </summary>
    Int16 = DbType.Int16,
    /// <summary>
    /// 32-bit integer representation (int).
    /// </summary>
    Int32 = DbType.Int32,
    /// <summary>
    /// 64-bit integer representation (long).
    /// </summary>
    Int64 = DbType.Int64,
    /// <summary>
    /// Floating point representation with 7 digits precision.
    /// </summary>
    Single = DbType.Single
}
