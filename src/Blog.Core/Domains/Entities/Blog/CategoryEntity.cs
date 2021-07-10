using Blog.Core.Domains.Common.Base;
using Blog.Core.Domains.Common.Consts;
using FreeSql.DataAnnotations;

namespace Blog.Core.Domains.Entities.Blog
{
    /// <summary>
    /// 分类表
    /// </summary>
    [Table(Name = SystemConst.DbTablePrefix + "_category")]
    public class CategoryEntity:FullAduitEntity
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        [Column(StringLength = 50)]
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
