using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
