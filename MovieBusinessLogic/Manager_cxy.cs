using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieModel;
using MovieRepository.MySQL;

namespace MovieBusinessLogic
{
    public class Manager
    {
        private MovieRepository.MySQL.ComplaintRepository repository1 = new MovieRepository.MySQL.ComplaintRepository();
       public List<UComplaint> UserComplaint()
        {        
            return repository1.UntreatedComplaint();
        }

        private MovieRepository.MySQL.ReplyRepository repository2 = new MovieRepository.MySQL.ReplyRepository();
        public int SendMess(MessageSend messageSend)
        {
            int flag = repository2.SendMessage(messageSend);
            return flag;
        }
    }
}
