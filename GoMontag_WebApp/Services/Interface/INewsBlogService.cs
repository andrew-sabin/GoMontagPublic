using GoMontag_Shared.Entities;
using GoMontag_Shared.Models.NewsBlog;
using GoMontag_Shared.Models.NewsUpdate;

namespace GoMontag_WebApp.Services.Interface
{
    public interface INewsBlogService
    {
        Task<List<NewsBlogResponse>> GetAllNewsBlogs();
        Task<NewsBlogResponse?> GetNewsBlogByIdAsync(int id);
        Task<NewsBlogResponse?> GetNewsBlogBySlugAsync(string slug);
        Task<NewsBlogResponse?> GetFirstBlogByType(string type);
        Task<List<NewsBlogResponse>?> GetLastestBlogsByType(string type, int amnt, bool sticky);
        Task<List<NewsBlogResponse>?> GetLatestStickyBlogs(int amnt, bool sticky);
        Task<NewsBlogResponse> CreateNewsBlogAsync(NewsBlogCreateRequest article);
        Task<NewsBlogResponse?> UpdateNewsBlogAsync(int id, NewsBlogUpdateRequest article);
        Task<List<NewsBlogResponse>?> DeleteFormatAsync(int id);

    }
}
