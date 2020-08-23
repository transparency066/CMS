using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieWeb.Models
{
    public class M2UInfo
    {
        public string uid { set; get; }
        public string mid { set; get; }
        public DateTime U2Mtime { set; get; }
        public DateTime M2Utime { set; get; }
        public string M2Utext { set; get; }

        public List<M2UInfo> m2UInfos { set; get; }
    }
}