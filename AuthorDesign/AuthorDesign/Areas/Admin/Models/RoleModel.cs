using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthorDesign.Web.Areas.Admin.Models
{
    /// <summary>
    /// 角色
    /// </summary>
    public class RoleModel
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Display(Name="角色Id")]
        public int Id { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        [StringLength(15)]
        [Display(Name = "角色名称")]
        [Required(ErrorMessage = "角色名称不能为空")]
        public string Name { get; set; }
        /// <summary>
        /// 角色简介
        /// </summary>
        [StringLength(30)]
        [Display(Name = "角色简介")]
        public string Intro { get; set; }
        /// <summary>
        /// 角色状态（0:正常;1:已删除）
        /// </summary>
        [Display(Name = "角色状态")]
        [Required(ErrorMessage = "角色状态不能为空")]
        public Byte State { get; set; }
        /// <summary>
        /// 排序数字
        /// </summary>
        [Display(Name="排序数字")]
        [Required(ErrorMessage = "排序数字不能为空")]
        public int OrderNum { get; set; }
    }
    /// <summary>
    /// 角色页面按钮
    /// </summary>
    public class RolePageModel {
        public int PageId { get; set; }
        public List<RolePageActionModel> ActionList { get; set; }
    }
    public class RolePageActionModel {
        public int ActionId { get; set; }
        public int actionChecked { get; set; }
    }
}