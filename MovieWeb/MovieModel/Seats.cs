using System;
namespace MovieModel
{
    /// <summary>
    /// 座位:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Seats
    {
        public Seats()
        { }
        #region Model
        private int _座位id;
        private string _放映厅id;
        private int _行位置;
        private int _列位置;
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
        public string 放映厅ID
        {
            set { _放映厅id = value; }
            get { return _放映厅id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int 行位置
        {
            set { _行位置 = value; }
            get { return _行位置; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int 列位置
        {
            set { _列位置 = value; }
            get { return _列位置; }
        }
        #endregion Model

    }
}

