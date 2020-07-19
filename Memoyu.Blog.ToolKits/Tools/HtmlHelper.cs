/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   命名空间 ：Memoyu.Blog.ToolKits.Tools
*   文件名称 ：HtmlHelper
*   =================================
*   创 建 者 ：Memoyu
*   电子邮箱 ：mmy6076@outlook.com
*   创建日期 ：2020-07-19 14:43:56
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using System;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.FileSystemGlobbing;

namespace Memoyu.Blog.ToolKits.Tools
{
    public class HtmlHelper
    {
        /// <summary>
        /// 提取摘要，是否清除HTML代码
        /// </summary>
        /// <param name="content"></param>
        /// <param name="length"></param>
        /// <param name="stripHTML"></param>
        /// <returns></returns>
        public static string GetContentSummary(string content, int length, bool stripHTML)
        {
            if (string.IsNullOrEmpty(content) || length == 0) return "";
            if (stripHTML)
            {

                Regex regExScript = new Regex("<script[^>]*?>[\\s\\S]*?<\\/script>"); // script
                Regex regExStyle = new Regex("<style[^>]*?>[\\s\\S]*?<\\/style>"); // style
                Regex regExHtml = new Regex("<[^>]+>"); // HTML tag
                Regex regExSpace = new Regex("\\s+|\t|\r|\n");// other characters

                content = regExScript.Replace(content, "");
                content = regExStyle.Replace(content, "");
                content = regExHtml.Replace(content, "");
                content = regExSpace.Replace(content, "");

                content = content.Replace("　", "").Replace(" ", "");
                if (content.Length <= length)
                    return content;
                else
                    return content.Substring(0, length) + "……";
            }
            else
            {
                if (content.Length <= length)
                    return content; int pos = 0, npos = 0, size = 0;
                bool firststop = false, notr = false, noli = false;
                StringBuilder sb = new StringBuilder();
                while (true)
                {
                    if (pos >= content.Length) break;
                    string cur = content.Substring(pos, 1);
                    if (cur == "<")
                    {
                        string next = content.Substring(pos + 1, 3).ToLower();
                        if (next.IndexOf("p") == 0 && next.IndexOf("pre") != 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                        }
                        else if (next.IndexOf("/p") == 0 && next.IndexOf("/pr") != 0)
                        {
                            npos = content.IndexOf(">", pos) + 1; if (size < length) sb.Append("<br/>");
                        }
                        else if (next.IndexOf("br") == 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length) sb.Append("<br/>");
                        }
                        else if (next.IndexOf("img") == 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length)
                            {
                                sb.Append(content.Substring(pos, npos - pos)); size += npos - pos + 1;
                            }
                        }
                        else if (next.IndexOf("li") == 0 || next.IndexOf("/li") == 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length)
                            {
                                sb.Append(content.Substring(pos, npos - pos));
                            }
                            else
                            {
                                if (!noli && next.IndexOf("/li") == 0)
                                {
                                    sb.Append(content.Substring(pos, npos - pos)); noli = true;
                                }
                            }
                        }
                        else if (next.IndexOf("tr") == 0 || next.IndexOf("/tr") == 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length)
                            {
                                sb.Append(content.Substring(pos, npos - pos));
                            }
                            else
                            {
                                if (!notr && next.IndexOf("/tr") == 0)
                                {
                                    sb.Append(content.Substring(pos, npos - pos)); notr = true;
                                }
                            }
                        }
                        else if (next.IndexOf("td") == 0 || next.IndexOf("/td") == 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length)
                            {
                                sb.Append(content.Substring(pos, npos - pos));
                            }
                            else
                            {
                                if (!notr)
                                {
                                    sb.Append(content.Substring(pos, npos - pos));
                                }
                            }
                        }
                        else { npos = content.IndexOf(">", pos) + 1; sb.Append(content.Substring(pos, npos - pos)); }
                        if (npos <= pos) npos = pos + 1; pos = npos;
                    }
                    else
                    {
                        if (size < length)
                        {
                            sb.Append(cur); size++;
                        }
                        else
                        {
                            if (!firststop)
                            {
                                sb.Append("……"); firststop = true;
                            }
                        }
                        pos++;
                    }
                }
                return sb.ToString();
            }
        }
    }
}
