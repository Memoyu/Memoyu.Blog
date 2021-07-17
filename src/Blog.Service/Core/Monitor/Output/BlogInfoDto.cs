using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Core.Monitor.Output
{
    public class BlogInfoDto
    {
        /// <summary>
        /// 文章总数
        /// </summary>
        public long ArticleTotal { get; set; }

        /// <summary>
        /// 分类总数
        /// </summary>
        public long CategoryTotal { get; set; }

        /// <summary>
        /// 标签总数
        /// </summary>
        public long TagTotal { get; set; }

        /// <summary>
        /// 留言总数
        /// </summary>
        public long CommentTotal { get; set; }
    }
}
