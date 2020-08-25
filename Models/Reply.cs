using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace MovieWeb.Models
{
    public class reply
    {
        public string UID { set; get; }
        public DateTime ComplaintTime { set; get; }
        public string AdminID { set; get; }
        public DateTime ReplyTime { set; get; }
        public string ReplyText { set; get; }
        public int flag { set; get; }
    }
}