using AngleSharp.Dom;
using Blazorise;
using GoMontag.Shared.Entities.Extras;
using GoMontag.Shared.Models.FormatRevision;
using GoMontag_Shared.Entities.Revisions;
using GoMontag_WebApp.Repositories.Interfaces;
using Mapster;
using Sqids;
using System.Text.RegularExpressions;

namespace GoMontag_WebApp.Services
{
    public class FormatRevisionService : IFormatRevisionService
    {
        private readonly IFormatRevisionRepository _formatRevisionRepo;
        private readonly IFormatImagesRepository _formatImagesRepository;
        public FormatRevisionService(IFormatRevisionRepository formatRevisionRepository, IFormatImagesRepository formatImagesRepository)
        {
            _formatRevisionRepo = formatRevisionRepository;
            _formatImagesRepository = formatImagesRepository;
        }

        public async Task<FormatRevisionResponse> CreateNewFormatRevisionAsync(FormatRevisionCreateRequest formatRevisionCreateRequest)
        {
           
            var newEntry = formatRevisionCreateRequest.Adapt<FormatRevision>();
            newEntry.Description = await AddFormatImageLinks(newEntry.Description, newEntry.FormatId, newEntry.ModifierName);
            var result = await _formatRevisionRepo.CreateNewFormatRevisionAsync(newEntry);
            return result.Adapt<FormatRevisionResponse>();
        }

        public async Task<List<FormatRevisionResponse>?> DeleteFormatRevisionAsync(int formatRevisionId)
        {
            var result = await _formatRevisionRepo.DeleteFormatRevisionAsync(formatRevisionId);
            if (result == null)
                return null;

            return result.Adapt<List<FormatRevisionResponse>>();
        }

        public async Task<FormatRevisionResponse?> GetFormatRevisionByIdAsync(int formatRevisionId)
        {
            var result = await _formatRevisionRepo.GetFormatRevisionByIdAsync(formatRevisionId);
            if (result is null)
                return null;

            return result.Adapt<FormatRevisionResponse>();
        }

        public async Task<List<FormatRevisionResponse>> GetFormatRevisionsAsync()
        {
            var result = await _formatRevisionRepo.GetAllFormatRevisions();
            return result.Adapt<List<FormatRevisionResponse>>();
        }

        public async Task<List<FormatRevisionResponse>> GetFormatRevisionsByFormatId(int formatId)
        {
            var result = await _formatRevisionRepo.GetFormatRevisionsByFormatId(formatId);
            return result.Adapt<List<FormatRevisionResponse>>();
        }

        public async Task<FormatRevisionResponse?> GetLatestFormatRevisionByFormatId(int formatId)
        {
            var result = await _formatRevisionRepo.GetMostRecentRevisionByFormatId(formatId);
            if (result is null)
                return null;

            return result.Adapt<FormatRevisionResponse>();
        }

        public async Task<FormatRevisionResponse?> GetMostRecentFormatRevisionForEditingAsync(int formatId)
        {
            var result = await _formatRevisionRepo.GetMostRecentRevisionByFormatId(formatId);
            if (result is null)
                return null;

            if (!string.IsNullOrEmpty(result.Description))
            {
                //result.Description = GetHeightAndWidthBack(result.Description);
            }

            return result.Adapt<FormatRevisionResponse>();
        }

        private string CreateIDHash(int Id)
        {
            var sqids = new SqidsEncoder<int>(new()
            {
                MinLength = 0,
                Alphabet = "",
            });
            var hash = sqids.Encode(Id);
            return hash;
        }

        private string CreateImageIDHash(int Id)
        {
            var sqids = new SqidsEncoder<int>(new()
            {
                MinLength = 0,
                Alphabet = "",
            });
            var hash = sqids.Encode(Id);
            return hash;
        }

        private async Task<string> AddFormatImageLinks(string html, int formatId, string userName)
        {
            if (string.IsNullOrWhiteSpace(html)) return html;
            var imgPattern = @"<img\s([^>]*)>";

            var srcUrls = new HashSet<string>();
            var altTexts = new List <string>();
            var widths = new List<int>();
            
            var matches = Regex.Matches(html, imgPattern);

            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                var attributes = match.Groups[1].Value;
                var srcMatch = Regex.Match(attributes, @"src\s*=\s*[""']?([^""'>]+)[""']?");
                if (srcMatch.Success)
                {
                    srcUrls.Add(srcMatch.Groups[1].Value);
                    var altMatch = Regex.Match(attributes, @"alt\s*=\s*(?:""([^""]*)""|'([^']*)')");
                    if (altMatch.Success)
                    {
                        altTexts.Add(altMatch.Groups[1].Value);
                    }
                    else
                    {
                        altTexts.Add("");
                    }
                    var widthMatch = Regex.Match(attributes, @"width\s*=\s*[""']?([^""'\s>]+)[""']");
                    if (widthMatch.Success) 
                    { 
                        widths.Add(Int32.Parse(widthMatch.Groups[1].Value));
                    }
                    else
                    {
                        widths.Add(0);
                    }
                }
            }

