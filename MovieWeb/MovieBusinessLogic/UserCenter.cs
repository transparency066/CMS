using MovieModel;
using MovieRepository.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBusinessLogic
{
    public class UserCenter
    {
        UserCenterRp uc = new UserCenterRp();


        public int UpdateUserInfo(string account, string name, string phone, int sex)//
        {
            return uc.UpdateUserInfo(account, name, phone, sex);
        }
        public Users GetUserInfoByUID(string account)//
        {
            return uc.GetUserInfoByUID(account);
        }
        public List<Ticket> GetTickList(string account)
        {
            return uc.GetTickList(account);
        }
    }
}
