
using BankDomain;
using FluentAssertions;
using System.Net.Http.Json;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace BankAPITest
{
    public class BankFunctionalTest : IClassFixture<DatabaseFixture>
    {
        private readonly HttpClient client;

        public BankFunctionalTest(DatabaseFixture databaseFixture)
        {
            client = databaseFixture.Factory.CreateClient();
            databaseFixture.WireMock
                 .Given(
                    Request.Create().WithPath("/banks").UsingGet())
                  .RespondWith(
                    Response.Create()
                      .WithStatusCode(200)
                      .WithHeader("Content-Type", "application/json")
                      .WithBody(@"
                                        [
                                            {
                                            name: ""Sparebank 1 SMN"",
                                            bic: ""SPTRNO22"",
                                            country: ""NO""
                                            },
                                            {
                                            name: ""Handelsbanken Suomi"",
                                            bic: ""HANDFIHH"",
                                            country: ""FI""
                                            }
                                        ]   
                                    ")
                  );
        }

        [Fact]
        public async Task BeforeLoadingBanksTheDatabaseIsEmpty()
        {
            var banks = await client.GetFromJsonAsync<List<Bank>>("/banks");

            banks.Should().BeEmpty();
        }

        [Fact]
        public async Task BanksAreLoadedFromExternalApi()
        {
            await client.PostAsync("/banks/LoadBanksFromAPI", null);
            var banks = await client.GetFromJsonAsync<List<Bank>>("/banks");

            banks.Should().HaveCount(2);
        }

        [Fact]
        public async Task WhenReloadingBanksFromExternalApiThenPreviousOnesAreRemoved()
        {
            await client.PostAsync("/banks/LoadBanksFromAPI", null);
            await client.PostAsync("/banks/LoadBanksFromAPI", null);
            var banks = await client.GetFromJsonAsync<List<Bank>>("/banks");

            banks.Should().HaveCount(2);
        }
    }
}