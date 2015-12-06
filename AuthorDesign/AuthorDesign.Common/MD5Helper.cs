using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.Common {

    public class MD5Helper {
        /// <summary>
        /// MD5散列
        /// </summary>
        public static string MD5(string inputStr) {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashByte = md5.ComputeHash(Encoding.UTF8.GetBytes(inputStr));
            StringBuilder sb = new StringBuilder();
            foreach (byte item in hashByte)
                sb.Append(item.ToString("X").PadLeft(2, '0'));
            return sb.ToString();
        }
        /// <summary>
        /// 创建密码MD5
        /// </summary>
        /// <param name="password">原密码</param>
        /// <param name="salt">盐值</param>
        /// <returns></returns>
        public static string CreatePasswordMd5(string password, string salt) {
            return MD5(string.Format("{0}{1}",password,salt));
        }
    }
}
