using MovieModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBusinessLogic
{
    public class Manager
    {
        private MovieRepository.MySQL.MovieRepository_cwb repository = new MovieRepository.MySQL.MovieRepository_cwb();

        public int InsertUser(Userinf userinf)
        {
            // string a = "123";
           int flag = 0;
           flag=repository.InsertUserinfs(userinf);
            return flag;
        }
        public Userinf SearchUser(Userinf userinf)
        {
            userinf = repository.SearchUserinfs(userinf);
            return userinf;
        }

        public int DeleteUser(Userinf userinf)
        {
            int flag = 0;
            flag = repository.DeleteUserinfs(userinf);
            return flag;
        }

        public int UpdateUser(Userinf userinf,string currentacc)
        {
            int flag = 0;
            flag = repository.UpdateUserinfs(userinf,currentacc);
            return flag;
        }

        public int InsertMovie(Movieinf movieinf)
        {
            int flag = repository.InsertMovieinfs(movieinf);
            return flag;
        }

        public Movieinf SearchMovie(Movieinf movieinf)
        {
            movieinf = repository.SearchMovieinfs(movieinf);
            return movieinf;
        }

        public int DeleteMovie(Movieinf movieinf)
        {
            int flag = repository.DeleteMovieinfs(movieinf);
            return flag;
        }

        public int UpdateMovie(Movieinf movieinf,string currentid)
        {
            int flag = repository.UpdateMovieinfs(movieinf, currentid);
            return flag;
        }

        public List<Movieinf> GetMovieinfs()
        {
            return repository.GetallMovieinfs();
        }

        public List<Userinf> GetUserinfs()
        {
            return repository.GetallUserinfs();
        }
    }
}
