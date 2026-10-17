using GoMontag_Shared.Models.FormatImage;
using GoMontag_WebApp.Client.ClientComponentServices.Interface;
using System.Net.Http.Json;

namespace GoMontag_WebApp.Client.ClientComponentServices
{
    public class FormatImageClientToServerComponentService : IFormatImageClientToServerComponentService
    {
        private readonly HttpClient _httpClient;

        public FormatImageClientToServerComponentService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ComponentApi");
        }

        public List<FormatImageResponse> FormatImages { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action? OnChange;

        public Task<int> CountAllFormatImages()
        {
            throw new NotImplementedException();
        }

        public Task<FormatImageResponse> CreateFormatImage()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteFormatImage(string hash)
        {
            Console.WriteLine($"Deleting Image with Id {hash}");
            await _httpClient.DeleteAsync($"api/formatimage/{hash}");
        }

        public async Task<FormatImageResponse> GetFormatImageById(string hash)
        {
            
            var response = await _httpClient.GetAsync($"api/formatimage/{hash}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<FormatImageResponse>();
                if (data != null)
                    return data;
                else
                    return null;
            }
            else
            {
                return (null);
            }

        }

        public Task<List<FormatImageResponse>> GetAllFormatImages()
        {
            throw new NotImplementedException();
        }

        public async Task<List<FormatImageResponse>> GetAllFormatImagesByFormatId(string hashedFormatId)
        {
            return await _httpClient.GetFromJsonAsync<List<FormatImageResponse>>($"/api/FormatImage/format/{hashedFormatId}");
        }

        public async Task<FormatImageResponse> GetFormatImageByUrl(string url)
        {
            var response = await _httpClient.GetAsync($"api/formatimage/url/{url}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<FormatImageResponse>();
                if (data != null)
                    return data;
                else
                    return null;
            }
            else
            {
                return (null);
            }
        }
    }
}
