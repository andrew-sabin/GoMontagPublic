using System;
using System.Collections.Generic;
using System.Text;

namespace GoMontag_Shared.Models.NewsUpdate
{
    public record struct NewsBlogResponse
        (int Id, string Title, string? ArticleType, string slug, string? CoverImageUrl, bool StickToFrontPage, string AuthorId, DateTime CreatedDate, string ModifierId, DateTime ModifiedDate, string NewsArticle);
}
