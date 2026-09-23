using System.Net.Http.Json;
using mcp.Entities;
using mcp.Responses;

namespace mcp.Services
{
    public class LeadsService
    {
        private readonly HttpClient _httpClient;

        public LeadsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> LeadsCountAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseModel<List<Lead>>>("/Leads");

            if (response!.Success is false)
            {
                throw new InvalidOperationException(response!.Message);
            }

            var leadsCount = response.Data!.Count;

            return leadsCount;
        }
    }
}