            var formatImageCache = new Dictionary<string, FormatImage>();
            var altnum = 0;
            var widthNum = 0;


            foreach (var src in srcUrls)
            {
                var alt = altTexts[altnum];
                var width = widths[widthNum];
                var formatImage = await _formatImagesRepository.GetFormatImageByImageUrl(src);
                if (formatImage != null)
                {
                    if (alt != formatImage.AltText || width != formatImage.Width)
                    {
                        var formatImageUpdateReq = new FormatImageUpdateRequest
                        {
                            UpdatedAltText = alt,
                            Width = width
                        };
                        var updatedFormatImage = await _formatImagesRepository.UpdateFormatImage(formatImage.Id, formatImageUpdateReq.Adapt<FormatImage>());
                        formatImageCache[src] = updatedFormatImage!;
                    }
                    else
                    {
                        formatImageCache[src] = formatImage;
                    }
                    
                }
                else
                {
                    var imageName = Regex.Match(src, @"([\w.\d*\s*-]+)(?!.*[\\/])");
                    var ImageName = imageName.Groups[1].Value;
                    var newFormatImageRequest = new FormatImageCreateRequest
                    {
                        ImageName = ImageName,
                        ImageUrl = src,
                        AltText = alt,
                        Width = width,
                        FormatId = formatId
                    };
                    var newFormatImage = await _formatImagesRepository.CreateFormatImage(newFormatImageRequest.Adapt<FormatImage>());
                    formatImageCache[src] = newFormatImage;
                }
                altnum++;
                widthNum++;
            }

            var result = Regex.Replace(html, imgPattern, match =>
            {
                var attributes = match.Groups[1].Value;
                var srcMatch = Regex.Match(attributes, @"src\s*=\s*[""']?([^""'>]+)[""']?");
                var src = srcMatch.Success ? srcMatch.Groups[1].Value : "";
                var asrc = "";
                var hasKey = formatImageCache.ContainsKey(src);
                if (hasKey == true)
                {
                    var imghash = CreateImageIDHash(formatImageCache[src].Id);
                    asrc = $"/formats/{CreateIDHash(formatImageCache[src].FormatId)}/images/{imghash}";
                    return $@"<a href=""{asrc}"" target=""_blank""><img {attributes}></a>";
                }
                return $@"<img {attributes}>";
            });
            return result;
        }

        /* 
         Learning Regex:
        Video: https://www.youtube.com/watch?v=rhzKDrUiJVk
        Microsoft Documentation: https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference#character-escapes
         */

        /// <summary>
        /// Wraps all of the iframe elements with a responsive container named iframe-container
        /// </summary>
        /// <param name="htmlContent"></param>
        /// <returns></returns>

        private string WrapIframesWithContainer(string htmlContent)
        {
            if (string.IsNullOrEmpty(htmlContent))
            {
                return htmlContent;
            }
            // Any space between <iframe where there is 0 or more > whether that would be in the beginning of a paragraph or otherwise
            var iframePattern = @"<iframe\s([^>]*)>";
            var replacement = @"<div class=""iframe-container""><iframe $1>";

            htmlContent = Regex.Replace(htmlContent, iframePattern, replacement, RegexOptions.IgnoreCase);

            htmlContent = Regex.Replace(htmlContent, @"</iframe>", @"</iframe></div>", RegexOptions.IgnoreCase);

            htmlContent = NormalizeIframeAttributes(htmlContent);

            return htmlContent;
        }

        /// <summary>
        /// Removes any values of style, width, or height within a <iframe> tag.
        /// </summary>
        /// <param name="htmlContent"></param>
        /// <returns></returns>
        private string NormalizeIframeAttributes(string htmlContent)
        {
            // Any space between <iframe where there is 0 or more > whether that would be in the beginning of a paragraph or otherwise
            string pattern = @"<iframe\s+([^>]*)>";
            return Regex.Replace(htmlContent, pattern, match =>
            {
                string tagContent = match.Groups[1].Value;
                //Any space betwen style= and anygroup or range between "" and replace it with an empty string
                tagContent = Regex.Replace(tagContent, @"\s*style=""[^""]*""", "", RegexOptions.IgnoreCase);
                //Any space betwen width= and anygroup or range between "" and replace it with an empty string
                tagContent = Regex.Replace(tagContent, @"\s*width=""[^""]*""", "", RegexOptions.IgnoreCase);
                //Any space betwen height= and anygroup or range between "" and replace it with an empty string
                tagContent = Regex.Replace(tagContent, @"\s*height=""[^""]*""", "", RegexOptions.IgnoreCase);

                return $@"<iframe width=""100%"" height=""315px"" style=""max-width:560px; max-height:315px;""{tagContent}>";
            }, RegexOptions.IgnoreCase);
        }

        
    }
}
