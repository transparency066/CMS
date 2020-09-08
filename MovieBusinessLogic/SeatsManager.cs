using MovieModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBusinessLogic
{
    public class SeatsManager
    {
        private MovieRepository.MySQL.Screening_Rp rp = new MovieRepository.MySQL.Screening_Rp();
        
        public List<SeatsModel> GetSeatsByScreening_id(string screening_id)
        {
            return rp.GetSeatsByScreening(screening_id);
        }
    }
}
