using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace AuthorDesign.Web.App_Start.Common {
    public class IpHelper {
        /// <summary>         
        /// 通过IP得到IP所在地省市（Porschev）         
        /// </summary>         
        /// <param name="ip"></param>         
        /// <returns></returns>         
        public static string GetAdrByIp(string ip) {
            string url = "http://www.cz88.net/ip/?ip=" + ip;
            string regStr = "(?<=<span\\s*id=\\\"cz_addr\\\">).*?(?=</span>)";

            //得到网页源码
            string html = GetHtml(url);
            Regex reg = new Regex(regStr, RegexOptions.None);
            Match ma = reg.Match(html);
            html = ma.Value;
            string[] arr = html.Split(' ');
            return arr[0];
        }
        /// <summary>
        /// 获取当前Ip所在地址
        /// </summary>
        /// <returns></returns>
        public static string GetAdrByIp() {
            string address = GetAdrByIp(GetRealIP());
            return address == "" ? "中国" : address;
        }

        /// <summary>         
        /// 获取HTML源码信息(Porschev)         
        /// </summary>         
        /// <param name="url">获取地址</param>         
        /// <returns>HTML源码</returns>         
        public static string GetHtml(string url) {
            string str = "";
            try {
                Uri uri = new Uri(url);
                WebRequest wr = WebRequest.Create(uri);
                Stream s = wr.GetResponse().GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.Default);
                str = sr.ReadToEnd();
            }
            catch (Exception e) {
            }
            return str;
        }

        /// <summary>
        /// 获得客户端的IP
        /// </summary>
        /// <returns>当前客户端的IP</returns>
        public static string GetRealIP() {
            string strIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (strIP == null || strIP.Length < 1) {
                strIP = HttpContext.Current.Request.UserHostAddress;
            }
            if (strIP == null || strIP.Length < 1) {
                return "0.0.0.0";
            }
            return strIP;
        }
        /// <summary>
        /// 获取浏览器的版本以及名称
        /// </summary>
        /// <returns></returns>
        public static string GetBrowerVersion() {
            string browerVersion = string.Empty;
            if (System.Web.HttpContext.Current != null) {
                System.Web.HttpBrowserCapabilities browser = System.Web.HttpContext.Current.Request.Browser;
                if (browser != null) {
                    browerVersion = browser.Browser + browser.Version;
                }
            }
            return browerVersion;
        }
    }
}