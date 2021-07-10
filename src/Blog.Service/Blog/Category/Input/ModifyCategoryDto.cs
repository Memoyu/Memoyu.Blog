using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Domains.Common;

namespace Blog.Service.Blog.Category.Input
{
    public class ModifyCategoryDto : AddCategoryDto
    {
        public long  Id { get; set; }

        public new (bool Fail, string Msg) Validation()
        {
            if (Id <= 0) return (true, "分类Id不能小于等于0");
            base.Validation();
            return (false, string.Empty);
        }
    }
}
