﻿using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Memoyu.Blog.BlazorApp.Response.Base;
using Memoyu.Blog.BlazorApp.Response.Blog;
using Microsoft.AspNetCore.Components;

namespace Memoyu.Blog.BlazorApp.Pages.Blog
{
    public partial class Posts
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        [Parameter]
        public int? page { get; set; }

        /// <summary>
        /// 限制条数
        /// </summary>
        private int Limit = 15;

        /// <summary>
        /// 总页码
        /// </summary>
        private int TotalPage;

        /// <summary>
        /// 文章列表数据
        /// </summary>
        private ServiceResult<PagedList<QueryPostDto>> posts;

        /// <summary>
        /// 初始化
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await Common.SetTitleAsync("Posts");
            // 设置默认值
            page = page.HasValue ? page : 1;
            await RenderPage(page);
        }
        /// <summary>
        /// 初始化完成后执行
        /// </summary>
        /// <returns></returns>
        protected override async Task OnParametersSetAsync()
        {
            // 从头部菜单过来page是没有值的，所以将page设为1，并请求数据，默认正常翻页请求不执行
            if (!page.HasValue)
            {
                page = 1;
                await RenderPage(page);
            }
        }
        /// <summary>
        /// 点击页码重新渲染数据
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private async Task RenderPage(int? page)
        {
            // 获取数据
            posts = await Http.GetFromJsonAsync<ServiceResult<PagedList<QueryPostDto>>>($"/blog/posts?page={page}&limit={Limit}");
            // 计算总页码
            TotalPage = (int)Math.Ceiling((posts.Result.Total / (double)Limit));
        }
    }
}