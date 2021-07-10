using System;
using System.Collections.Generic;
using Blog.Service.Blog.Category.Output;
using Blog.Service.Blog.Tag.Output;

namespace Blog.Service.Blog.Article.Output
{
    public class ArticleDto
    {
        public long Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 标题颜色
        /// </summary>
        public string TitleColor { get; set; }

        /// <summary>
        /// 副标题
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop { get; set; }

        /// <summary>
        /// 点击数
        /// </summary>
        public int Hits { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public int Comments { get; set; }

        /// <summary>
        /// 缩略图地址
        /// </summary>
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public CategoryDto Category { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public List<TagDto> Tags { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
