﻿namespace Blog.Service.Blog.Article.Output
{
    public class ArticleContentDto : ArticleDto
    {
        /// <summary>
        /// HTML
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// Markdown
        /// </summary>
        public string Markdown { get; set; }
    }
}
