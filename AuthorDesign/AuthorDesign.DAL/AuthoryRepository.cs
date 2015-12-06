using AuthorDesign.IDAL;
using AuthorDesign.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.DAL {
    public class AuthoryRepository : BaseRepository<Authory>, IAuthoryRepository {
        /// <summary>
        /// 获取角色分页列表
        /// </summary>
        /// <param name="startNum">起始数字</param>
        /// <param name="pageSize">页长</param>
        /// <param name="isDesc">是否倒序排列</param>
        /// <param name="rowCount">总页数</param>
        /// <returns>角色分页列表</returns>
        public IQueryable<Authory> LoadPageList(int startNum, int pageSize, bool isDesc, out int rowCount) {
            rowCount = 0;
            var result = from p in db.Set<Authory>()
                         select p;
            if (isDesc) {
                result = result.OrderByDescending(m => m.OrderNum);
            }
            else {
                result = result.OrderBy(m => m.OrderNum);
            }
            rowCount = result.Count();
            return result.Skip(startNum).Take(pageSize);
        }
        /// <summary>
        /// 获取权限选择列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Authory> LoadAllRole() {
            var result = from p in db.Set<Authory>()
                         orderby p.OrderNum
                         select new { p.Name,p.Id};
            return result.ToList().Select(m => new Authory() {  Name=m.Name,Id=m.Id});
        }
    }
}
