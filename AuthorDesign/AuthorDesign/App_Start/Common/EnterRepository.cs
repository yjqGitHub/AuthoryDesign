using AuthorDesign.DAL;
using AuthorDesign.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace AuthorDesign.Web.App_Start.Common {
    public class EnterRepository {
        /// <summary>
        /// 获取DAL入口类
        /// </summary>
        /// <returns></returns>
        public static IRepositoryEnter GetRepositoryEnter() {
            IRepositoryEnter _enter = CallContext.GetData("CurrentRepositoryEnter") as RepositoryEnter;
            if (_enter == null) {
                _enter = new RepositoryEnter();
                CallContext.SetData("CurrentRepositoryEnter", _enter);
            }
            return _enter;
        }
    }
}