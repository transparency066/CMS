using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieModel;

namespace MovieBusinessLogic
{
    public class Admin
    {
        //增删查改用户信息
        private MovieRepository.MySQL.UserInfoRp userinfo = new MovieRepository.MySQL.UserInfoRp();

        //插入用户
        public int InsertUser(string account, string password, string name, string phone, int sex)
        {
            // string a = "123";
            //int flag = 0;
            //flag = userinfo.InsertUserinfs(account, password, name, phone, sex);
            return userinfo.InsertUserinfs(account, password, name, phone, sex);
        }

        //搜索用户
        public Account SearchUser(string account)
        {
            return userinfo.SearchUserinfs(account);
        }

        //删除用户
        public int DeleteUser(string account)
        {
            return userinfo.DeleteUserinfs(account);
        }

        //更新用户
        public int UpdateUser(string account,string password,string name,string phone,int sex,string currentacc)
        {
            return userinfo.UpdateUserinfs(account, password, name, phone, sex, currentacc);
        }

        //获取所有用户
        public List<Account> GetUserinfs()
        {
            return userinfo.GetallUserinfs();
        }


        //增删查改影片信息
        private MovieRepository.MySQL.MovieInfoRp movieinfo = new MovieRepository.MySQL.MovieInfoRp();

        //插入电影
        public int InsertMovie(Movieinfo movieinf)
        {
            return movieinfo.InsertMovieinfs(movieinf);
        }

        //搜索电影
        public Movieinfo SearchMovie(string id)
        {
            return movieinfo.SearchMovieinfs(id);
        }

        //删除电影
        public int DeleteMovie(string id)
        {
            return movieinfo.DeleteMovieinfs(id);
        }

        //更新电影
        public int UpdateMovie(Movieinfo movieinf, string currentid)
        {
            return movieinfo.UpdateMovieinfs(movieinf, currentid);
        }

        //获取所有电影
        public List<Movieinfo> GetMovieinfs()
        {
            return movieinfo.GetallMovieinfs();
        }


        //查看反馈投诉
        private MovieRepository.MySQL.ComplaintRp complaint = new MovieRepository.MySQL.ComplaintRp();

        public List<Complaint> UserComplaint()
        {
            return complaint.ViewComplaint();
        }

        //发送消息
        private MovieRepository.MySQL.MessageRp reply = new MovieRepository.MySQL.MessageRp();

        public int SendMess(string uid, DateTime complainTime, string aid, DateTime replyTime, string message)
        {
            int flag = reply.SendMessage(uid, complainTime, aid, replyTime, message);
            return flag;
        }

        private MovieRepository.MySQL.MarkRp mark = new MovieRepository.MySQL.MarkRp();

        //查询所有票据
        public DataRowCollection queryTicket() {
            return mark.queryTicket();
        }

        //查询评论记录
        public DataRowCollection queryComments()
        {
            return mark.getComments();
        }

        //删除评论记录
        public Boolean delComment(String pid, String id)
        {
            return mark.DelComment(pid,id);
        }

        private MovieRepository.MySQL.Screening_Rp screening_rp = new MovieRepository.MySQL.Screening_Rp();
        
        //
        public HallModel GetHallById(string hall_id)
        {
            return screening_rp.GetHallByHallId(hall_id);
        }

        public List<ScreeningModel> GetAllScreenings()
        {
            return screening_rp.Manager_GetAllScreeningData();
        }
        public ScreeningModel GetScreeningsById(string screening_id)
        {
            return screening_rp.GetScreeningById(screening_id);
        }
        public void DeleteScreeningInfoById(string screening_id)
        {
            screening_rp.DeleteScreeningById(screening_id);
        }
        public void ModifyScreeningInfoById(string screening_id, string film_id, string hall_id, DateTime start_time)
        {
            screening_rp.ModifyScreening(screening_id, film_id, hall_id, start_time);
        }
        public void CreateScreeningInfoById(string screening_id, string film_id, string hall_id, DateTime start_time)
        {
            screening_rp.CreateScreening(screening_id, film_id, hall_id, start_time);
        }
        public List<SeatsModel> GetSeatsByHallId(string screening_id)
        {
            return screening_rp.GetSeatsByHall(screening_id);
        }
        public bool CheckHallUsed(string hall_id, DateTime start_time)
        {
            return screening_rp.CheckHallTime(hall_id, start_time);
        }

        public bool CheckScreening(string screening_id)
        {
            return screening_rp.CheckScreeningId(screening_id);
        }

    }
}
