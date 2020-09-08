using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieModel;

namespace MovieBusinessLogic
{
    public class ScreeningManager
    {
        private MovieRepository.MySQL.Screening_Rp rp = new MovieRepository.MySQL.Screening_Rp();

        public List<ScreeningModel> GetAllScreenings()
        {
            return rp.Manager_GetAllScreeningData();
        }
        public ScreeningModel GetScreeningsById(string screening_id)
        {
            return rp.GetScreeningById(screening_id);
        }
        public void DeleteScreeningInfoById(string screening_id)
        {
            rp.DeleteScreeningById(screening_id);
        }
        public void ModifyScreeningInfoById(string screening_id, string film_id, string hall_id, DateTime start_time)
        {
            rp.ModifyScreening(screening_id, film_id, hall_id, start_time);
        }
        public void CreateScreeningInfoById(string screening_id, string film_id, string hall_id, DateTime start_time)
        {
            rp.CreateScreening(screening_id, film_id, hall_id, start_time);
        }

    }
}
