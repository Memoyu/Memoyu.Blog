using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Domains.Common;
using Blog.Core.Domains.Entities.Blog;
using Blog.Core.Exceptions;
using Blog.Core.Interface.IRepositories.Blog;
using Blog.Service.Base;
using Blog.Service.Blog.Category.Input;
using Blog.Service.Blog.Category.Output;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Blog.Service.Blog.Category
{
    public class CategorySvc : ApplicationSvc, ICategorySvc
    {
        private readonly ILogger<CategorySvc> _logger;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IArticleRepo _articleRepo;
        private readonly IArticleContentRepo _articleContentRepo;

        public CategorySvc(ILogger<CategorySvc> logger, ICategoryRepo categoryRepo, IArticleRepo articleRepo, IArticleContentRepo articleContentRepo)
        {
            _logger = logger;
            _categoryRepo = categoryRepo;
            _articleRepo = articleRepo;
            _articleContentRepo = articleContentRepo;
        }

        public async Task<ServiceResult> AddAsync(AddCategoryDto add)
        {
            var exist = await _categoryRepo.Select.AnyAsync(c => c.Name == add.Name);
            if (exist) return await Task.FromResult(ServiceResult.Failed($"Name:{add.Name} 的文章分类已存在"));
            var entity = Mapper.Map<CategoryEntity>(add);
            await _categoryRepo.InsertAsync(entity);
            return await Task.FromResult(ServiceResult.Successed("新增文章分类成功"));
        }

        public async Task<ServiceResult> DeleteAsync(long id)
        {
            var exist = await _categoryRepo.Select.AnyAsync(c => c.Id == id);
            if (!exist) return await Task.FromResult(ServiceResult.Failed($"Id:{id} 的文章分类不存在"));

            var articleIds = await _articleRepo.Select.Where(a => a.CategoryId == id).ToListAsync(a => a.Id);
            // 删除相关的文章信息
            await _articleRepo.DeleteAsync(a => articleIds.Contains(a.Id));
            // 删除相关文章的内容信息
            await _articleContentRepo.DeleteAsync(a => articleIds.Contains(a.ArticleId));
            // 删除文章分类
            await _categoryRepo.DeleteAsync(id);
            return await Task.FromResult(ServiceResult.Successed("删除文章分类成功"));
        }

        public async Task<ServiceResult<CategoryTotalDto>> GetAsync(long id)
        {

            var category = await _categoryRepo
            .Select
            .Where(c => !c.IsDeleted)
            .Where(c => c.Id == id)
            .ToOneAsync<CategoryTotalDto>();
            category.Total = (int)await _articleRepo.Select.Where(a => a.CategoryId == category.Id).CountAsync();
            return await Task.FromResult(ServiceResult<CategoryTotalDto>.Successed(category));

        }

        public async Task<ServiceResult<List<CategoryTotalDto>>> GetsAsync()
        {

            var categories = await _categoryRepo
                .Select
                .Where(c => !c.IsDeleted)
                .ToListAsync<CategoryTotalDto>();
            var dtos = categories
                .Select(c =>
                {
                    c.Total = (int)_articleRepo.Select.Where(a => a.CategoryId == c.Id).Count();
                    return c;
                }).ToList();

            return await Task.FromResult(ServiceResult<List<CategoryTotalDto>>.Successed(dtos));

        }

        public async Task<ServiceResult> UpdateAsync(ModifyCategoryDto modify)
        {
            var category = await _categoryRepo.Select.Where(c => c.Name == modify.Name).ToOneAsync();
            if (category != null && category.Id != modify.Id) return await Task.FromResult(ServiceResult.Failed($"Name:{modify.Name} 的文章分类已存在"));
            var entity = Mapper.Map<CategoryEntity>(modify);
            await _categoryRepo.UpdateAsync(entity);
            return await Task.FromResult(ServiceResult.Successed("更新文章分类成功"));

        }
    }
}
