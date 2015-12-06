using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.Model {
    /// <summary>
    /// 角色与页面和页面动作之间联系
    /// </summary>
    public class AuthoryToPage {
        /// <summary>
        /// 自增Id
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 权限Id
        /// </summary>
        public int AuthoryId { get; set; }
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
