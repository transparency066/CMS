using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWeb.Models
{
    public class Complaint
    {
        public string UID { set; get; }
        public DateTime ComplaintTime { set; get; }
        public string ComplaintText { set; get; }
        public int ReplyFlag { set; get; }
        public List<Complaint> Complaints { set; get; }
    }
}
