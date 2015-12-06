using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.Model {
    /// <summary>
    /// 管理员登录日志
    /// </summary>
    public class AdminLoginLog {
        /// <summary>
        /// 管理员登录记录Id
        /// </summary>
        [Key]
        public int AdminLoginLogId { get; set; }
        /// <summary>
        /// 管理员Id
        /// </summary>
        public int AdminId { get; set; }
        /// <summary>
        /// 管理员登录地址
        /// </summary>
        [StringLength(50)]
        [Column(TypeName = "nvarchar")]
        public string AdminLoginAddress { get; set; }
        /// <summary>
        /// 管理员登录IP
        /// </summary>
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string AdminLoginIP { get; set; }
        /// <summary>
        /// 管理员登录时间
        /// </summary>
        public DateTime AdminLoginTime { get; set; }
        /// <summary>
        /// 浏览器信息或者APP信息
        /// </summary>
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string AdminLoginInfo { get; set; }
        public AdminLoginLog() {
            AdminLoginTime = DateTime.Now;
        }
    }
}
