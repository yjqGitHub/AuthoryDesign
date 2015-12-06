using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthorDesign.Web.Areas.Admin.Models {
    /// <summary>
    /// 登录类
    /// </summary>
    public class LoginModel {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage="请输入用户名")]
        [StringLength(30,MinimumLength=5,ErrorMessage="请输入正确的用户名")]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入用密码")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "请输入正确的密码")]
        public string Password { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage="请输入验证码")]
        [StringLength(6,MinimumLength=6,ErrorMessage="验证码错误")]
        public string ValidateCode { get; set; }
        /// <summary>
        /// 是否记住密码
        /// </summary>
        public bool IsRemind { get; set; }
    }
}