# AdoNet.Fluent
Base classes and interfaces for ADO.Net [fluent interface](https://en.wikipedia.org/wiki/Fluent_interface).

## Purpose
This library offers another abstraction layer to Ado.Net, as [LINQ](https://learn.microsoft.com/en-us/dotnet/standard/linq/) does. It is aimed to deliver a higher performance than [Entity Framework](https://learn.microsoft.com/en-us/ef/ef6/) because combines the ease of fluent notation with direct call to [ADO.Net](https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/) classes.

As pure ADO.Net, the AdoNet.Fluent implementations demands a knowledge of database to be used, including SQL commands. The benefit is a code easier to be developed, suitable to be used with AI helpers such as GitHub Copilot, in order to achieve a godd development productivity along with better performance.

AdoNet.Fluent fits in development of containerized microservices that use relational databases and requires high throughput.

## Available Classes

- **DataObject**  
  Encapsulates ADO.Net functionality, abstracting complexities such as open and close connections, using Fluent Notation.

- **DataObjectBuilder**  
  Bulider for `DataObject` instances using Fluent Notation.

- **IDataObject**  
  Interface that defines the contract for data objects, including methods for set commands, parameters and execute operations of [DML](https://en.wikipedia.org/wiki/Data_manipulation_language) and [Select](https://en.wikipedia.org/wiki/Select_(SQL)).

- **IDataObjectBuilder**  
  Interface that specifies the contract for `DataObject` builders, supporting fluent configuration and mapping.

- **ITrnsaction**  
  Interface that specifies the contract of `DataObject` builders for [transaction](https://en.wikipedia.org/wiki/Database_transaction) operations, supporting fluent configuration and mapping.

## Getting Started

AdoNet.Fluent is a core library for implementations targeting DBMSs such as [SQL Server](https://github.com/marcoshidalgonunes/AdoNet.Fluent.SqlServer). It is installed as a dependency of these implementations, but if you want to develop your own AdoNet.Fluent implementation, feel free to use it as a base for this development. Just install it via NuGet:

```script
dotnet add package AdoNet.Fluent
```
