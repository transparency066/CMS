using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieModel
{
    public class SeatsModel
    {
        public string screening_id { set; get; }
        
        public int x { set; get; }
        public int y { set; get; }
        public bool available { set; get; }
        public bool used { set; get; }

    }
    
}
