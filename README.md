# MikesEshop 

MikesEshop is built for scalability and future microservices migration, using:
- Vertical slices and Clean Architecture
- Domain-Driven Design (DDD)
- CQRS

### Technologies
* ASP.NET Core
* Entity Framework Core (ORM)
* MSSQL Server (database)
* Wolverine (endpoints and message bus)
* Swagger (API documentation)
* xUnit (unit testing)
* NSubstitute (mocking in tests)
* Bogus (data seeding)
* Docker and Docker Compose

### Prerequisites
- .NET 9 SDK
- IDE (e.g. Rider or Visual Studio)
- MSSQL Server
- or just Docker and Docker Compose ;)

## Running the project
### Running the project manually with IDE
Open the solution in your IDE and add database connection string to user secrets in this format that is also defined in `appsettings.json`:
```json
{
    "ConnectionStrings": {
      "EshopProductsDb": ""
    }
}
```
_Note: Make sure your SQL Server is running and the connection string is correct._

Then, run `MikesEshop.Host` project using predefined launch settings - this is the entry point of the application,
and you can visit API documentation on http://localhost:5041/swagger (when using http launch configuration).

### Running the project using Docker
When running the project using Docker, both application and sql server will be started. You can use the provided `docker-compose.yml` file using the following command in the root directory of the solution:
```shell
  docker compose up
```

Project will run on the following URL: [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html)

### Seeding Database
Both using predefined launch settings in your IDE or using Docker Compose will seed the database with some test data (**ASPNETCORE_ENVIRONMENT** set to `Development` causes seeding to start). 

If table is already filled with data, it will be skipped.

### Running unit tests
To run the unit tests, you can use your IDE (VS or Rider) or the command line in root directory of the solution:
```shell
  dotnet test MikesEshop.Products.UnitTests/MikesEshop.Products.UnitTests.csproj
```