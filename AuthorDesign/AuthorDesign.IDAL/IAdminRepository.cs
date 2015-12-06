using AuthorDesign.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.IDAL
{
    public interface IAdminRepository : IBaseRepository<Admin>
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
        IQueryable<dynamic> LoadPageList(int roleId, string mobile, int startNum, int pageSize, bool IsDesc, out int rowCount);
        /// <summary>
        /// 添加时判断用户名或者邮箱或者手机号码是否存在（修改时另写）
        /// </summary>
        /// <param name="adminName">用户名</param>
        /// <param name="mobile">手机号码</param>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        Admin GetFirstAdmin(string adminName, string mobile, string email);
    }
}
