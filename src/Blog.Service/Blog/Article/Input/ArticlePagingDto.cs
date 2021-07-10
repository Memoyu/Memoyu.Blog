using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Domains.Common;

namespace Blog.Service.Blog.Article.Input
{
    public class ArticlePagingDto : PagingDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        public long CategoryId { get; set; }

        /// <summary>
        /// 创建时间起
        /// </summary>
        public DateTime? CreateTimeStart { get; set; }

        /// <summary>
        /// 创建时间止
        /// </summary>
        public DateTime? CreateTimeEnd { get; set; }
    }
}
