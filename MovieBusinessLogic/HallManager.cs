using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieModel;

namespace MovieBusinessLogic
{
    public class HallManager
    {
        private MovieRepository.MySQL.Screening_Rp rp = new MovieRepository.MySQL.Screening_Rp();
        public HallModel GetHallById(string screening_id)
        {
            return rp.GetHallByScreeningId(screening_id);
        }
    }
}
