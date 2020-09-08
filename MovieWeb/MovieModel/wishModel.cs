using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieModel
{
    public class wishModel//收藏影片模型
    {
        public string movieID { set; get; }
        public string movieName { set; get; }
        public DateTime wishTime { set; get; }
        public string movieImgPath { set; get; }
        public DateTime upTime { set; get; }
        public DateTime downTime { set; get; }
        public string movieDetail { set; get; }
    }
}
