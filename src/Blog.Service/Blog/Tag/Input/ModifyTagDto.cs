namespace Blog.Service.Blog.Tag.Input
{
    public class ModifyTagDto : AddTagDto
    {
        public long Id { get; set; }

        public new(bool Fail, string Msg) Validation()
        {
            if (Id <= 0) return (true, "标签Id不能小于等于0");
            base.Validation();
            return (false, string.Empty);
        }
    }
}
