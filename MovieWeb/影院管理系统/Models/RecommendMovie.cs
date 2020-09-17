using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieWeb.Models
{
    public class RecommendMovie
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string TEXT { set; get; }//详情页加入，用于收藏
        public string CLASS { set; get; }//详情页加入，用于收藏
        public List<RecommendMovie> recommendMovies { set; get; }
    }
}