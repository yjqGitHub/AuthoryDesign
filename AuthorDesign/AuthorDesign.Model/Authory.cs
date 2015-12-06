using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.Model {
    /// <summary>
    /// 角色
    /// </summary>
    public class Authory {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        [StringLength(15)]
        [Column(TypeName = "varchar")]
        public string Name { get; set; }
        /// <summary>
        /// 角色简介
        /// </summary>
        [StringLength(30)]
        [Column(TypeName = "varchar")]
        public string Intro { get; set; }
        /// <summary>
        /// 角色状态（0:正常;1:已删除）
        /// </summary>
        public Byte State { get; set; }
        /// <summary>
        /// 排序数字
        /// </summary>
        public int OrderNum { get; set; }
    }
}
