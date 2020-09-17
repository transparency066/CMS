using System;
namespace MovieModel
{
    /// <summary>
    /// 影片:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Movie
    {
        public Movie()
        { }
        #region Model
        private string _影片id;
        private string _名字;
        private string _类型;
        private DateTime _时长;
        private DateTime _上映日期;
        private DateTime _下线日期;
        private float _票价;
        private string _简介;
        private string _图片url;
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
        public string 名字
        {
            set { _名字 = value; }
            get { return _名字; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string 类型
        {
            set { _类型 = value; }
            get { return _类型; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime 时长
        {
            set { _时长 = value; }
            get { return _时长; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime 上映日期
        {
            set { _上映日期 = value; }
            get { return _上映日期; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime 下线日期
        {
            set { _下线日期 = value; }
            get { return _下线日期; }
        }
        /// <summary>
        /// 
        /// </summary>
        public float 票价
        {
            set { _票价 = value; }
            get { return _票价; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string 简介
        {
            set { _简介 = value; }
            get { return _简介; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string 图片url
        {
            set { _图片url = value; }
            get { return _图片url; }
        }
        #endregion Model

    }
}

