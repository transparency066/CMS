using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieModel
{
    public class Comment
    {
        public string movieID { get; set; }
        public string userID { get; set; }
        
        public DateTime commentTime { get; set; }
        public string commentContent { get; set; }
    }
}
