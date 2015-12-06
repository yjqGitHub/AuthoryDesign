using AuthorDesign.Common;
using AuthorDesign.Web.App_Start.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AuthorDesign.Web.Areas.Admin.Controllers {
    [AdminAuthory]
    public class RoleController : Controller {
        //
        // GET: /Admin/Role/
        [AdminActionMethod(RoleCode = "LookRole", ActionUrl = "Role/RoleList")]
        public ActionResult RoleList() {
            ViewBag.Title = "角色列表";

            return View();
        }

        [HttpPost]
        public JsonResult RoleList(Models.RoleParamModel model) {
            int rowCount = 0;
            var result = EnterRepository.GetRepositoryEnter().GetAuthoryRepository.LoadPageList(model.iDisplayStart, model.iDisplayLength, model.IsDesc, out rowCount);
            return Json(new {
                sEcho = model.sEcho,
                iTotalRecords = rowCount,
                iTotalDisplayRecords = rowCount,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "LookRole", ActionUrl = "Role/RoleList", ActionResultType = 1)]
        public JsonResult GetRoleInfo(int id = 0) {
            var result = EnterRepository.GetRepositoryEnter().GetAuthoryRepository.LoadEntities(m => m.Id == id).FirstOrDefault();
            if (result == null) {
                return Json(new { state = "error", message = "角色不存在" });
            }
            else {
                return Json(new { state = "success", result });
            }
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "LookRole", ActionUrl = "Role/RoleList", ActionResultType = 1)]
        public JsonResult AddRole(Models.RoleModel model) {
            if (ModelState.IsValid) {
                IDAL.IAuthoryRepository authoryRepository = EnterRepository.GetRepositoryEnter().GetAuthoryRepository;
                //判断权限名称是否已存在
                var result = authoryRepository.LoadEntities(m => m.Name == model.Name.Trim()).FirstOrDefault();
                if (result == null) {
                    authoryRepository.AddEntity(new Model.Authory() {
                        Intro = model.Intro,
                        Name = model.Name,
                        OrderNum = model.OrderNum,
                        State = model.State
                    });
                    //添加下操作记录
                    PublicFunction.AddOperation(1, string.Format("添加角色"), string.Format("添加角色=={0}==成功", model.Name));
                    if (EnterRepository.GetRepositoryEnter().SaveChange() > 0) {
                        return Json(new {
                            state = "success",
                            message = "添加角色成功"
                        });
                    }
                    else {
                        PublicFunction.AddOperation(1, string.Format("添加角色"), string.Format("添加角色=={0}==失败", model.Name));
                        EnterRepository.GetRepositoryEnter().SaveChange();
                        return Json(new {
                            state = "error",
                            message = "添加角色失败"
                        });
                    }
                }
                else {
                    return Json(new {
                        state = "error",
                        message = "角色名称已经存在了"
                    });
                }
            }
            else {
                return Json(new {
                    state = "error",
                    message = "信息不完整"
                });
            }
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "UpdateRole", ActionUrl = "Role/RoleList", ActionResultType = 1)]
        public JsonResult UpdateRole(Models.RoleModel model) {
            if (ModelState.IsValid && model.Id > 0) {
                IDAL.IAuthoryRepository authoryRepository = EnterRepository.GetRepositoryEnter().GetAuthoryRepository;
                //判断权限名称是否已存在
                var result = authoryRepository.LoadEntities(m => m.Name == model.Name.Trim()).FirstOrDefault();
                if (result != null && result.Id != model.Id) {
                    return Json(new {
                        state = "error",
                        message = "角色名称已经存在了"
                    });
                }
                else {
                    Model.Authory authory = new Model.Authory() {
                        Intro = model.Intro,
                        Name = model.Name,
                        OrderNum = model.OrderNum,
                        State = model.State,
                        Id = model.Id
                    };
                    EnterRepository.GetRepositoryEnter().GetAuthoryRepository.Get(m => m.Id == model.Id);
                    EnterRepository.GetRepositoryEnter().GetAuthoryRepository.EditEntity(authory, new string[] { "Intro", "Name", "OrderNum", "State" });
                    PublicFunction.AddOperation(1, string.Format("修改角色"), string.Format("修改角色=={0}==成功", model.Name));
                    if (EnterRepository.GetRepositoryEnter().SaveChange() > 0) {
                        return Json(new {
                            state = "success",
                            message = "修改角色成功"
                        });
                    }
                    else {
                        PublicFunction.AddOperation(1, string.Format("修改角色"), string.Format("修改角色=={0}==失败", model.Name));
                        EnterRepository.GetRepositoryEnter().SaveChange();
                        return Json(new {
                            state = "error",
                            message = "修改角色失败"
                        });
                    }
                }
            }
            else {
                return Json(new {
                    state = "error",
                    message = "信息不完整"
                });
            }
        }
        /// <summary>
        /// 更改角色状态
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <param name="state">角色状态</param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "UpdateRole", ActionUrl = "Role/RoleList", ActionResultType = 1)]
        public JsonResult UpdateState(int id = 0, int state = 0) {
            EnterRepository.GetRepositoryEnter().GetAuthoryRepository.EditEntity(new Model.Authory() { Id = id, State = (byte)state }, new string[] { "State" });
            PublicFunction.AddOperation(2, string.Format("修改角色状态"), string.Format("修改角色状态成功"));
            if (EnterRepository.GetRepositoryEnter().SaveChange() > 0) {
                return Json(new { state = "success", message = "修改角色状态成功" });
            }
            else {
                PublicFunction.AddOperation(2, string.Format("修改角色状态"), string.Format("修改角色状态失败"));
                EnterRepository.GetRepositoryEnter().SaveChange();
                return Json(new { state = "error", message = "服务器泡妞去了" });
            }
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "DeleteRole", ActionUrl = "Role/RoleList", ActionResultType = 1)]
        public JsonResult DeleteRole(int id = 0) {
            EnterRepository.GetRepositoryEnter().GetAuthoryRepository.DeleteEntity(new Model.Authory() { Id = id });
            PublicFunction.AddOperation(4, string.Format("删除角色"), string.Format("删除角色成功"));
            if (EnterRepository.GetRepositoryEnter().SaveChange() > 0) {
                return Json(new { state = "success", message = "删除角色成功" });
            }
            else {
                PublicFunction.AddOperation(4, string.Format("删除角色"), string.Format("删除角色失败"));
                EnterRepository.GetRepositoryEnter().SaveChange();
                return Json(new { state = "error", message = "服务器泡妞去了" });
            }
        }

        #region 角色页面按钮编辑
        /// <summary>
        /// 查看角色页面按钮
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "LookActionRole", ActionUrl = "Role/RoleList", ActionResultType = 1)]
        public JsonResult RolePageActionDesc(int id = 0) {
            IDAL.IAuthoryToPageRepository repository = EnterRepository.GetRepositoryEnter().GetAuthoryToPageRepository;
            List<Model.AuthoryToPage> authoryPageList = repository.LoadEntities(m => m.AuthoryId == id).ToList();
            return Json(new { state = "success", authoryPageList });
        }

        /// <summary>
        /// 角色页面按钮编辑
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "LookActionRole", ActionUrl = "Role/RoleList", ActionResultType = 1)]
        public JsonResult UpdatePageAction(int id, List<Models.RolePageModel> model) {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            IDAL.IAuthoryToPageRepository repository = EnterRepository.GetRepositoryEnter().GetAuthoryToPageRepository;
            foreach (var item in model) {
                AuthorDesign.Model.AuthoryToPage authoryPageModel = repository.LoadEntities(m => m.AuthoryId == id && m.PageId == item.PageId).FirstOrDefault();
                if (authoryPageModel == null) {
                    authoryPageModel = new Model.AuthoryToPage();
                    authoryPageModel.AuthoryId = id;
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
                    authoryPageModel.AuthoryId = id;
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
            PublicFunction.AddOperation(2, string.Format("编辑角色页面按钮"), string.Format("编辑角色页面按钮成功"));
            if (EnterRepository.GetRepositoryEnter().SaveChange() > 0) {
                CacheHelper.RemoveCache("AdminMenuList");
                //将管理员对应按钮表将 角色Id对应的角色页面按钮 IsShow为0的页面 删除
                EnterRepository.GetRepositoryEnter().GetAdminToPageRepository.DeleteByAuthoryId(id);
                return Json(new { state = "success", message = "编辑角色页面按钮成功" });
            }
            else {
                PublicFunction.AddOperation(2, string.Format("编辑角色页面按钮"), string.Format("编辑角色页面按钮失败"));
                EnterRepository.GetRepositoryEnter().SaveChange();
                return Json(new { state = "fail", message = "编辑角色页面按钮失败" });
            }
        }
        #endregion
    }
}
