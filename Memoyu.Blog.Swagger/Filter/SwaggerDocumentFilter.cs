/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：SwaggerDocumentFilter
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/6/26 21:30:54
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Memoyu.Blog.Swagger.Filter
{
    public class SwaggerDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// 对应Controller的API文档描述信息
        /// </summary>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var tags = new List<OpenApiTag>
            {
                    new OpenApiTag
                    {
                        Name = "Auth",
                        Description = "JWT模式认证授权",
                        ExternalDocs = new OpenApiExternalDocs { Description = "JSON Web Token" }
                    },
                    new OpenApiTag
                    {
                        Name = "Blog",
                        Description = "个人博客相关接口",
                        ExternalDocs = new OpenApiExternalDocs { Description = "包含：文章/标签/分类/友链" }
                    },
            };

            #region 实现自定义API描述，并过滤不属于当前分组的API
            //获取当前组名称
            var groupName = context.ApiDescriptions.FirstOrDefault()?.GroupName;
            var apis = context.ApiDescriptions.GetType()
                .GetField("_source", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(context.ApiDescriptions) as IEnumerable<ApiDescription>;

            // 不属于当前分组的所有Controller
            // 注意：配置的OpenApiTag，Name名称要与Controller的Name对应才会生效。
            var controllers = apis.Where(x => x.GroupName != groupName).Select(x => ((ControllerActionDescriptor)x.ActionDescriptor).ControllerName).Distinct();

            // 筛选一下tags
            swaggerDoc.Tags = tags.Where(x => !controllers.Contains(x.Name)).OrderBy(x => x.Name).ToList();
            #endregion
        }
    }
}
