using System;
namespace MovieModel
{
    /// <summary>
    /// 放映厅:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Hall
    {
        public Hall()
        { }
        #region Model
        private string _放映厅id;
        private int _容量;
        private string _放映厅位置;
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
        public int 容量
        {
            set { _容量 = value; }
            get { return _容量; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string 放映厅位置
        {
            set { _放映厅位置 = value; }
            get { return _放映厅位置; }
        }
        #endregion Model

    }
}

