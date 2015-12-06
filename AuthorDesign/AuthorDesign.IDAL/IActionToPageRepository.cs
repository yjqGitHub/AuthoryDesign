using AuthorDesign.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.IDAL {
    public interface IActionToPageRepository : IBaseRepository<ActionToPage> {
        /// <summary>
        /// 根据页面Id删除关联表
        /// </summary>
        /// <param name="pageId">页面Id</param>
        /// <returns></returns>
        bool DeleteByPageId(int pageId);
    }
}
