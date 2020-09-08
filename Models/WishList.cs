using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieWeb.Models
{
    public class WishList
    {
        public string uid { set; get; }
        public string movieID { set; get; }
        public string movieName { set; get; }
        public string movieImgPath { set; get; }
        public string movieDetail { set; get; }
        public DateTime upTime { set; get; }
        public DateTime downTime { set; get; }
        public DateTime wishTime { set; get; }
        public List<WishList> wishLists { set; get; }
    }
}