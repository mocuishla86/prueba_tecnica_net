using Microsoft.EntityFrameworkCore;
using BankApplication;
using BankInfraestructure;
using BankInfraestructure.Context;
using BankInfraestructure.Repositories;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ExternalAppOptions>(builder.Configuration.GetSection("ExternalApi"));

builder.Services.AddScoped<LoadBanksUseCase>();
builder.Services.AddScoped<GetAllBanksUseCase>();
builder.Services.AddScoped<GetBanksByIdUseCase>();
builder.Services.AddScoped<IExternalBankRepository, RestExternalBankRepository>();
builder.Services.AddScoped<IInternalBankRepository, SQLServerBankRepository>();

builder.Services.AddHttpClient<IExternalBankRepository, RestExternalBankRepository>(client =>
{
    client.BaseAddress = new Uri("https://api.opendata.esett.com");
});

if (!"Test".Equals(builder.Environment.EnvironmentName))
{
    var connectionString = builder.Configuration.GetConnectionString("MyAppCs");
   // builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("BankInfraestructure")));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
