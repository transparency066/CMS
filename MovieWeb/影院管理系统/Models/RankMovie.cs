using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieWeb.Models
{
    public class RankMovie
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Tickets { get; set; }
    }
}