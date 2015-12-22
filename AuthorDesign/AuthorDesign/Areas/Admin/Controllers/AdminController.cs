using AuthorDesign.Common;
using AuthorDesign.Web.App_Start.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AuthorDesign.Web.Areas.Admin.Controllers
{
    [AdminAuthory]// 添加权限控制
    public class AdminController : Controller
    {
        //
        // GET: /Admin/Admin/

        /// <summary>
        /// 设置当前的路径与操作代码
        /// </summary>
        /// <returns></returns>
        [AdminActionMethod(RoleCode = "LookRole", ActionUrl = "Admin/AdminList")]
        public ActionResult AdminList()
        {
            ViewBag.Title = "管理员列表";
            ViewBag.RoleSelectItem = SelectItemHelper.GetRoleItemList(0);
            return View();
        }
        [HttpPost]
        public JsonResult AdminList(Models.AdminParamModel model) {
            int rowCount = 0;
            var result = EnterRepository.GetRepositoryEnter().GetAdminRepository.LoadPageList(model.RoleId, model.Mobile, model.iDisplayStart, model.iDisplayLength, model.IsDesc, out rowCount);
            return Json(new {
                sEcho = model.sEcho,
                iTotalRecords = rowCount,
                iTotalDisplayRecords = rowCount,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取管理员信息
        /// </summary>
        /// <param name="id">管理员Id</param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "LookRole", ActionUrl = "Admin/AdminList",ActionResultType=1)]
        public JsonResult GetAdminInfo(int id = 0) {
            var result = EnterRepository.GetRepositoryEnter().GetAdminRepository.LoadEntities(m => m.Id == id && m.IsSuperAdmin == 0).FirstOrDefault();
            if (result == null) {
                return Json(new { state = "error", message = "无法找到该管理员信息" });
            }
            else {
                return Json(new { state = "success", result });
            }
        }
        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "LookRole", ActionUrl = "Admin/AdminList", ActionResultType = 1)]
        public JsonResult AddAdmin(Models.AdminModel model) {
            if (ModelState.IsValid) {
                IDAL.IAdminRepository adminRepository = EnterRepository.GetRepositoryEnter().GetAdminRepository;
                //判断权限名称是否已存在
                var result = adminRepository.LoadEntities(m => m.Mobile == model.Mobile.Trim()).FirstOrDefault();
                if (result == null) {
                    Random rn = new Random();
                    string salt = rn.Next(10000, 99999).ToString() + rn.Next(10000, 99999).ToString();
                    adminRepository.AddEntity(new Model.Admin() {
                        AuthoryId = model.AuthoryId,
                        CreateTime = DateTime.Now,
                        IsLogin = model.IsLogin,
                        Mobile = model.Mobile,
                        IsSuperAdmin = 0,
                        LastLoginTime = DateTime.Now,
                        Salt = salt,
                        Password = MD5Helper.CreatePasswordMd5("ad" + model.Mobile.Substring(7, 4), salt)
                    });
                    //添加下操作记录
                    PublicFunction.AddOperation(1, string.Format("添加管理员"), string.Format("添加管理员=={0}==成功", model.Mobile));
                    if (EnterRepository.GetRepositoryEnter().SaveChange() > 0) {
                        return Json(new {
                            state = "success",
                            message = "添加管理员成功"
                        });
                    }
                    else {
                        PublicFunction.AddOperation(1, string.Format("添加管理员"), string.Format("添加管理员=={0}==失败", model.Mobile));
                        EnterRepository.GetRepositoryEnter().SaveChange();
                        return Json(new {
                            state = "error",
                            message = "添加管理员失败"
                        });
                    }
                }
                else {
                    return Json(new {
                        state = "error",
                        message = "手机号码已经存在了"
                    });
                }
            } else {
                return Json(new { state="error",message="信息不完整"});
            }
        }
        /// <summary>
        /// 修改管理员
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "UpdateRole", ActionUrl = "Admin/AdminList", ActionResultType = 1)]
        public JsonResult UpdateAdmin(Models.AdminModel model) {
            if (ModelState.IsValid) {
                IDAL.IAdminRepository adminRepository = EnterRepository.GetRepositoryEnter().GetAdminRepository;
                //判断权限名称是否已存在
                var result = adminRepository.LoadEntities(m => m.Mobile == model.Mobile.Trim()).FirstOrDefault();
                if (result != null && result.Id != model.Id) {
                    return Json(new {
                        state = "error",
                        message = "手机号码已经存在了"
                    });
                }
                else {
                    Model.Admin admin = new Model.Admin() {
                        AuthoryId = model.AuthoryId,
                        IsLogin = model.IsLogin,
                        Mobile = model.Mobile,
                        Id = model.Id
                    };
                    //清楚context中result对象
                    adminRepository.Get(m => m.Id == model.Id);
                    adminRepository.EditEntity(admin, new string[] { "AuthoryId", "IsLogin", "Mobile" });
                    PublicFunction.AddOperation(1, string.Format("修改管理员"), string.Format("修改管理员=={0}==成功", model.Mobile));
                    if (EnterRepository.GetRepositoryEnter().SaveChange() > 0) {
                        return Json(new {
                            state = "success",
                            message = "修改管理员成功"
                        });
                    }
                    else {
                        PublicFunction.AddOperation(1, string.Format("修改管理员"), string.Format("修改管理员=={0}==失败", model.Mobile));
                        EnterRepository.GetRepositoryEnter().SaveChange();
                        return Json(new {
                            state = "error",
                            message = "修改管理员失败"
                        });
                    }
                }
            }
            else {
                return Json(new { state = "error", message = "信息不完整" });
            }
        }

        /// <summary>
        /// 更改管理员可登陆状态
        /// </summary>
        /// <param name="id">管理员Id</param>
        /// <param name="state">管理员状态</param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "UpdateRole", ActionUrl = "Admin/AdminList", ActionResultType = 1)]
        public JsonResult UpdateState(int id = 0, int state = 0) {
            EnterRepository.GetRepositoryEnter().GetAdminRepository.EditEntity(new Model.Admin() { Id = id, IsLogin = (byte)state }, new string[] { "IsLogin" });
            PublicFunction.AddOperation(1, string.Format("修改管理员可登陆状态"), string.Format("修改管理员可登陆状态成功"));
            if (EnterRepository.GetRepositoryEnter().SaveChange() > 0) {
                return Json(new { state = "success", message = "修改管理员可登陆状态成功" });
            }
            else {
                PublicFunction.AddOperation(1, string.Format("修改管理员可登陆状态"), string.Format("修改管理员可登陆状态失败"));
                EnterRepository.GetRepositoryEnter().SaveChange();
                return Json(new { state = "error", message = "服务器泡妞去了" });
            }
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id">管理员Id</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "UpdateRole", ActionUrl = "Admin/AdminList", ActionResultType = 1)]
        public JsonResult RestPassword(int id, string newPassword) {
            Random rn = new Random();
            string salt = rn.Next(10000, 99999).ToString() + rn.Next(10000, 99999).ToString();
            EnterRepository.GetRepositoryEnter().GetAdminRepository.EditEntity(new Model.Admin() { Id = id, Password = MD5Helper.CreatePasswordMd5(newPassword, salt), Salt = salt }, new string[] { "Password", "Salt" });
            PublicFunction.AddOperation(1, string.Format("重置管理员密码"), string.Format("重置管理员=={0}==密码成功", id));
            if (EnterRepository.GetRepositoryEnter().SaveChange() > 0) {
                return Json(new { state = "success", message = "重置管理员密码成功" });
            }
            else {
                PublicFunction.AddOperation(1, string.Format("重置管理员密码"), string.Format("修改管理员=={0}==可登陆状态失败", id));
                EnterRepository.GetRepositoryEnter().SaveChange();
                return Json(new { state = "error", message = "服务器泡妞去了" });
            }
        }
        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="id">管理员Id</param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "DeleteRole", ActionUrl = "Admin/AdminList", ActionResultType = 1)]
        public JsonResult DeleteAdmin(int id = 0) {
            EnterRepository.GetRepositoryEnter().GetAdminRepository.DeleteEntity(new Model.Admin() { Id = id });
            PublicFunction.AddOperation(1, string.Format("删除管理员"), string.Format("删除管理员成功"));
            if (EnterRepository.GetRepositoryEnter().SaveChange() > 0) {
                return Json(new { state = "success", message = "删除管理员成功" });
            }
            else {
                PublicFunction.AddOperation(1, string.Format("删除管理员"), string.Format("删除管理员失败"));
                EnterRepository.GetRepositoryEnter().SaveChange();
                return Json(new { state = "error", message = "服务器泡妞去了" });
            }
        }

        #region 管理员页面按钮编辑
        /// <summary>
        /// 查看管理员页面按钮
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "LookActionRole", ActionUrl = "Admin/AdminList", ActionResultType = 1)]
        public JsonResult AdminPageActionDesc(int id = 0) {
            IDAL.IAdminToPageRepository repository = EnterRepository.GetRepositoryEnter().GetAdminToPageRepository;
            List<Model.AdminToPage> adminPageList = repository.LoadEntities(m => m.AdminId == id).ToList();
            return Json(new { state = "success", adminPageList });
        }

        /// <summary>
        /// 管理员页面按钮编辑
        /// </summary>
        /// <param name="id">管理员Id</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "UpdateActionRole", ActionUrl = "Admin/AdminList", ActionResultType = 1)]
        public JsonResult UpdatePageAction(int id, List<Models.RolePageModel> model) {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            IDAL.IAdminToPageRepository repository = EnterRepository.GetRepositoryEnter().GetAdminToPageRepository;
            foreach (var item in model) {
                AuthorDesign.Model.AdminToPage authoryPageModel = repository.LoadEntities(m => m.AdminId == id && m.PageId == item.PageId).FirstOrDefault();
                if (authoryPageModel == null) {
                    authoryPageModel = new Model.AdminToPage();
                    authoryPageModel.AdminId = id;
                    authoryPageModel.PageId = item.PageId;
                    authoryPageModel.ActionList = serializer.Serialize(item.ActionList);
                    if (item.ActionList != null && item.ActionList.Count() > 0) {
                        if (item.ActionList.Where(m => m.actionChecked == 1).Count() == 0) {
                            authoryPageModel.IsShow = (byte)0;
                        }
                        else {
                            authoryPageModel.IsShow = (byte)1;
                        }
                    }
                    else {
                        authoryPageModel.IsShow = (byte)0;
                    }
                    repository.AddEntity(authoryPageModel);
                }
                else {
                    authoryPageModel.AdminId = id;
                    authoryPageModel.PageId = item.PageId;
                    authoryPageModel.ActionList = serializer.Serialize(item.ActionList);
                    if (item.ActionList != null && item.ActionList.Count() > 0) {
                        if (item.ActionList.Where(m => m.actionChecked == 1).Count() == 0) {
                            authoryPageModel.IsShow = (byte)0;
                        }
                        else {
                            authoryPageModel.IsShow = (byte)1;
                        }
                    }
                    else {
                        authoryPageModel.IsShow = (byte)0;
                    }
                }
            }
            PublicFunction.AddOperation(2, string.Format("编辑管理员页面按钮"), string.Format("编辑管理员页面按钮成功"));
            if (EnterRepository.GetRepositoryEnter().SaveChange() > 0) {
                CacheHelper.RemoveCache("AdminMenuList");
                return Json(new { state = "success", message = "编辑管理员页面按钮成功" });
            }
            else {
                PublicFunction.AddOperation(2, string.Format("编辑角色页面按钮"), string.Format("编辑角色页面按钮失败"));
                EnterRepository.GetRepositoryEnter().SaveChange();
                return Json(new { state = "fail", message = "编辑管理员页面按钮失败" });
            }
        }

        [HttpGet]
        public ActionResult AdminActionListPartial(int id=0) {
            var result = AuthorDesign.Web.App_Start.Common.EnterRepository.GetRepositoryEnter().GetAdminRepository.LoadEntities(m => m.Id == id).FirstOrDefault();

            IEnumerable<AuthorDesign.Model.PageMenuAction> pageMenuList = AuthorDesign.Web.App_Start.Common.EnterRepository.GetRepositoryEnter().GetPageMenuRepository.GetAdminPageMenuList(result == null ? 0 : result.AuthoryId);
            ViewBag.Id = id;
            return PartialView(pageMenuList);
        }
        #endregion
    }
}
