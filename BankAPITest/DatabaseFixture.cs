using BankApplication;
using BankInfraestructure.Context;
using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace BankAPITest
{
    public class DatabaseFixture : IDisposable
    {
        private const string Database = "master";
        private const string Username = "sa";
        private const string Password = "$trongPassword";
        private const ushort MsSqlPort = 1433;

        private readonly DotNet.Testcontainers.Containers.IContainer _container;

        public WebApplicationFactory<Program> Factory { private set; get; }

        public WireMockServer WireMock { private set; get; }

        public DatabaseFixture()
        {
            WireMock = WireMockServer.Start();
            String fakeExternalApiUrl = WireMock.Url + "/banks";
            _container = CreateContainer();
            Factory = CreateAppWithReplacedConnectionString(_container.Hostname, _container.GetMappedPublicPort(MsSqlPort), fakeExternalApiUrl);
            RunMigrations();
        }

        private IServiceScope RunMigrations()
        {
            using var scope = Factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
            return scope;
        }

        private WebApplicationFactory<Program> CreateAppWithReplacedConnectionString(string host, ushort port, string fakeExternalApiUrl)
        {
            var connectionString = $"Server={host},{port};Database={Database};User Id={Username};Password={Password};TrustServerCertificate=True";
            return new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Test");
                    builder.ConfigureServices(services =>
                    {
                        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("BankInfraestructure")));
                        services.Configure<ExternalAppOptions>(opts =>
                        {
                            opts.Url = fakeExternalApiUrl;
                        });
                    });
                });
        }

        private DotNet.Testcontainers.Containers.IContainer CreateContainer()
        {
            var container = new ContainerBuilder()
                            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                            .WithPortBinding(MsSqlPort, true)
                            .WithEnvironment("ACCEPT_EULA", "Y")
                            .WithEnvironment("SQLCMDUSER", Username)
                            .WithEnvironment("SQLCMDPASSWORD", Password)
                            .WithEnvironment("MSSQL_SA_PASSWORD", Password)
                            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(MsSqlPort))
                            .Build();

            //Start Container
            Task.Run(async () => await container.StartAsync()).Wait();
            return container;
        }

        public void Dispose()
        {
            Task.Run(async () => await _container.StopAsync()).Wait();
            Task.Run(async () => await _container.DisposeAsync()).Wait();

            Factory.Dispose();
        }
    }
}
