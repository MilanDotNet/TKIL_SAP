using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKILSAPRFC.Service.Services
{
    public class MyHttpClientService
    {
        private readonly HttpClient _httpClient;

        public MyHttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromMinutes(20); // Set timeout
        }

        public async Task<string> GetPOLayoutAsync(string sapServiceUrl, string poNo)
        {
            var uri = new Uri($"{sapServiceUrl}/GetPOLayout?PONo={poNo}");
            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                // Handle non-success status codes
                response.EnsureSuccessStatusCode();
                return null; // or throw an exception
            }
        }
    }
}
