/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：UserResponse
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/6/28 12:51:18
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Memoyu.Blog.ToolKits.GitHub
{
    public class UserResponse
    {
        public string Login { get; set; }

        public int Id { get; set; }

        public string Avatar_url { get; set; }

        public string Html_url { get; set; }

        public string Repos_url { get; set; }

        public string Name { get; set; }

        public string Company { get; set; }

        public string Blog { get; set; }

        public string Location { get; set; }

        public string Email { get; set; }

        public string Bio { get; set; }

        public int Public_repos { get; set; }
    }
}
