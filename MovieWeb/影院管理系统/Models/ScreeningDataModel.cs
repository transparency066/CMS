using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MovieWeb.Models
{
    public class ScreeningDataModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0}不能为空！")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "{0}的长度应为3-10位")]
        public string screening_id { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0}不能为空！")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "{0}的长度应为3-10位")]
        public string film_id { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0}不能为空！")]
        public DateTime start_time { set; get; }

        [StringLength(10, MinimumLength = 3, ErrorMessage = "{0}的长度应为3-10位")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "不能为空！")]
        public string hall_id { set; get; }
    }
    public class ScreeningList
    {
        public List<ScreeningDataModel> ScreeningDataList { set; get; }
    }
}