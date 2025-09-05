# AdoNet.Fluent
Base classes and interfaces for ADO.Net fluent notation.

## Purpose
This library offers another abstraction layer to Ado.Net, as [LINQ](https://learn.microsoft.com/en-us/dotnet/standard/linq/) does. It is aimed to deliver a higher performance than [Entity Framework](https://learn.microsoft.com/en-us/ef/ef6/) because combines the ease of fluent notation with direct call to [ADO.Net](https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/) classes.

As pure ADO.Net, the AdoNet.Fluent implementations demands a knowledge of database to be used, including SQL commands. The benefit is a code easier to be developed, suitable to be used with AI helpers such as GitHub Copilot, in order to achieve a godd development productivity along with better performance.

AdoNet.Fluent fits in development of containerized microservices that use relational databases and requires high throughput.