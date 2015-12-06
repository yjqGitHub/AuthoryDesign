using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthorDesign.Web.Areas.Admin.Models {
    /// <summary>
    /// 页面动作按钮列表
    /// </summary>
    public class PageMenuActionModel {
        /// <summary>
        /// 页面Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 页面父Id
        /// </summary>
        public int PId { get; set; }
        /// <summary>
        /// 页面名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 按钮列表
        /// </summary>
        public string ActionList { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public byte IsDelete { get; set; }
    }
}