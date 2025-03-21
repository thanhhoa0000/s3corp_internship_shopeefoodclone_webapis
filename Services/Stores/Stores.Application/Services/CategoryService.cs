namespace ShopeeFoodClone.WebApi.Stores.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CategoryService(
        ICategoryRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Get list of categories
    /// </summary>
    /// <param name="pageSize">Pages number to get categories</param>
    /// <param name="pageNumber">Page number to start with</param>
    /// <returns>The categories list</returns>
    public async Task<Response> GetAllAsync(int pageSize = 0, int pageNumber = 1)
    {
        var response = new Response();

        try
        {
            var categories = await _repository.GetAllAsync(tracked: false, pageSize: pageSize, pageNumber: pageNumber);
            
            response.Body = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            
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
    /// Get a category
    /// </summary>
    /// <param name="cateId">The ID of the category</param>
    /// <returns>The category</returns>
    public async Task<Response> GetAsync(Guid cateId)
    {
        var response = new Response();

        try
        {
            var category = await _repository.GetAsync(s => s.Id == cateId, tracked: false);
            
            response.Body = _mapper.Map<CategoryDto>(category);
            
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
    /// Get a category
    /// </summary>
    /// <param name="name">The code name of the category</param>
    /// <returns>The category</returns>
    public async Task<Response> GetByCodeNameAsync(string name)
    {
        var response = new Response();

        try
        {
            var category = await _repository.GetAsync(s => s.CodeName == name, tracked: false);
            
            response.Body = _mapper.Map<CategoryDto>(category);
            
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
    /// Create a category
    /// </summary>
    /// <param name="categoryDto">The category to create</param>
    /// <returns>The created category</returns>
    public async Task<Response> CreateAsync(CategoryDto categoryDto)
    {
        var response = new Response();

        try
        {
            var category = _mapper.Map<Category>(categoryDto);
            
            await _repository.CreateAsync(category);
            
            response.Body = _mapper.Map<CategoryDto>(category);
            
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
    /// Update the category's metadata
    /// </summary>
    /// <param name="categoryDto">The category to update</param>
    /// <returns>The updated category</returns>
    public async Task<Response> UpdateAsync(CategoryDto categoryDto)
    {
        var response = new Response();

        try
        {
            var category = await _repository.GetAsync(s => s.Id == categoryDto.Id, tracked: false);

            if (category is null)
            {
                response.IsSuccessful = false;
                response.Message = "Category not found!";
                
                return response;
            }
            
            if (category.ConcurrencyStamp != categoryDto.ConcurrencyStamp)
            {
                response.IsSuccessful = false;
                response.Message = "Concurrency conflict! Data was modified by another user!";
                
                return response;
            }
            
            var categoryToUpdate = _mapper.Map<Category>(categoryDto);
            
            categoryToUpdate.ConcurrencyStamp = Guid.NewGuid();
            
            await _repository.UpdateAsync(categoryToUpdate);
            
            response.Body = _mapper.Map<CategoryDto>(categoryToUpdate);
            
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
    /// Deleted the category
    /// </summary>
    /// <param name="cateId">The category's ID to delete</param>
    /// <returns>The deleted category</returns>
    public async Task<Response> RemoveAsync(Guid cateId)
    {
        var response = new Response();

        try
        {
            var category = await _repository.GetAsync(s => s.Id == cateId, tracked: false);
            
            if (category is null)
            {
                response.IsSuccessful = false;
                response.Message = "Category not found!";
                
                return response;
            }
            
            await _repository.RemoveAsync(category);
            
            response.Body = _mapper.Map<CategoryDto>(category);
            
            return response;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            
            return response;
        }
    }
}