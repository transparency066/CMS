using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieModel
{
    public class Account
    {
        [Display(Name = "用户名")]
        [Required(ErrorMessage ="请填写用户名")]
        [StringLength(10, ErrorMessage = "用户名为十位字符", MinimumLength = 10)]
        public string UserName { get; set; }

        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "请填写密码")]
        [StringLength(20,ErrorMessage = "密码不能超过20位")]
        public string PassWord { get; set; }
    }
}
