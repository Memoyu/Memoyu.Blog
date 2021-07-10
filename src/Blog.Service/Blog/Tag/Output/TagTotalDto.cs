namespace Blog.Service.Blog.Tag.Output
{
    public class TagTotalDto : TagDto
    {
        /// <summary>
        /// 关联文章数量
        /// </summary>
        public int Total { get; set; }
    }
}
