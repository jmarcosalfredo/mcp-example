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

        public async Task<(bool Success, string Content)> CreateLeadAsync(Lead lead)
        {
            var response = await _httpClient.PostAsJsonAsync("/Leads", lead);
            var content = await response.Content.ReadAsStringAsync();

            return (response.IsSuccessStatusCode, content);
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
