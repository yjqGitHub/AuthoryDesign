using AuthorDesign.IDAL;
using AuthorDesign.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.DAL {
    public class ActionToPageRepository : BaseRepository<ActionToPage> ,IActionToPageRepository{
        /// <summary>
        /// 根据页面Id删除关联表
        /// </summary>
        /// <param name="pageId">页面Id</param>
        /// <returns></returns>
        public bool DeleteByPageId(int pageId) {
            var result = from p in db.Set<ActionToPage>()
                         where p.PageId == pageId
                         select new { p.Id };
            foreach (var item in result) {
                ActionToPage delMember = new ActionToPage() {
                    Id = item.Id
                };
                DbEntityEntry<ActionToPage> entry = db.Entry<ActionToPage>(delMember);
                entry.State = EntityState.Deleted;
            }
            return true;
        }
    }
}
