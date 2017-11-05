using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Yinyue200.NavigationHelper
{
    public static class NavigationDictionaryHelper
    {
        /// <summary>
        /// 将内容转换为编码的URL字符串,不带?前缀
        /// </summary>
        /// <returns></returns>
        public static string ToPostString(this Dictionary<string,string> th)
        {
            StringBuilder sb = new StringBuilder();
            bool first = true;
            foreach (string a in th.Keys)
            {
                if (!first)
                {
                    sb.Append("&");
                }
                else
                {
                    first = false;
                }
                sb.Append(a);
                sb.Append("=");
                sb.Append(System.Net.WebUtility.UrlEncode(th[a]));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 将内容转换为编码的URL字符串,带?前缀
        /// </summary>
        /// <returns></returns>
        public static string ToQueryString(this Dictionary<string,string> th)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("?");
            bool first=true ;
            foreach (string a in th.Keys)
            {
                if(!first )
                {
                    sb.Append("&");
                }
                else
                {
                    first = false;
                }
                sb.Append(a);
                sb.Append("=");
                sb.Append(System.Net.WebUtility.UrlEncode(th[a]));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 从URL字符串初始化<see cref="NavigationDictionaryHelper"/>
        /// </summary>
        /// <param name="formatString"></param>
        /// <returns></returns>
        public static Dictionary<string,string> FormatFormString(string formatString)
        {
            if (formatString == null)
                throw new System.ArgumentNullException("url");

            System.Collections.Generic.Dictionary<string, string> newdic() => new System.Collections.Generic.Dictionary<string, string>();

            if (formatString == "")
                return newdic();

            int questionMarkIndex = formatString.IndexOf('?');

            if (questionMarkIndex == -1 || questionMarkIndex == formatString.Length - 1)
                return newdic();
            string ps = formatString.Substring(questionMarkIndex + 1);

            // 开始分析参数对    
            return new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?").Matches(ps).Cast<Match>().ToDictionary(m=> m.Result("$2").ToLowerInvariant(),m=> WebUtility.UrlDecode(m.Result("$3")));
        }
    }
}
