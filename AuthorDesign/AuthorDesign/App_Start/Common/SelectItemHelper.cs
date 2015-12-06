using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace AuthorDesign.Web.App_Start.Common
{
    /// <summary>
    /// 选择框列表帮助
    /// </summary>
    public class SelectItemHelper
    {
        /// <summary>
        /// 获取权限选择框列表
        /// </summary>
        /// <param name="nowId">当前权限Id</param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetRoleItemList(int nowId) {
            IEnumerable<Model.Authory> authoryEnu= EnterRepository.GetRepositoryEnter().GetAuthoryRepository.LoadAllRole();
            List<SelectListItem> itemList = new List<SelectListItem>() { new SelectListItem(){ Text="请选择权限",Value="-1"}};
            foreach (var item in authoryEnu) {
                itemList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name, Selected = item.Id == nowId });
            }
            return itemList;
        }
    }
}