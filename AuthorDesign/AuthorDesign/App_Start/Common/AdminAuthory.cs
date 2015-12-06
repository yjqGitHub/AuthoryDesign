using AuthorDesign.Web.App_Start.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace System.Web.Mvc {
    #region 管理员权限控制
    public class AdminAuthory : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            //用MVC系统自带的功能 获取当前方法上的特性名称
            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(NoNeedAdminAuthory), inherit: true)
                                     || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(NoNeedAdminAuthory), inherit: true);
            if (skipAuthorization) {
                return;
            }
            //检查是否登录
            if (!WebCookieHelper.AdminCheckLogin()) {
                filterContext.Result = new RedirectResult("~/Admin/Account/Login", true);
                return;
            }
            //如果是超级管理就免去验证
            if (WebCookieHelper.GetAdminId(5) == 1) {
                return;
            }
            //页面权限验证开始
            var customAttributes = filterContext.ActionDescriptor.GetCustomAttributes(true);
            if (customAttributes != null && customAttributes.Length > 0) {
                for (int i = 0; i < customAttributes.Count(); i++) {
                    if (customAttributes.GetValue(i).GetType().Name == "AdminActionMethod") {//判断anction特性名称
                        string actionCode = (customAttributes[i] as AdminActionMethod).RoleCode;//获取特性功能按钮代码
                        string actionUrl = (customAttributes[i] as AdminActionMethod).ActionUrl;//获取特性功能地址
                        int actionResultType = (customAttributes[i] as AdminActionMethod).ActionResultType;//获取返回视图类型
                        if (actionCode == "NoNeedAuthory") {//不需要权限认证
                            return;
                        }
                        else { //判断权限是否符合
                            List<AuthorDesign.Model.AdminPageAction> pageActionList = AdminMenuHelper.GetNowAdminMenu();
                            var pageSelect = pageActionList.Where(m => m.PageUrl == actionUrl);
                            if (pageSelect != null && pageSelect.Count() > 0) { //判断有无执行该页面的权利
                                //判断有误执行该动作权利
                                var codeList = AdminMenuHelper.LoadActionCodeList();
                                //先根据动作按钮代码查找到代码所在按钮Id
                                var codeSelect = codeList.Where(m => m.ActionCode == actionCode);
                                if (codeSelect != null && codeSelect.Count() > 0) {
                                    int codeId = codeSelect.First().Id;
                                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                                    //判断codeId在角色动作列表中是否为选择状态
                                    string roleActionList = pageSelect.First().RoleActionList;

                                    List<AuthorDesign.Web.Areas.Admin.Models.RolePageActionModel> roleActionListModel = serializer.Deserialize<List<AuthorDesign.Web.Areas.Admin.Models.RolePageActionModel>>(roleActionList);
                                    if (roleActionListModel != null && roleActionListModel.Where(m => m.ActionId == codeId && m.actionChecked == 1).Count() > 0) {
                                        //判断CodeId在管理员动作列表中是否为选择状态
                                        List<AuthorDesign.Web.Areas.Admin.Models.RolePageActionModel> adminActionListModel = serializer.Deserialize<List<AuthorDesign.Web.Areas.Admin.Models.RolePageActionModel>>(pageSelect.First().AdminActionList);
                                        if (adminActionListModel != null && adminActionListModel.Where(m => m.ActionId == codeId && m.actionChecked == 1).Count() > 0) {
                                            StringBuilder sb = new StringBuilder();
                                            sb.Append("[");
                                            //传递在该页面可执行的按钮
                                            foreach (var item in roleActionListModel) {
                                                if (item.actionChecked == 1) {
                                                    var needChangeAction = adminActionListModel.Where(m => m.ActionId == item.ActionId).FirstOrDefault();
                                                    if (needChangeAction == null) {
                                                        item.actionChecked = 0;
                                                    }
                                                    else if (needChangeAction.actionChecked == 0) {
                                                        item.actionChecked = 0;
                                                    }
                                                }
                                                AuthorDesign.Model.PageAction OneAction = codeList.Where(m => m.Id == item.ActionId).FirstOrDefault();
                                                sb.Append("{").Append("\"").Append("ActionName").Append("\"").Append(":").Append("\"").Append(OneAction == null ? "" : OneAction.ActionCode).Append("\"").Append(",").Append("\"").Append("IsChecked").Append("\"").Append(":").Append(item.actionChecked).Append("}").Append(",");

                                            }
                                            sb.Remove(sb.Length - 1, 1);
                                            sb.Append("]");
                                            filterContext.Controller.ViewBag.CanOperationActionList = sb.ToString();
                                        }
                                        else {
                                            if (actionResultType == 0) {
                                                filterContext.Result = new RedirectResult("~/Admin/Home/NoAuthory", true);
                                            }
                                            else if (actionResultType == 1) {
                                                filterContext.Result = new JsonResult() { Data = new { state = "error", message = "您暂无权限操作" } };
                                            }
                                            return;
                                        }
                                    }
                                    else {
                                        if (actionResultType == 0) {
                                            filterContext.Result = new RedirectResult("~/Admin/Home/NoAuthory", true);
                                        }
                                        else if (actionResultType == 1) {
                                            filterContext.Result = new JsonResult() { Data = new { state = "error", message = "您暂无权限操作" } };
                                        }
                                        return;
                                    }

                                }
                                else {
                                    if (actionResultType == 0) {
                                        filterContext.Result = new RedirectResult("~/Admin/Home/NoAuthory", true);
                                    }
                                    else if (actionResultType == 1) {
                                        filterContext.Result = new JsonResult() { Data = new { state = "error", message = "您暂无权限操作" } };
                                    }
                                    return;
                                }
                            }
                            else {
                                if (actionResultType == 0) {
                                    filterContext.Result = new RedirectResult("~/Admin/Home/NoAuthory", true);
                                }
                                else if (actionResultType == 1) {
                                    filterContext.Result = new JsonResult() { Data = new { state = "error", message = "您暂无权限操作" } };
                                }
                                return;
                            }
                        }
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class NoNeedAdminAuthory : Attribute {
    }

    /// <summary>
    /// 表示当前Action请求为一个具体的功能页面
    /// </summary>
    public class AdminActionMethod : Attribute {
        /// <summary>
        /// 页面请求路径
        /// </summary>
        public string ActionUrl { get; set; }
        /// <summary>
        /// 页面操作代码
        /// </summary>
        public string RoleCode { get; set; }
        /// <summary>
        /// 页面返回类型（0：返回页面，1返回json格式）
        /// </summary>
        public int ActionResultType { get; set; }
    }
    #endregion
}