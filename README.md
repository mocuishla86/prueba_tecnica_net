# prueba_tecnica_net
Prueba tecnica 
Realizar un programa en .Net - C# 
1. Crear: clase, función que consuma la siguiente API. puede escoger cualquier servicio.
- https://api.opendata.esett.com/
2. Almacenar esta inforamción en la base de datos
3. realizar un controlador que filter por Primary Key
4. Construir una api Rest con swagger que permita visualizar los datos almacenados en la base de datos.

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