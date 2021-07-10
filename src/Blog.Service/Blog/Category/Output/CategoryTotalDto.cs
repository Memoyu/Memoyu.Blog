namespace Blog.Service.Blog.Category.Output
{
    public class CategoryTotalDto : CategoryDto
    {
        /// <summary>
        /// 关联文章数量
        /// </summary>
        public int Total { get; set; }
        
    }
}
