using BankAPI.Controllers;
using BankApplication;
using BankDomain;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net.Http.Json;

namespace BankAPITest
{
    public class BankFunctionalTest : IClassFixture<DatabaseFixture>
    {
        private readonly HttpClient client;

        public BankFunctionalTest(DatabaseFixture databaseFixture)
        {
            client = databaseFixture.Factory.CreateClient();
        }

        [Fact]
        public async Task BeforeLoadingBanksTheDatabaseIsEmpty()
        {
            var banks = await client.GetFromJsonAsync<List<Bank>>("/banks");

            banks.Should().BeEmpty();
        }
    }
}