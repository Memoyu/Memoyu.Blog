namespace Blog.Service.Blog.Tag.Input
{
    public class AddTagDto
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        public (bool Fail, string Msg) Validation()
        {
            if (string.IsNullOrWhiteSpace(Name)) return (true, "标签名称不能为空");
            return (false, string.Empty);
        }
    }
}
