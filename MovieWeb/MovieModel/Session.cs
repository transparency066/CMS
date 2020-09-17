using System;
namespace MovieModel
{
    /// <summary>
    /// 场次:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Session
    {
        public Session()
        { }
        #region Model
        private string _场次id;
        private string _影片id;
        private DateTime _开映时间;
        private string _放映厅id;
        /// <summary>
        /// 
        /// </summary>
        public string 场次ID
        {
            set { _场次id = value; }
            get { return _场次id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string 影片ID
        {
            set { _影片id = value; }
            get { return _影片id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime 开映时间
        {
            set { _开映时间 = value; }
            get { return _开映时间; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string 放映厅ID
        {
            set { _放映厅id = value; }
            get { return _放映厅id; }
        }
        #endregion Model

    }
}

