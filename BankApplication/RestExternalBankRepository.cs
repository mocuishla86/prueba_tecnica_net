using BankDomain;
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
        private readonly HttpClient _httpClient;

        public RestExternalBankRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Bank>> GetAllBanks()
        {
            var url = "https://api.opendata.esett.com/EXP06/Banks";

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
                Console.WriteLine($"Error al obtener los bancos desde la API: {ex.Message}");
                return new List<Bank>();
                ///DEVOLVER UN 500, VER TEMA DE EXCEPCIONES
            }
        }
        
    }
}
