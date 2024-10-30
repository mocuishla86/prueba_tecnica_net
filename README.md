# prueba_tecnica_net
Prueba tecnica 
Realizar un programa en .Net - C# 
1. Crear: clase, función que consuma la siguiente API. puede escoger cualquier servicio.
- https://api.opendata.esett.com/
2. Almacenar esta inforamción en la base de datos
3. realizar un controlador que filter por Primary Key
4. Construir una api Rest con swagger que permita visualizar los datos almacenados en la base de datos.
 -------------------------------
 ### Architecture: Hexagonal:
* One project for API (starts the application and wires dependencies)
* One project for domain (Project class)
* One project for application layer (use cases)
* One project for persistence (spefic adapter for output port).
# How to run
* Docker must be installed.
### Start Sql Server container
```shell
docker-compose up -d
```
### From BankAPI root
```shell
dotnet run
```

### Stop Sql container
```shell
docker-compose down
```

# Migrations
## How to add migrations

From root 
```shell
dotnet ef migrations add Initial --project BankInfraestructure/BankInfraestructure.csproj --startup-project BankAPI/BankAPI.csproj --context BankInfraestructure.Context.AppDbContext
```

## How to apply all migrations
```shell
dotnet ef migrations update --project BankInfraestructure/BankInfraestructure.csproj --startup-project BankAPI/BankAPI.csproj --context BankInfraestructure.Context.AppDbContext
```

## How to remove LAST migration
```shell
dotnet ef migrations remove --project BankInfraestructure/BankInfraestructure.csproj --startup-project BankAPI/BankAPI.csproj --context BankInfraestructure.Context.AppDbContext
```

# Links

- Read configuration from file: https://blog.markvincze.com/overriding-configuration-in-asp-net-core-integration-tests/
- Wiremock example: https://blog.markvincze.com/overriding-configuration-in-asp-net-core-integration-tests/