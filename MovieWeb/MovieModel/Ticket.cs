using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieModel
{
    public class Ticket
    {
        public string TicketID { get; set; }
        public string ChangCiID{ get; set; }
        public string MovieName { get; set; }
        public DateTime PlayTime { get; set; }
        public DateTime TicketDate { get; set; }
        public string FangYingTingID { get; set; }
        public string FangYingTingPosition { get; set; }
        public string SeatID { get; set; }
        public int HangNo { get; set; }
        public int LieNo { get; set; }
        public int State { get; set; }
        public float Mark { get; set; }
    }
}