using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.Model {
    /// <summary>
    /// 页面
    /// </summary>
    public class PageMenu {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 父Id
        /// </summary>
        public int PId { get; set; }
        /// <summary>
        /// 页面名称
        /// </summary>
        [StringLength(15)]
        [Column(TypeName = "varchar")]
        public string Name { get; set; }
        /// <summary>
        /// 页面路径
        /// </summary>
        [StringLength(50)]
        [Column(TypeName = "varchar")]
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
        [Column(TypeName = "varchar")]
        public string Ico { get; set; }
    }

    /// <summary>
    /// 不写入数据库（菜单页面与页面按钮关系查询表）
    /// </summary>
    public class PageMenuAction {
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

    /// <summary>
    /// 不写入数据库（获取管理员对应可执行菜单列表）
    /// </summary>
    public class AdminPageAction {
        /// <summary>
        /// 页面Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 父Id
        /// </summary>
        public int PId { get; set; }
        /// <summary>
        /// 页面路径
        /// </summary>
        public string PageUrl { get; set; }
        /// <summary>
        /// 页面排序数字
        /// </summary>
        public int OrderNum { get; set; }
        /// <summary>
        /// 页面名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 页面图标
        /// </summary>
        public string Ico { get; set; }
        /// <summary>
        /// 管理员按钮操作列表
        /// </summary>
        public string AdminActionList { get; set; }
        /// <summary>
        /// 对应角色按钮操作列表
        /// </summary>
        public string RoleActionList { get; set; }
    }
}
