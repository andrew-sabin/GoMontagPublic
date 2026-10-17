using GoMontag.Shared.Entities;
using GoMontag.Shared.Models.Category;
using GoMontag.Shared.Models.Format;
using System.Net.Http.Json;

namespace GoMontag_WebApp.Client.ClientComponentServices
{
    public class CategoryClientToServerComponentService : ICategoryClientToServerComponentService
    {
        private readonly HttpClient _httpClient;
        public List<CategoryResponse> ProductCategories { get; set; } = new List<CategoryResponse>();

        public event Action? OnChange;

        public CategoryClientToServerComponentService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ComponentApi");
        }

        public Task CreateCategory(CategoryRequest Category)
        {
            var createRequest = new CategoryCreateRequest
            {
                Name = Category.Name,
                Description = Category.Description,
                CreatorsTitle = Category.CreatorsTitle,
                FormatsTitle = Category.FormatsTitle,
                TypeTitle = Category.TypeTitle,
                EditionTitle = Category.EditionTitle

            };
            return _httpClient.PostAsJsonAsync("api/Category", createRequest);
        }

        public Task<List<CategoryResponse>?> DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryResponse?> GetCategoryById(int id)
        {
            var response = await _httpClient.GetAsync($"api/Category/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            return await _httpClient.GetFromJsonAsync<CategoryResponse>($"api/Category/{id}");
        }

        public async Task UpdateCategoryByID(int id, CategoryRequest Category)
        {
            var updateRequest = new CategoryRequest
            {
                Name = Category.Name,
                Slug = Category.Slug,
                Description = Category.Description,
                CreatorsTitle = Category.CreatorsTitle,
                FormatsTitle = Category.FormatsTitle,
                TypeTitle = Category.TypeTitle,
                EditionTitle = Category.EditionTitle
            };
            await _httpClient.PutAsJsonAsync($"api/Category/{id}", updateRequest);
        }

        public async Task LoadAllProductCategories()
        {
            var result = await _httpClient.GetFromJsonAsync<List<CategoryResponse>>("api/Category");
            if(result is not null)
            {
                ProductCategories = result;
                OnChange?.Invoke();
            }
            else
            {
                ProductCategories = new List<CategoryResponse>();
            }
        }

        public async Task<CategoryResponse?> GetCategoryBySlug(string slug)
        {
            var response = await _httpClient.GetAsync($"api/Category/slug/{slug}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            return await _httpClient.GetFromJsonAsync<CategoryResponse>($"api/Category/slug/{slug}");
        }

        public Task<List<CategoryResponse>?> UpdateCategoryBySlug(string slug, CategoryRequest Category)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryResponse>> GetAllProductCategories()
        {
            var results = await _httpClient.GetFromJsonAsync<List<CategoryResponse>>("api/Category");
            if(results is null)
            {
              return new List<CategoryResponse>();
            }
            return results;
        }

        public async Task LockCategory(int id, CategoryRequest Category)
        {
            var lockRequest = new CategoryLockRequest
            {
                IsLocked = Category.IsLocked,
                lockedReason = Category.lockedReason
            };
            await _httpClient.PatchAsJsonAsync($"api/Category/{id}/lock/", lockRequest);
        }

        public async Task<List<CategoryResponse>> GetAllUnlockedProductCategories()
        {
            var results = await _httpClient.GetFromJsonAsync<List<CategoryResponse>>("api/Category");
            if (results is null)
            {
                return new List<CategoryResponse>();
            }
            var unlockedCata = results.Where(c => !c.IsLocked).ToList();
            return results;
        }

        public async Task LoadAllUnlockedProductCategories()
        {
            var result = await _httpClient.GetFromJsonAsync<List<CategoryResponse>>("api/Category/unlocked");
            if (result is not null)
            {
                ProductCategories = result;
                OnChange?.Invoke();
            }
            else
            {
                ProductCategories = new List<CategoryResponse>();
            }
        }
    }
}
