using AuthorDesign.Common;
using AuthorDesign.Web.App_Start.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthorDesign.Web.Areas.Admin.Controllers {
    [AdminAuthory]
    public class PageActionController : Controller {
        //
        // GET: /Admin/PageAction/
         [AdminActionMethod(RoleCode = "LookRole", ActionUrl = "PageAction/PageActionList")]
        public ActionResult PageActionList() {
            ViewBag.Title = "页面按钮列表";
            return View();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PageActionList(Models.ButtonParamModel model) {
            int rowCount = 0;
            var result = EnterRepository.GetRepositoryEnter().GetPageActionRepository.LoadPageList(model.iDisplayStart, model.iDisplayLength, model.IsDesc, out rowCount);
            return Json(new {
                sEcho = model.sEcho,
                iTotalRecords = rowCount,
                iTotalDisplayRecords = rowCount,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取按钮信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "LookRole", ActionUrl = "PageAction/PageActionList", ActionResultType = 1)]
        public JsonResult GetPageActionInfo(int id = 0) {
            var result = EnterRepository.GetRepositoryEnter().GetPageActionRepository.LoadEntities(m => m.Id == id).FirstOrDefault();
            if (result == null) {
                return Json(new { state = "error", message = "按钮不存在" });
            }
            else {
                return Json(new { state = "success", result });
            }
        }
        /// <summary>
        /// 添加页面按钮
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "LookRole", ActionUrl = "PageAction/PageActionList", ActionResultType = 1)]
        public JsonResult AddPageAction(Models.PageActionModel model) {
            if (ModelState.IsValid) {
                IDAL.IPageActionRepository pageActionRepository = EnterRepository.GetRepositoryEnter().GetPageActionRepository;
                //判断页面按钮代码是否已存在
                var result = pageActionRepository.LoadEntities(m => m.ActionCode == model.ActionCode.Trim()).FirstOrDefault();
                if (result == null) {
                    pageActionRepository.AddEntity(new Model.PageAction() {
                        ActionCode = model.ActionCode,
                        ActionLevel = model.ActionLevel,
                        IsShow = model.IsShow,
                        Name = model.Name
                    });
                    //添加下操作记录
                    PublicFunction.AddOperation(1, string.Format("添加页面按钮"), string.Format("添加角色=={0}==页面按钮成功", model.Name));
                    if (EnterRepository.GetRepositoryEnter().SaveChange() > 0) {
                        CacheHelper.RemoveCache("ActionCodeList");
                        return Json(new {
                            state = "success",
                            message = "添加页面按钮成功"
                        });
                    }
                    else {
                        PublicFunction.AddOperation(1, string.Format("添加页面按钮"), string.Format("添加页面按钮=={0}==失败", model.Name));
                        EnterRepository.GetRepositoryEnter().SaveChange();
                        return Json(new {
                            state = "error",
                            message = "添加页面按钮失败"
                        });
                    }
                }
                else {
                    return Json(new {
                        state = "error",
                        message = "页面按钮代码已经存在了"
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
        /// 修改页面按钮
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "UpdateRole", ActionUrl = "PageAction/PageActionList", ActionResultType = 1)]
        public JsonResult UpdatePageAction(Models.PageActionModel model) {
            if (ModelState.IsValid && model.Id > 0) {
                IDAL.IPageActionRepository pageActionRepository = EnterRepository.GetRepositoryEnter().GetPageActionRepository;
                //判断权限名称是否已存在
                var result = pageActionRepository.LoadEntities(m => m.ActionCode == model.ActionCode.Trim()).FirstOrDefault();
                if (result != null && result.Id != model.Id) {
                    return Json(new {
                        state = "error",
                        message = "页面按钮代码已经存在了"
                    });
                }
                else {
                    Model.PageAction pageAction = new Model.PageAction() {
                        ActionCode = model.ActionCode,
                        ActionLevel = model.ActionLevel,
                        IsShow = model.IsShow,
                        Name = model.Name,
                        Id = model.Id
                    };
                    pageActionRepository.Get(m => m.Id == model.Id);
                    pageActionRepository.EditEntity(pageAction, new string[] { "ActionCode", "ActionLevel", "IsShow", "Name" });
                    PublicFunction.AddOperation(1, string.Format("修改页面按钮"), string.Format("修改页面按钮=={0}==成功", model.Name));
                    if (EnterRepository.GetRepositoryEnter().SaveChange() > 0) {
                        CacheHelper.RemoveCache("ActionCodeList");
                        return Json(new {
                            state = "success",
                            message = "修改页面按钮成功"
                        });
                    }
                    else {
                        PublicFunction.AddOperation(1, string.Format("修改页面按钮"), string.Format("修改页面按钮=={0}==失败", model.Name));
                        EnterRepository.GetRepositoryEnter().SaveChange();
                        return Json(new {
                            state = "error",
                            message = "修改页面按钮失败"
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
        /// 更改按钮状态
        /// </summary>
        /// <param name="id">按钮Id</param>
        /// <param name="state">按钮状态</param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionMethod(RoleCode = "UpdateRole", ActionUrl = "PageAction/PageActionList", ActionResultType = 1)]
        public JsonResult UpdateState(int id = 0, int state = 0) {
            EnterRepository.GetRepositoryEnter().GetPageActionRepository.EditEntity(new Model.PageAction() { Id = id, IsShow = (byte)state }, new string[] { "IsShow" });
            PublicFunction.AddOperation(1, string.Format("修改按钮状态"), string.Format("修改按钮状态成功"));
            if (EnterRepository.GetRepositoryEnter().SaveChange() > 0) {
                CacheHelper.RemoveCache("ActionCodeList");
                return Json(new { state = "success", message = "修改按钮状态成功" });
            }
            else {
                PublicFunction.AddOperation(1, string.Format("修改按钮状态"), string.Format("修改按钮状态失败"));
                EnterRepository.GetRepositoryEnter().SaveChange();
                return Json(new { state = "error", message = "服务器泡妞去了" });
            }
        }
    }
}
