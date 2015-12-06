using AuthorDesign.IDAL;
using AuthorDesign.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.DAL {
    public class AdminToPageRepository : BaseRepository<AdminToPage>, IAdminToPageRepository {
        /// <summary>
        /// 根据角色Id删除管理员对应页面按钮
        /// </summary>
        /// <param name="authoryId">角色Id</param>
        /// <returns></returns>
        public bool DeleteByAuthoryId(int authoryId) {
            var result = from admintopages in db.Set<AdminToPage>()
                         where
                             (from authorytopages in db.Set<AuthoryToPage>()
                              where
                                authorytopages.AuthoryId == authoryId &&
                                authorytopages.IsShow == 0
                              select new {
                                  authorytopages.PageId
                              }).Contains(new { admintopages.PageId })
                         select new {
                             admintopages.Id
                         };
           
                foreach (var item in result) {
                    DeleteEntity(new AdminToPage() { Id = item.Id });
                }
                return db.SaveChanges() > 0;
        }
    }
}
