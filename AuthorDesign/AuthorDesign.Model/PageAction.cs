using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.Model {
    /// <summary>
    /// 页面动作
    /// </summary>
    public class PageAction {
        /// <summary>
        /// 动作Id
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 动作名称
        /// </summary>
        [StringLength(15)]
        [Column(TypeName = "varchar")]
        public string Name { get; set; }
        /// <summary>
        /// 动作代码（页面代码）
        /// </summary>
        [StringLength(35)]
        [Column(TypeName = "varchar")]
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
