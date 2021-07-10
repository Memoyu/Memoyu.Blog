using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Blog.Category.Input
{
    public class AddCategoryDto
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        public (bool Fail, string Msg) Validation()
        {
            if (string.IsNullOrWhiteSpace(Name)) return (true, "分类名称不能为空");
            return (false, string.Empty);
        }
    }
}
