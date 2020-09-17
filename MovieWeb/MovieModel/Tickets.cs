using System;
namespace MovieModel
{
    /// <summary>
    /// 影票:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Tickets
    {
        public Tickets()
        { }
        #region Model
        private string _影票id;
        private string _拥有者账号id;
        private string _场次id;
        private int _座位id;
        private DateTime _购票时间;
        private int _影票状态;
        /// <summary>
        /// 
        /// </summary>
        public string 影票ID
        {
            set { _影票id = value; }
            get { return _影票id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string 拥有者账号ID
        {
            set { _拥有者账号id = value; }
            get { return _拥有者账号id; }
        }
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
        public int 座位ID
        {
            set { _座位id = value; }
            get { return _座位id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime 购票时间
        {
            set { _购票时间 = value; }
            get { return _购票时间; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int 影票状态
        {
            set { _影票状态 = value; }
            get { return _影票状态; }
        }
        #endregion Model

    }
}

