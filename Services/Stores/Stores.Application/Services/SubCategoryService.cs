﻿namespace ShopeeFoodClone.WebApi.Stores.Application.Services;

public class SubCategoryService : ISubCategoryService
{
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public SubCategoryService(
        ISubCategoryRepository subCategoryRepository,
        ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _subCategoryRepository = subCategoryRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Get list of subCategories
    /// </summary>
    /// <param name="request">The main category that the sub-categories belong to</param>
    /// <returns>The subCategories list</returns>
    public async Task<Response> GetAllByCategoryIdAsync(GetSubCategoriesRequest request)
    {
        var response = new Response();

        try
        {
            var cateId = request.CategoryId;
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;
            
            var subCategories = await _subCategoryRepository.GetAllAsync(
                filter: s => s.CategoryId == cateId, 
                tracked: false,
                pageSize: pageSize, 
                pageNumber: pageNumber);

            foreach (var subCategory in subCategories)
            {
                subCategory.Category = await _categoryRepository.GetAsync(c => c.Id == subCategory.CategoryId);
            }
            
            response.Body = _mapper.Map<IEnumerable<SubCategoryDto>>(subCategories);

            return response;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;

            return response;
        }
    }
    
    /// <summary>
    /// Get list of subCategories
    /// </summary>
    /// <param name="request">The main category that the sub-categories belong to</param>
    /// <returns>The subCategories list</returns>
    public async Task<Response> GetAllByCategoryCodeNameAsync(GetSubCategoriesRequest request)
    {
        var response = new Response();

        try
        {
            var cateName = request.CategoryName;
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;
            
            var subCategories = await _subCategoryRepository.GetAllAsync(
                filter: s => s.Category!.CodeName == cateName, 
                tracked: false,
                pageSize: pageSize, 
                pageNumber: pageNumber);

            foreach (var subCategory in subCategories)
            {
                subCategory.Category = await _categoryRepository.GetAsync(c => c.Id == subCategory.CategoryId);
            }
            
            response.Body = _mapper.Map<IEnumerable<SubCategoryDto>>(subCategories);

            return response;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;

            return response;
        }
    }

    /// <summary>
    /// Get a subCategory
    /// </summary>
    /// <param name="subCateId">The ID of the subCategory</param>
    /// <returns>The subCategory</returns>
    public async Task<Response> GetAsync(Guid subCateId)
    {
        var response = new Response();

        try
        {
            var subCategory = await _subCategoryRepository.GetAsync(s => s.Id == subCateId, tracked: false);
            
            if (subCategory is null)
            {
                response.IsSuccessful = false;
                response.Message = "SubCategory not found!";
                
                return response;
            }

            response.Body = _mapper.Map<CategoryDto>(subCategory);

            return response;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;

            return response;
        }
    }

    /// <summary>
    /// Create a subCategory
    /// </summary>
    /// <param name="request">The subCategory to create</param>
    /// <returns>The created subCategory</returns>
    public async Task<Response> CreateAsync(CreateSubCategoryRequest request)
    {
        var response = new Response();

        try
        {

            var subCategory = new SubCategory
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CodeName = request.CodeName
            };

            subCategory.CategoryId = request.CategoryId;
            
            subCategory.Category = await _categoryRepository.GetAsync(s => s.Id == request.CategoryId);

            await _subCategoryRepository.CreateAsync(subCategory);

            response.Body = _mapper.Map<SubCategoryDto>(subCategory);

            return response;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;

            return response;
        }
    }

    /// <summary>
    /// Update the subCategory's metadata
    /// </summary>
    /// <param name="request">The subCategory to update</param>
    /// <returns>The updated subCategory</returns>
    public async Task<Response> UpdateAsync(UpdateSubCategoryRequest request)
    {
        var response = new Response();

        try
        {
            var subCategory = 
                await _subCategoryRepository.GetAsync(s => s.Id == request.Id, tracked: false);

            if (subCategory is null)
            {
                response.IsSuccessful = false;
                response.Message = "SubCategory not found!";
                
                return response;
            }

            if (subCategory.ConcurrencyStamp != request.ConcurrencyStamp)
            {
                response.IsSuccessful = false;
                response.Message = "Concurrency conflict! Data was modified by another user!";

                return response;
            }
            
            var subCategoryToUpdate = new SubCategory
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
                CodeName = request.CodeName
            };
            
            subCategoryToUpdate.ConcurrencyStamp = Guid.NewGuid();

            await _subCategoryRepository.UpdateAsync(subCategoryToUpdate);

            response.Body = _mapper.Map<SubCategoryDto>(subCategoryToUpdate);

            return response;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;

            return response;
        }
    }

    /// <summary>
    /// Deleted the subCategory
    /// </summary>
    /// <param name="subCateId">The subCategory's ID to delete</param>
    /// <returns>The deleted subCategory</returns>
    public async Task<Response> RemoveAsync(Guid subCateId)
    {
        var response = new Response();

        try
        {
            var subCategory = await _subCategoryRepository.GetAsync(s => s.Id == subCateId, tracked: false);
            
            if (subCategory is null)
            {
                response.IsSuccessful = false;
                response.Message = "SubCategory not found!";
                
                return response;
            }
            
            await _subCategoryRepository.RemoveAsync(subCategory);

            response.Body = _mapper.Map<SubCategoryDto>(subCategory);

            return response;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.ToString();

            return response;
        }
    }
    
    /// <summary>
    /// Deleted (change state) the subCategory
    /// </summary>
    /// <param name="subCateId">The subCategory's ID to delete</param>
    /// <returns>The deleted subCategory</returns>
    public async Task<Response> DeleteAsync(Guid subCateId)
    {
        var response = new Response();

        try
        {
            var subCategory = await _subCategoryRepository.GetAsync(s => s.Id == subCateId, tracked: false);
            
            if (subCategory is null)
            {
                response.IsSuccessful = false;
                response.Message = "SubCategory not found!";
                
                return response;
            }

            subCategory.State = CategoryState.Deleted;
            await _subCategoryRepository.UpdateAsync(subCategory);

            response.Body = _mapper.Map<SubCategoryDto>(subCategory);

            return response;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.ToString();

            return response;
        }
    }
}
