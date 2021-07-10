using Blog.Core.Domains.Common.Base;
using Blog.Core.Domains.Common.Consts;
using FreeSql.DataAnnotations;

namespace Blog.Core.Domains.Entities.Blog
{
    /// <summary>
    /// 文章对应标签表
    /// </summary>
    [Table(Name = SystemConst.DbTablePrefix + "_article_tag")]
    public class ArticleTagEntity : FullAduitEntity
    {
        /// <summary>
        /// 文章Id
        /// </summary>
        public long ArticleId { get; set; }

        /// <summary>
        /// 标签Id
        /// </summary>
        public long TagId { get; set; }

        [Navigate("ArticleId")]
        public ArticleEntity Article { get; set; }

        [Navigate("TagId")]
        public TagEntity Tag { get; set; }
    }
}
