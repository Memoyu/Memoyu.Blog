using Blog.Service.Core.Files.Output;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Blog.Service.Core.Files
{
    public interface IFileSvc
    {
        /// <summary>
        /// 单文件上传，键为file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<FileDto> UploadAsync(IFormFile file, string type, int key = 0);
    }
}
