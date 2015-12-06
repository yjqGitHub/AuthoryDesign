using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthorDesign.Web.Areas.Admin.Models {
    public class PageActionModel {
        /// <summary>
        /// 动作Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 动作名称
        /// </summary>
        [StringLength(15)]
        [Required(ErrorMessage = "动作名称不能为空")]
        public string Name { get; set; }
        /// <summary>
        /// 动作代码（页面代码）
        /// </summary>
        [StringLength(35)]
        [Required(ErrorMessage = "动作代码不能为空")]
        public string ActionCode { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public Byte IsShow { get; set; }
        /// <summary>
        /// 动作等级
        /// </summary>
        public Byte ActionLevel { get; set; }
    }
}