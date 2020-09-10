using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieModel;
using MovieRepository.MySQL;

namespace MovieBusinessLogic
{
    public class User
    {
        //用户登录注册
        private MovieRepository.MySQL.AccountRp account = new MovieRepository.MySQL.AccountRp();
        
        public int Login(string UserName,string PassWord)//登录
        {
            return account.CheckAccount(UserName,PassWord);
        }

        public int Register(string UserName,string PassWord,string Name,string PhoneNumber,int Sex)
        {
            return account.CreateAccount(UserName, PassWord,Name,PhoneNumber,Sex);
        }

        //收藏影片
        private WishRp wish_rp = new WishRp();

        public List<wishModel> getWishModels(string uid)
        {
            return wish_rp.GetWishModels(uid);
        }

        public bool addWishModels(string uid, string movieID, DateTime wishtime)
        {
            return wish_rp.AddWishModels(uid, movieID, wishtime);
        }

        public void deleteWishModels(string uid, string movieID)
        {
            wish_rp.DeleteWishModels(uid, movieID);
        }

        //评论
        private MovieRepository.MySQL.CommentRp comment = new MovieRepository.MySQL.CommentRp();

        public List<Comment> GetAllComments(string mid)
        {
            return comment.GetComments(mid);
        }

        public int PostComment(string mid, string uid, DateTime time, string content)
        {
            return comment.AddComment(mid, uid, time, content);
        }

        //查看用户反馈投诉情况
        private MovieRepository.MySQL.ComplaintRp complaint = new MovieRepository.MySQL.ComplaintRp();

        public List<Complaint> GetAllComplaints(string uid)
        {
            return complaint.GetComplaints(uid);
        }

        //反馈投诉
        public int Complain(DateTime time, string uid, string content, int reply)
        {
            return complaint.AddComplaint(time, uid, content, reply);
        }

        //查看信息
        private MessageRp message = new MessageRp();

        public List<Message> getMessages(string uid)
        {
            return message.GetMessages(uid);
        }

        //查看信息
        private FilmRp film = new FilmRp();

        //搜索影片功能
        public DataRowCollection QueryFilm(string key)
        {
            return film.queryFilms(key);
        }
        public Boolean Rate(String id, String s,String uid)
        {
            return film.rate(id, s, uid);
        }
        public String getMark(String id)
        {
            return film.queryScoreById(id);
        }
        public DataRow getFilmById(String id)
        {
            return film.queryFilmById(id);
        }
    }
}
