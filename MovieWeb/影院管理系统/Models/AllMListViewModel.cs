using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieWeb.Models
{
    public class AllMListViewModel
    {
        public List<AllMViewModel> AllMs { get; set; }
        public int Count { get; set; }
    }
}