using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieRepository.MySQL;
using MovieModel;

namespace MovieBusinessLogic
{
    public class Manager_nyx
    {
        private MovieRepository.MySQL.MovieRepository_nyx repository = new MovieRepository.MySQL.MovieRepository_nyx();
        public List<Comment> GetAllComments()
        {
            return repository.GetAll();
        }
        public int PostComment(string mid,string uid,DateTime time,string content)
        {
            return repository.AddComment(mid,uid,time,content);
        }
        public int Complain(DateTime time,string uid,string content,int reply)
        {
            return repository.AddComplaint(time, uid, content, reply);
        }
    }
}
