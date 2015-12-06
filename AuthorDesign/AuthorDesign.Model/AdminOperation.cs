using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.Model {
    /// <summary>
    /// 管理员操作类
    /// </summary>
    public class AdminOperation {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 管理员Id
        /// </summary>
        public int AdminId { get; set; }
        /// <summary>
        /// 权限Id
        /// </summary>
        public int AuthoryId { get; set; }
        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public Byte IsSuperAdmin { get; set; }
        /// <summary>
        /// 动作Id（1：增，2：修改，3：查看，4：删除）
        /// </summary>
        public int Action { get; set; }
        /// <summary>
        /// 操作标题
        /// </summary>
        [StringLength(15)]
        [Column(TypeName = "nvarchar")]
        public string Title { get; set; }
        /// <summary>
        /// 操作详情
        /// </summary>
        [StringLength(200)]
        [Column(TypeName = "nvarchar")]
        public string Content { get; set; }
        /// <summary>
        /// 操作IP
        /// </summary>
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string OperateIP { get; set; }
        /// <summary>
        /// 操作地址
        /// </summary>
        [StringLength(50)]
        [Column(TypeName = "nvarchar")]
        public string OperateAddress { get; set; }
        /// <summary>
        /// 浏览器信息或者APP信息
        /// </summary>
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string OperateInfo { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        public AdminOperation() {
            CreateTime = DateTime.Now;
        }
    }
}
