using Microsoft.EntityFrameworkCore;
using BankApplication;
using BankInfraestructure;
using BankInfraestructure.Context;
using BankInfraestructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<LoadBanksUseCase>();
builder.Services.AddScoped<GetAllBanksUseCase>();
builder.Services.AddScoped<GetBanksByIdUseCase>();
builder.Services.AddScoped<IExternalBankRepository, FakeBankAPI>();
builder.Services.AddSingleton<IInternalBankRepository, InMemoryBanksDataBase>();

var connectionString = builder.Configuration.GetConnectionString("MyAppCs");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
//builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("ProductsSqlServer")));


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
