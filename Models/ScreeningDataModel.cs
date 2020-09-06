using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Screening.Models
{
    public class ScreeningDataModel
    {
       
        public string screening_id { set; get; }
        public string film_id { set; get; }
        public DateTime start_time { set; get; }
        public string hall_id { set; get; }
    }
    public class ScreeningList
    {
        public List<ScreeningDataModel> ScreeningDataList { set; get; }
    }
}