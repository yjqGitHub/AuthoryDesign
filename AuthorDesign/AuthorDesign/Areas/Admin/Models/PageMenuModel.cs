using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthorDesign.Web.Areas.Admin.Models {
    public class PageMenuModel {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 父Id
        /// </summary>
        public int PId { get; set; }
        /// <summary>
        /// 页面名称
        /// </summary>
        [StringLength(15)]
        [Required(ErrorMessage = "页面名称不能为空")]
        public string Name { get; set; }
        /// <summary>
        /// 页面路径
        /// </summary>
        [StringLength(50)]
        public string PageUrl { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public byte IsShow { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; }
        /// <summary>
        /// 页面图标
        /// </summary>
        [StringLength(30)]
        public string Ico { get; set; }
    }
}