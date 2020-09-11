using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieModel
{
    public class Ticket
    {
        public string 影票ID { get; set; }
        public string 场次ID { get; set; }
        public DateTime 购票时间 { get; set; }
        public int 放映厅ID { get; set; }
        public string 放映厅位置 { get; set; }
        public int 座位ID { get; set; }
        public int 行位置 { get; set; }
        public int 列位置 { get; set; }
        public int 影票状态 { get; set; }
    }
}