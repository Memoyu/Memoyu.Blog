using System;
using System.Collections.Generic;
using Blog.Core.Domains.Common.Base;
using Blog.Core.Domains.Common.Consts;
using FreeSql.DataAnnotations;

namespace Blog.Core.Domains.Entities.Blog
{
    /// <summary>
    /// 发表文章表
    /// </summary>
    [Table(Name = SystemConst.DbTablePrefix + "_article")]
    public class ArticleEntity : FullAduitEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Column(StringLength = 200)]
        public string Title { get; set; }

        /// <summary>
        /// 标题颜色
        /// </summary>
        public string TitleColor { get; set; }

        /// <summary>
        /// 副标题
        /// </summary>
        [Column(StringLength = 300)]
        public string Subtitle { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [Column(StringLength = 500)]
        public string Introduction { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [Column(StringLength = 50)]
        public string Author { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        [Column(StringLength = 300)]
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
        [Column(StringLength = 300)]
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        public long CategoryId { get; set; }


        [Navigate]
        public virtual CategoryEntity Category { get; set; }

        [Navigate(ManyToMany = typeof(ArticleTagEntity))]
        public virtual IEnumerable<TagEntity> Tags { get; set; }

        [Navigate("ArticleId")]
        public virtual ICollection<ArticleTagEntity> ArticleTags { get; set; }
    }
}
