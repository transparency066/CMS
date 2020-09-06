using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieModel
{
    public class ScreeningModel
    {
        public string screening_id { set; get; }
        public string film_id { set; get; }
        public DateTime start_time { set; get; }
        public string hall_id { set; get;  }
    }
}
