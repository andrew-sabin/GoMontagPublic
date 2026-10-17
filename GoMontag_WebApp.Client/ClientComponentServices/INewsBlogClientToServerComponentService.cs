using GoMontag_Shared.Models.NewsBlog;
using GoMontag_Shared.Models.NewsUpdate;

namespace GoMontag_WebApp.Client.ClientComponentServices
{
    public interface INewsBlogClientToServerComponentService
    {
        event Action? OnChange;
        List<NewsBlogResponse> NewsBlogs { get; set; }
        Task<List<NewsBlogResponse>> GetNewsBlogs();
        Task<NewsBlogResponse?> GetArticleBySlug(string slug);
        Task<NewsBlogResponse?> GetFirstTypeArticle(string type);
        Task<List<NewsBlogResponse>> GetLatestUpdates(string type, int amount, bool sticky);
        Task<List<NewsBlogResponse>> GetLatestStickies(int amount, bool sticky);
        Task CreateArticle (NewsBlogRequest request);
        Task UpdateArticle (int id, NewsBlogRequest article);
        Task DeleteArticle (int id);
    }
}
