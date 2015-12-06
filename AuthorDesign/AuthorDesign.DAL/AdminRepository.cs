using AuthorDesign.IDAL;
using AuthorDesign.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.DAL
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        /// <summary>
        /// 加载管理员列表
        /// </summary>
        /// <param name="roleId">权限Id</param>
        /// <param name="mobile">手机号码</param>
        /// <param name="startNum">起始数字</param>
        /// <param name="pageSize">页长</param>
        /// <param name="IsDesc">是否倒序排列</param>
        /// <param name="rowCount">总个数</param>
        /// <returns></returns>
        public IQueryable<dynamic> LoadPageList(int roleId, string mobile, int startNum, int pageSize, bool IsDesc, out int rowCount) {
            rowCount = 0;
            var result = from p in db.Set<Admin>()
                         join a in db.Set<Authory>() on new { AuthoryId = p.AuthoryId } equals new { AuthoryId = a.Id } into a_join
                         from a in a_join.DefaultIfEmpty()
                         where p.IsSuperAdmin == 0
                         select new {
                             p.Id,
                             p.IsLogin,
                             p.Mobile,
                             p.AdminName,
                             p.Email,
                             p.CreateTime,
                             a.Name,
                             p.AuthoryId,
                             p.LastLoginTime
                         };
            if (roleId > 0) {
                result = result.Where(m => m.AuthoryId == roleId);
            }
            if (!string.IsNullOrEmpty(mobile)) {
                result = result.Where(m => m.Mobile == mobile);
            }
            if (IsDesc) {
                result = result.OrderByDescending(m => m.CreateTime);
            }
            else {
                result = result.OrderBy(m => m.CreateTime);
            }
            rowCount = result.Count();
            return result.Skip(startNum).Take(pageSize);
        }

        /// <summary>
        /// 添加时判断用户名或者邮箱或者手机号码是否存在（修改时另写）
        /// </summary>
        /// <param name="adminName">用户名</param>
        /// <param name="mobile">手机号码</param>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        public Admin GetFirstAdmin(string adminName, string mobile, string email) {
            var result = (from admins in db.Set<Admin>()
                          where
                            admins.AdminName == adminName && string.IsNullOrEmpty(adminName)
                          select admins
                          ).Union(from admins in db.Set<Admin>()
                                  where
                                    admins.Mobile == mobile && string.IsNullOrEmpty(mobile)
                                  select admins
                          ).Union(from admins in db.Set<Admin>()
                                  where
                                    admins.Email == email && string.IsNullOrEmpty(email)
                                  select admins
                          ).FirstOrDefault();
            return result;
        }
    }
}
