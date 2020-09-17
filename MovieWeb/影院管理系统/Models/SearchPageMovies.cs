using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieWeb.Models
{
    public class SearchPageMovies
    {
        public List<SearchMovie> searchMovies { get; set; }
        public List<RankMovie> rankMovies { get; set; }
    }
}