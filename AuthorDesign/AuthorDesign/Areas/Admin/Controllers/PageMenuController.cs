using AuthorDesign.Common;
using AuthorDesign.Web.App_Start.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthorDesign.Web.Areas.Admin.Controllers {
    [AdminAuthory]
    public class PageMenuController : Controller {
        //
        // GET: /Admin/PageMenu/
        [AdminActionMethod(RoleCode = "LookRole", ActionUrl = "PageMenu/PageMenuList")]
        public ActionResult PageMenuList() {
            ViewBag.Title = "页面菜单列表";
            return View();
        }
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="listPage"></param>
        /// <param name="pid"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult ShowMenuList(List<Model.PageMenu> listPage, int pid = 0, int layer = 0) {
            ViewBag.Pid = pid;
            ViewBag.Layer = layer;
            return View(listPage);
        }
        [ChildActionOnly]
        public ActionResult ShowMenuSelectList(List<Model.PageMenu> listPage, int pid = 0, int layer = 0) {
            ViewBag.Pid = pid;
            ViewBag.Layer = layer;
            return View(listPage);
        }
        /// <summary>
        /// 获取页面信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "LookRole", ActionUrl = "PageMenu/PageMenuList", ActionResultType = 1)]
        public JsonResult GetPageMenuInfo(int id = 0) {
            var result = EnterRepository.GetRepositoryEnter().GetPageMenuRepository.LoadEntities(m => m.Id == id).FirstOrDefault();
            if (result == null) {
                return Json(new { state = "error", message = "页面不存在" });
            }
            else {
                return Json(new { state = "success", result });
            }
        }

        /// <summary>
        /// 添加页面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "LookRole", ActionUrl = "PageMenu/PageMenuList", ActionResultType = 1)]
        public JsonResult AddPageMenu(Models.PageMenuModel model) {
            if (ModelState.IsValid) {
                IDAL.IPageMenuRepository pageMenuRepository = EnterRepository.GetRepositoryEnter().GetPageMenuRepository;
                //判断权限名称是否已存在
                var result = pageMenuRepository.LoadEntities(m => m.Name == model.Name.Trim()).FirstOrDefault();
                if (result == null) {
                    pageMenuRepository.AddEntity(new Model.PageMenu() {
                        Ico = model.Ico,
                        IsShow = model.IsShow,
                        Name = model.Name,
                        OrderNum = model.OrderNum,
                        PageUrl = model.PageUrl,
                        PId = model.PId
                    });
                    //添加下操作记录
                    PublicFunction.AddOperation(1, string.Format("添加页面"), string.Format("添加页面=={0}==成功", model.Name));
                    if (EnterRepository.GetRepositoryEnter().SaveChange() > 0) {
                        CacheHelper.RemoveCache("SuperAdminMenuList");
                        return Json(new {
                            state = "success",
                            message = "添加页面成功"
                        });
                    }
                    else {
                        PublicFunction.AddOperation(1, string.Format("添加页面"), string.Format("添加页面=={0}==失败", model.Name));
                        EnterRepository.GetRepositoryEnter().SaveChange();
                        return Json(new {
                            state = "error",
                            message = "添加页面失败"
                        });
                    }
                }
                else {
                    return Json(new {
                        state = "error",
                        message = "页面名称已经存在了"
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
        /// 修改页面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "UpdateRole", ActionUrl = "PageMenu/PageMenuList", ActionResultType = 1)]
        public JsonResult UpdatePageMenu(Models.PageMenuModel model) {
            if (ModelState.IsValid && model.Id > 0) {
                if (model.Id == model.PId) {
                    return Json(new { state = "error", message = "不能讲自己作为自己的父类" });
                }
                IDAL.IPageMenuRepository pageMenuRepository = EnterRepository.GetRepositoryEnter().GetPageMenuRepository;
                var result = pageMenuRepository.LoadEntities(m => m.Name == model.Name.Trim()).FirstOrDefault();
                if (result != null && result.Id != model.Id) {
                    return Json(new {
                        state = "error",
                        message = "页面名称已经存在了"
                    });
                }
                else {
                    Model.PageMenu pageMenu = new Model.PageMenu() {
                        Ico = model.Ico,
                        IsShow = model.IsShow,
                        Name = model.Name,
                        OrderNum = model.OrderNum,
                        PageUrl = model.PageUrl,
                        PId = model.PId,
                        Id = model.Id
                    };
                    pageMenuRepository.Get(m => m.Id == model.Id);
                    pageMenuRepository.EditEntity(pageMenu, new string[] { "Ico", "IsShow", "Name", "OrderNum", "PageUrl", "PId" });
                    PublicFunction.AddOperation(1, string.Format("修改页面"), string.Format("修改页面=={0}==成功", model.Name));
                    if (EnterRepository.GetRepositoryEnter().SaveChange() > 0) {
                        CacheHelper.RemoveCache("SuperAdminMenuList");
                        return Json(new {
                            state = "success",
                            message = "修改页面成功"
                        });
                    }
                    else {
                        PublicFunction.AddOperation(1, string.Format("修改页面"), string.Format("修改页面=={0}==失败", model.Name));
                        EnterRepository.GetRepositoryEnter().SaveChange();
                        return Json(new {
                            state = "error",
                            message = "修改页面失败"
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
        /// 删除页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "DeleteRole", ActionUrl = "PageMenu/PageMenuList", ActionResultType = 1)]
        public JsonResult DeletePageMenu(int id = 0) {
            EnterRepository.GetRepositoryEnter().GetPageMenuRepository.DeleteEntity(new Model.PageMenu() { Id = id });
            PublicFunction.AddOperation(1, string.Format("删除页面"), string.Format("删除页面成功"));
            //删除页面与按钮之间的关系表
            EnterRepository.GetRepositoryEnter().GetActionToPageRepository.DeleteByPageId(id);
            if (EnterRepository.GetRepositoryEnter().SaveChange() > 0) {
                CacheHelper.RemoveCache("SuperAdminMenuList");
                return Json(new { state = "success", message = "删除页面成功" });
            }
            else {
                PublicFunction.AddOperation(1, string.Format("删除页面"), string.Format("删除页面失败"));
                EnterRepository.GetRepositoryEnter().SaveChange();
                return Json(new { state = "error", message = "服务器泡妞去了" });
            }
        }
        /// <summary>
        /// 获取页面按钮
        /// </summary>
        /// <param name="id">页面Id</param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "LookActionRole", ActionUrl = "PageMenu/PageMenuList", ActionResultType = 1)]
        public JsonResult GetAction(int id = 0) {
            IDAL.IActionToPageRepository actionPageRepository = EnterRepository.GetRepositoryEnter().GetActionToPageRepository;
            var result = actionPageRepository.LoadEntities(m => m.PageId == id).FirstOrDefault();
            return Json(new { state="success",actionList=result==null?"":result.ActionList});
        }

        /// <summary>
        /// 修改页面按钮
        /// </summary>
        /// <param name="ActionListId">按钮Id，多个用逗号隔开</param>
        /// <param name="id">页面Id</param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "UpdateActionRole", ActionUrl = "PageMenu/PageMenuList", ActionResultType = 1)]
        public JsonResult UpdateAction(string ActionListId, int id = 0) {
            IDAL.IActionToPageRepository actionPageRepository= EnterRepository.GetRepositoryEnter().GetActionToPageRepository;
            var result = actionPageRepository.LoadEntities(m => m.PageId == id).FirstOrDefault();
            if (result == null) {
                Model.ActionToPage actionPage = new Model.ActionToPage() { ActionList = ActionListId, PageId = id, IsDelete = string.IsNullOrEmpty(ActionListId) ? (byte)1 : (byte)0 };
                actionPageRepository.AddEntity(actionPage);
                PublicFunction.AddOperation(1, string.Format("编辑页面与页面按钮"), string.Format("编辑页面与页面按钮成功"));
                if (EnterRepository.GetRepositoryEnter().SaveChange() > 0) {
                    return Json(new { state = "success", message = "添加页面按钮成功" });
                }
                else {
                    PublicFunction.AddOperation(1, string.Format("编辑页面与页面按钮"), string.Format("编辑页面与页面按钮失败"));
                    EnterRepository.GetRepositoryEnter().SaveChange();
                    return Json(new { state = "error", message = "服务器泡妞去了" });
                }
            }
            else {
                result.ActionList = ActionListId;
                result.IsDelete = string.IsNullOrEmpty(ActionListId) ? (byte)1 : (byte)0;
                PublicFunction.AddOperation(1, string.Format("编辑页面与页面按钮"), string.Format("编辑页面与页面按钮成功"));
                if (EnterRepository.GetRepositoryEnter().SaveChange() > 0) {
                    return Json(new { state = "success", message = "修改页面按钮成功" });
                }
                else {
                    PublicFunction.AddOperation(1, string.Format("编辑页面与页面按钮"), string.Format("编辑页面与页面按钮失败"));
                    EnterRepository.GetRepositoryEnter().SaveChange();
                    return Json(new { state = "error", message = "服务器泡妞去了" });
                }
            }
        }
    }
}
