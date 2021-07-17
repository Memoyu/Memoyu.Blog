using Blog.Core.Domains.Common.Base;
using Blog.Core.Domains.Common.Consts;
using FreeSql.DataAnnotations;

namespace Blog.Core.Domains.Entities.Core
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    [Table(Name = SystemConst.DbTablePrefix + "_user")]
    public class UserEntity : FullAduitEntity
    {
        /// <summary>
        /// 用户Id(第三方)
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 用户头像地址
        /// </summary>
        [Column(StringLength = 200)]
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 用户首页(第三方)
        /// </summary>
        [Column(StringLength = 200)]
        public string UserUrl { get; set; }

        /// <summary>
        /// 用户名(第三方)
        /// </summary>
        [Column(StringLength = 50)]
        public string Name { get; set; }

        /// <summary>
        /// 用户公司信息
        /// </summary>
        [Column(StringLength = 100)]
        public string Company { get; set; }

        /// <summary>
        /// 用户地址信息
        /// </summary>
        [Column(StringLength = 200)]
        public string Location { get; set; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        [Column(StringLength = 50)]
        public string Email { get; set; }

        /// <summary>
        /// 用户个人简介
        /// </summary>
        [Column(StringLength = 500)]
        public string Bio { get; set; }
    }
}
