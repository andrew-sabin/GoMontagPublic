using GoMontag_Shared.Entities;
using GoMontag_Shared.Models.NewsBlog;
using GoMontag_Shared.Models.NewsUpdate;

namespace GoMontag_WebApp.Services.Interface
{
    public interface INewsBlogRepository
    {
        Task<List<NewsBlog>> GetAllNewsBlogs();
        Task<NewsBlog?> GetNewsBlogByIdAsync(int id);
        Task<NewsBlog?> GetNewsBlogBySlugAsync(string slug);
        Task<NewsBlog?> GetFirstBlogByType(string type);
        Task<List<NewsBlog>> GetLastestBlogsByType(string type, int amnt, bool sticky);
        Task<List<NewsBlog>> GetLatestStickyBlogs(int amnt, bool sticky);
        Task<NewsBlog> CreateNewsBlogAsync(NewsBlog article);
        Task<NewsBlog?> UpdateNewsBlogAsync(int id, NewsBlog article);
        Task<List<NewsBlog>?> DeleteNewsBlogAsync(int id);

    }
}
