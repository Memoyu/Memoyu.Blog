using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Domains.Common.Base;
using Blog.Core.Domains.Common.Consts;
using FreeSql.DataAnnotations;

namespace Blog.Core.Domains.Entities.Blog
{
    /// <summary>
    /// 发表文章表
    /// </summary>
    [Table(Name = SystemConst.DbTablePrefix + "_comment")]
    public class CommentEntity : FullAduitEntity
    {  /// <summary>
       /// 回复评论Id
       /// </summary>
        public long? RespId { get; set; }

        /// <summary>
        /// 根回复id
        /// </summary>
        public long? RootCommentId { get; set; }

        public int ChildCount { get; set; } = 0;

        /// <summary>
        /// 被回复的用户Id
        /// </summary>
        public long? RespUserId { get; set; }

        /// <summary>
        /// 回复的文本内容
        /// </summary>
        [Column(StringLength = 500)]
        public string Text { get; set; }

        /// <summary>
        /// 点赞量
        /// </summary>
        public int LikesQuantity { get; set; }

        /// <summary>
        /// 是否已审核
        /// </summary>
        public bool? IsAudit { get; set; } = true;

        /// <summary>
        /// 关联文章id
        /// </summary>
        public long ArticleId { get; set; }
    }
}
