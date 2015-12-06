using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.Model {
    /// <summary>
    /// 管理员
    /// </summary>
    public class Admin {
        /// <summary>
        /// 管理员ID（主键）
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [StringLength(30)]
        [Column(TypeName="varchar")]
        public string AdminName { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [StringLength(11)]
        [Column(TypeName = "varchar")]
        public string Mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(100)]
        [Column(TypeName = "varchar")]
        public string Email { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(120)]
        [Column(TypeName = "varchar")]
        public string Password { get; set; }
        /// <summary>
        /// 密码盐值
        /// </summary>
        [StringLength(10)]
        [Column(TypeName = "varchar")]
        public string Salt { get; set; }
        /// <summary>
        /// 对应角色Id
        /// </summary>
        public int AuthoryId { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public Byte IsSuperAdmin { get; set; }
        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
        /// <summary>
        /// 上次登录地址
        /// </summary>
        [StringLength(50)]
        [Column(TypeName = "nvarchar")]
        public string LastLoginAddress { get; set; }
        /// <summary>
        /// 上次登录端口基本信息
        /// </summary>
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string LastLoginInfo { get; set; }
        /// <summary>
        /// 上次登录Ip
        /// </summary>
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string LastLoginIp { get; set; }
        /// <summary>
        /// 是否可登录
        /// </summary>
        public Byte IsLogin { get; set; }
        public Admin(){
            CreateTime = DateTime.Now;
            LastLoginTime = DateTime.Now;
        }
    }
}
