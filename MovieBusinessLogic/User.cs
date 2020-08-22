using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieModel;
using MovieRepository.MySQL;

namespace MovieBusinessLogic
{
    public class User
    {
        private MovieRepository.MySQL.Account account = new MovieRepository.MySQL.Account();
        
        public int Login(string UserName,string PassWord)//登录
        {
            return account.CheckAccount(UserName,PassWord);
        }

        public int Register(string UserName,string PassWord)
        {
            return account.CreateAccount(UserName, PassWord);
        }

        /*MJX*/
        private wishList_Rp wish_rp = new wishList_Rp();
        private M2U_Rp m2u_Rp = new M2U_Rp();

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

        public List<M2UModel> getM2UModels(string uid)
        {
            return m2u_Rp.GetM2UModels(uid);
        }
    }
}
