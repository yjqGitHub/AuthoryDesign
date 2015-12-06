using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.Model {
    /// <summary>
    /// 用户与页面和页面动作联系表
    /// </summary>
    public class AdminToPage {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 管理员Id
        /// </summary>
        public int AdminId { get; set; }
        /// <summary>
        /// 页面Id
        /// </summary>
        public int PageId { get; set; }
        /// <summary>
        /// 动作集合（用json存储）
        /// </summary>
        [StringLength(300)]
        [Column(TypeName = "varchar")]
        public string ActionList { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public Byte IsShow { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public Byte IsDelete { get; set; }
    }
}
