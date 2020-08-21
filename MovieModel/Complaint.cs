using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieModel
{
    class Complaint
    {
        public DateTime ComplaintTime { get; set; }
        public string userID { get; set; }
        public string ComplaintContent { get; set; }
        public int isReply { get; set; }
    }
}
