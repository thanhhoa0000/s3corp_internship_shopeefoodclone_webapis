namespace ShopeeFoodClone.WebApi.Stores.Application.Services;

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
    /// <param name="cateId">The main category that the sub-categories belong to</param>
    /// <param name="pageSize">Pages number to get subCategories</param>
    /// <param name="pageNumber">Page number to start with</param>
    /// <returns>The subCategories list</returns>
    public async Task<Response> GetAllAsync(Guid cateId, int pageSize = 0, int pageNumber = 1)
    {
        var response = new Response();

        try
        {
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
    /// <param name="cateName">The main category that the sub-categories belong to</param>
    /// <param name="pageSize">Pages number to get subCategories</param>
    /// <param name="pageNumber">Page number to start with</param>
    /// <returns>The subCategories list</returns>
    public async Task<Response> GetAllByCodeNameAsync(string cateName, int pageSize = 0, int pageNumber = 1)
    {
        var response = new Response();

        try
        {
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
            var subCategory = _mapper.Map<SubCategory>(request.SubCategoryDto);

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
    /// <param name="subCategoryDto">The subCategory to update</param>
    /// <returns>The updated subCategory</returns>
    public async Task<Response> UpdateAsync(SubCategoryDto subCategoryDto)
    {
        var response = new Response();

        try
        {
            var subCategory = await _subCategoryRepository.GetAsync(s => s.Id == subCategoryDto.Id, tracked: false);

            if (subCategory.ConcurrencyStamp != subCategoryDto.ConcurrencyStamp)
            {
                response.IsSuccessful = false;
                response.Message = "Concurrency conflict! Data was modified by another user!";

                return response;
            }

            var subCategoryToUpdate = _mapper.Map<SubCategory>(subCategoryDto);
            
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
}