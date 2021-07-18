using Blog.Core.Domains.Common.Base;
using Blog.Core.Domains.Common.Consts;
using FreeSql.DataAnnotations;

namespace Blog.Core.Domains.Entities.Blog
{
    /// <summary>
    /// 友情链接表
    /// </summary>
    [Table(Name = SystemConst.DbTablePrefix + "_friend_link")]
    public class FriendLinkEntity : FullAduitEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Column(StringLength = 100)]
        public string Title { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [Column(StringLength = 200)]
        public string Desc { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        [Column(StringLength = 200)]
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        [Column(StringLength = 100)]
        public string LinkUrl { get; set; }

        /// <summary>
        /// 链接类型：0：友链；1：推荐网站
        /// </summary>
        public int Type { get; set; }
    }
}
