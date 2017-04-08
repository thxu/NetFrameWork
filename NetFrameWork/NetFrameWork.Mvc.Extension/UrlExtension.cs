using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NetFrameWork.Mvc.Extension
{
    /// <summary>
    /// Url扩展
    /// </summary>
    public static class UrlExtension
    {
        /// <summary>
        /// Url添加版本号 
        /// </summary>
        /// <param name="urlHelper">urlHelper</param>
        /// <param name="url">url</param>
        /// <returns>结果</returns>
        public static string ContentVersion(this UrlHelper urlHelper, string url)
        {
            var version = ConfigurationManager.AppSettings["version"];
            return ContentVersion(urlHelper, url, version);
        }

        /// <summary>
        /// Url添加版本号 
        /// </summary>
        /// <param name="urlHelper">urlHelper</param>
        /// <param name="url">url</param>
        /// <param name="version">版本号</param>
        /// <returns>结果</returns>
        public static string ContentVersion(this UrlHelper urlHelper, string url, string version)
        {
            return urlHelper.Content(url) + "?version=" + version;
        }
    }
}
