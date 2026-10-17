using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace GoMontag_WebApp.Client.Helpers
{
    public class HeaderProcessor
    {

        private readonly string _entityName;
        private readonly int[] _counts;

        public HeaderProcessor(string entityName)
        {
            _entityName = SanitizeEntityName(entityName);
            _counts = new int[4];
        }
        /// <summary>
        /// Processes the HTML Document to look for different header tags and create different ids for each header 
        /// based on different header levels.
        /// </summary>
        /// <param name="htmlContent"></param>
        /// <returns></returns>
        public string ProcessHtml(string htmlContent)
        {
            Array.Clear(_counts, 0, _counts.Length);

            var doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);

            var headers = doc.DocumentNode.SelectNodes("//h1|//h2|//h3|//h4");

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    int level = int.Parse(header.Name.Substring(1));

                    for (int itr = level; itr < _counts.Length; itr++)
                    {
                        _counts[itr] = 0;
                    }

                    _counts[level - 1]++;

                    string id = BuildId(level);

                    header.SetAttributeValue("id", id);
                    header.AddClass("toc-header");
                }
            }

            return doc.DocumentNode.OuterHtml;
        }
        /// <summary>
        /// Creates the Id based on the level and the current amount per each level.
        /// If a header is level 2 it will add the current counts of level 1 and level 2 after
        /// the _entityName.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private string BuildId(int level)
        {
            var idParts = new List<string> { _entityName };


            for (int jitr = 0; jitr < level; jitr++)
            {
                idParts.Add(_counts[jitr].ToString());
                Console.WriteLine($"Counts level {jitr + 1} = {_counts[jitr]}");
            }

            return string.Join("-", idParts);
        }


        /// <summary>
        /// Changes the entity name value to be all lowercase and makes sure to replace any letters and number values with "-".
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        private string SanitizeEntityName(string entityName)
        {
            return Regex.Replace(entityName.ToLower().Trim(), @"[^a-z0-9-]", "-");
        }
    }
}
