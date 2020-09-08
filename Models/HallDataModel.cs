using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Screening.Models
{
    public class HallDataModel
    {
        public string screening_id { set; get; }
        public string hall_id { set; get; }
        public int total { set; get; }
        public int left { set; get; }
    }
}