using BankDomain;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace BankApplication
{
    public class RestExternalBankRepository : IExternalBankRepository
    {
        private readonly IOptions<ExternalAppOptions> _options;
        private readonly HttpClient _httpClient;

        public RestExternalBankRepository(IOptions<ExternalAppOptions> options, HttpClient httpClient)
        {
            _options = options;
            _httpClient = httpClient;
        }

        public async Task<List<Bank>> GetAllBanks()
        {
            var url = _options.Value.Url;

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();

                var banks = JsonConvert.DeserializeObject<List<Bank>>(jsonResponse);

                return banks;
            }

            catch (Exception ex)
            {
                throw new HttpRequestException("Failed to fetch banks from external API", ex);
            }
        }
        
    }
}
