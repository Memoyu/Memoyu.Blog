using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.AOP.Attributes;
using Blog.Core.Domains.Common;
using Blog.Core.Domains.Entities.Blog;
using Blog.Core.Interface.IRepositories.Blog;
using Blog.Service.Base;
using Blog.Service.Blog.Tag.Input;
using Blog.Service.Blog.Tag.Output;

namespace Blog.Service.Blog.Tag
{
    public class TagSvc : ApplicationSvc, ITagSvc
    {
        private readonly ITagRepo _tagRepo;
        private readonly IArticleTagRepo _articleTagRepo;

        public TagSvc(ITagRepo tagRepo, IArticleTagRepo articleTagRepo)
        {
            _tagRepo = tagRepo;
            _articleTagRepo = articleTagRepo;
        }

        public async Task<ServiceResult> AddAsync(AddTagDto add)
        {
            try
            {
                var exist = await _tagRepo.Select.AnyAsync(c => c.Name == add.Name);
                if (exist) return await Task.FromResult(ServiceResult.Failed($"Name:{add.Name} 的文章标签已存在"));
                var entity = Mapper.Map<TagEntity>(add);
                await _tagRepo.InsertAsync(entity);
                return await Task.FromResult(ServiceResult.Successed("新增文章标签成功"));
            }
            catch (Exception ex)
            {
                // TODO 异常日志记录
                return await Task.FromResult(ServiceResult.Failed($"新增文章标签异常"));
            }
        }

        [Transactional]
        public async Task<ServiceResult> DeleteAsync(long id)
        {
            try
            {
                var exist = await _tagRepo.Select.AnyAsync(c => c.Id == id);
                if (!exist) return await Task.FromResult(ServiceResult.Failed($"Id:{id} 的文章标签不存在"));
                // 先删除与文章的关系
                await _articleTagRepo.DeleteAsync(a => a.TagId == id);
                // 删除文章标签
                await _tagRepo.DeleteAsync(id);
                return await Task.FromResult(ServiceResult.Successed("删除文章标签成功"));
            }
            catch (Exception ex)
            {
                // TODO 异常日志记录
                return await Task.FromResult(ServiceResult.Failed($"删除文章标签异常"));
            }
        }

        public async Task<ServiceResult<TagTotalDto>> GetAsync(long id)
        {
            try
            {
                var tag = await _tagRepo
                       .Select
                       .Where(t => !t.IsDeleted)
                       .Where(t => t.Id == id)
                       .ToOneAsync<TagTotalDto>();
                tag.Total = (int)await _articleTagRepo.Select.Where(a => a.TagId == tag.Id).CountAsync();
                return await Task.FromResult(ServiceResult<TagTotalDto>.Successed(tag));
            }
            catch (Exception ex)
            {
                // TODO 异常日志记录
                return await Task.FromResult(ServiceResult<TagTotalDto>.Failed($"获取文章标签异常"));
            }
        }

        public async Task<ServiceResult<List<TagTotalDto>>> GetsAsync()
        {
            try
            {
                var tags = await _tagRepo
                        .Select
                        .Where(t => !t.IsDeleted)
                        .ToListAsync<TagTotalDto>();
                var dtos = tags
                    .Select(c =>
                    {
                        c.Total = (int)_articleTagRepo.Select.Where(a => a.TagId == c.Id).Count();
                        return c;
                    }).ToList();

                return await Task.FromResult(ServiceResult<List<TagTotalDto>>.Successed(dtos));
            }
            catch (Exception ex)
            {
                // TODO 异常日志记录
                return await Task.FromResult(ServiceResult<List<TagTotalDto>>.Failed($"获取文章标签列表异常"));
            }
        }

        public async Task<ServiceResult> UpdateAsync(ModifyTagDto modify)
        {
            try
            {
                var tag = await _tagRepo.Select.Where(c => c.Name == modify.Name).ToOneAsync();
                if (tag != null && tag.Id != modify.Id) return await Task.FromResult(ServiceResult.Failed($"Name:{modify.Name} 的文章标签已存在"));
                var entity = Mapper.Map<TagEntity>(modify);
                await _tagRepo.UpdateAsync(entity);
                return await Task.FromResult(ServiceResult.Successed("更新文章标签成功"));
            }
            catch (Exception ex)
            {
                // TODO 异常日志记录
                return await Task.FromResult(ServiceResult.Failed($"更新文章标签异常"));
            }
        }
    }
}
