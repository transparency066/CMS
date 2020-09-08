using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieWeb.Models
{
    public class Account
    {
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "请填写用户名")]
        [StringLength(10, ErrorMessage = "用户名为10位字符", MinimumLength = 10)]
        public string UserName { get; set; }

        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "请填写密码")]
        [StringLength(20, ErrorMessage = "密码不能超过20位")]
        public string PassWord { get; set; }

        [Display(Name="昵称")]
        [Required(ErrorMessage ="请填写昵称")]
        [StringLength(30,ErrorMessage ="昵称不能超过30位字符")]
        public string Name { get; set; }

        [Display(Name="电话")]
        [Required(ErrorMessage ="请填写电话号码")]
        [StringLength(11,ErrorMessage ="电话号码为11位字符",MinimumLength =11)]
        public string PhoneNumber { get; set; }

        [Display(Name="性别")]
        [Required(ErrorMessage ="请选择性别")]
        public int Sex { get; set; }

        public int flag { get; set; }
    }

    //public class AllUViewModel
    //{
    //    public string Account { get; set; }
    //    public string Password { get; set; }
    //    public string Name { get; set; }
    //    public string Phone { get; set; }
    //    public int Sex { get; set; }
    //}
}