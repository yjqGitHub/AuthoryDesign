using AuthorDesign.Web.App_Start.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthorDesign.Web.Areas.Admin.Controllers
{
    [AdminAuthory]
    public class HomeController : Controller
    {
        //
        // GET: /Admin/Home/

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [AdminActionMethod(RoleCode = "NoNeedAuthory", ActionUrl = "")]
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 无权限页面
        /// </summary>
        /// <returns></returns>
         [AdminActionMethod(RoleCode = "NoNeedAuthory", ActionUrl = "")]
        public ActionResult NoAuthory() {
            ViewBag.Title = "您越权了";
            return View();
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AdminActionMethod(RoleCode = "NoNeedAuthory", ActionUrl = "")]
         public ActionResult LoginOut() {
             if (WebCookieHelper.AdminCheckLogin()) {
                 WebCookieHelper.AdminLoginOut();
             }
             return RedirectToAction("Login", "Account");
         }
    }
}
