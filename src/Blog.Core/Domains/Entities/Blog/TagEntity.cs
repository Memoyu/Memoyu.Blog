using System.Collections.Generic;
using Blog.Core.Domains.Common.Base;
using Blog.Core.Domains.Common.Consts;
using FreeSql.DataAnnotations;

namespace Blog.Core.Domains.Entities.Blog
{
    /// <summary>
    /// 标签表
    /// </summary>
    [Table(Name = SystemConst.DbTablePrefix + "_tag")]
    public class TagEntity : FullAduitEntity
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        [Column(StringLength = 50)]
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        [Navigate(ManyToMany = typeof(ArticleTagEntity))]
        public virtual ICollection<ArticleEntity> Articles { get; set; }
    }
}
