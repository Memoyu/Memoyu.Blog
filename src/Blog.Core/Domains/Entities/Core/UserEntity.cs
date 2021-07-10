using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        [Column(StringLength = 50)]
        public string Login { get; set; }

        public int UserId { get; set; }

        public int UserType { get; set; }

        [Column(StringLength = 200)]
        public string AvatarUrl { get; set; }

        [Column(StringLength = 200)]
        public string HtmlUrl { get; set; }

        [Column(StringLength = 200)]
        public string ReposUrl { get; set; }

        [Column(StringLength = 50)]
        public string Name { get; set; }

        [Column(StringLength = 100)]
        public string Company { get; set; }

        [Column(StringLength = 200)]
        public string Blog { get; set; }

        [Column(StringLength = 200)]
        public string Location { get; set; }

        [Column(StringLength = 50)]
        public string Email { get; set; }

        [Column(StringLength = 500)]
        public string Bio { get; set; }

        public int PublicRepos { get; set; }
    }
}
