using AuthorDesign.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.IDAL {
    public interface IPageActionRepository : IBaseRepository<PageAction> {
        /// <summary>
        /// 获取页面按钮列表
        /// </summary>
        /// <param name="startNum">起始数字</param>
        /// <param name="pageSize">页长</param>
        /// <param name="isDesc">是否倒叙排序</param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        IQueryable<PageAction> LoadPageList(int startNum, int pageSize, bool isDesc, out int rowCount);
    }
}
