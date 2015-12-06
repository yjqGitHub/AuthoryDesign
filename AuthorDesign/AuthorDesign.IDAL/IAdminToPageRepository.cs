using AuthorDesign.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.IDAL {
    public interface IAdminToPageRepository : IBaseRepository<AdminToPage> {
        /// <summary>
        /// 根据角色Id删除管理员对应页面按钮
        /// </summary>
        /// <param name="authoryId">角色Id</param>
        /// <returns></returns>
        bool DeleteByAuthoryId(int authoryId);
    }
}
