using System;
using System.Collections.Generic;
using System.Text;

namespace GoMontag_Shared.Models.RecommendationList
{
    public class ListRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SharableLink { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool PutOnFrontPage { get; set; }
    }
}
