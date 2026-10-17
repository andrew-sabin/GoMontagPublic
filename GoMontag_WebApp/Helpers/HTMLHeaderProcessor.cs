using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace GoMontag_WebApp.Client.Helpers
{
    public class HTMLHeaderProcessor
    {
        private class HeaderNode
        {
            public int Level { get; set; }
            public int Count { get; set; }
            public HeaderNode? Parent { get; set; }
            public List<HeaderNode> Children { get; set; } = new();
        }

        private readonly string _entityName;
        private HeaderNode? _root;
        private HeaderNode? _currentNode;

        public HTMLHeaderProcessor(string entityName)
        {
            _entityName = SanitizeEntityName(entityName);
        }

        public string ProcessHtml(string htmlContent)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);

            ResetHierarchy();

            var headers = doc.DocumentNode.SelectNodes("//h1|//h2|//h3|//h4");

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    int level = int.Parse(header.Name.Substring(1));
                    ProcessHeader(header, level);
                }
            }

            return doc.DocumentNode.OuterHtml;
        }

        private string SanitizeEntityName(string entityName)
        {
            return Regex.Replace(entityName.ToLower().Trim(), @"[^a-z0-9-]", "-");
        }

        private void ResetHierarchy()
        {
            _root = new HeaderNode { Level = 0 };
            _currentNode = _root;
        }

        private void ProcessHeader(HtmlNode header, int level)
        {
            // Find appropriate parent for this header level
            while (_currentNode != null && _currentNode.Level >= level)
            {
                _currentNode = _currentNode.Parent;
            }

            // Create new header node
            var newNode = new HeaderNode
            {
                Level = level,
                Parent = _currentNode,
                Count = (_currentNode?.Children.Count ?? 0) + 1
            };

            _currentNode?.Children.Add(newNode);

            // Generate ID
            string headerId = BuildHeaderId(newNode);

            // Add ID to header
            header.SetAttributeValue("id", headerId);
            header.AddClass("toc-header");

            // Update current node
            _currentNode = newNode;
        }

        private string BuildHeaderId(HeaderNode node)
        {
            var idParts = new List<string> { _entityName };
            var path = new List<HeaderNode>();

            // Build path from root to current node
            var current = node;
            while (current != null && current.Level > 0)
            {
                path.Insert(0, current);
                current = current.Parent;
            }

            // Add counts to ID
            foreach (var headerNode in path)
            {
                idParts.Add(headerNode.Count.ToString());
            }

            return string.Join("-", idParts);
        }
    }
}
