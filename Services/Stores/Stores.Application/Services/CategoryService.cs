namespace ShopeeFoodClone.WebApi.Stores.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(
        ICategoryRepository categoryRepository,
        ISubCategoryRepository subCategoryRepository,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _subCategoryRepository = subCategoryRepository;
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
            var categories = await _categoryRepository.GetAllAsync(tracked: false, pageSize: pageSize, pageNumber: pageNumber);
            
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
            var category = await _categoryRepository.GetAsync(s => s.Id == cateId, tracked: false);
            
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
            var category = await _categoryRepository.GetAsync(s => s.CodeName == name, tracked: false);
            
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
            
            await _categoryRepository.CreateAsync(category);
            
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
            var category = await _categoryRepository.GetAsync(s => s.Id == categoryDto.Id, tracked: false);

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
            
            await _categoryRepository.UpdateAsync(categoryToUpdate);
            
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
            var category = await _categoryRepository.GetAsync(s => s.Id == cateId, tracked: false);
            
            if (category is null)
            {
                response.IsSuccessful = false;
                response.Message = "Category not found!";
                
                return response;
            }
            
            await _categoryRepository.RemoveAsync(category);
            
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
    /// Deleted (change state) the category
    /// </summary>
    /// <param name="cateId">The category's ID to delete</param>
    /// <returns>The deleted category</returns>
    public async Task<Response> DeleteAsync(Guid cateId)
    {
        var response = new Response();

        try
        {
            var category = await _categoryRepository.GetAsync(s => s.Id == cateId, tracked: false);
            
            if (category is null)
            {
                response.IsSuccessful = false;
                response.Message = "Category not found!";
                
                return response;
            }

            category.State = CategoryState.Deleted;
            await _categoryRepository.UpdateAsync(category);
            
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

    public async Task<Response> GetNamesListWithSubCategoriesNameAsync(int pageSize = 0, int pageNumber = 1)
    {
        var response = new Response();

        try
        {
            var categories = 
                await _categoryRepository.GetAllAsync(tracked: false, pageSize: pageSize, pageNumber: pageNumber);
            
            var returnObjects = new List<object>();

            foreach (var category in categories.OrderBy(c => c.CodeName != "food").ToList())
            {
                var subCategories = 
                    await _subCategoryRepository
                        .GetAllAsync(s => s.Category!.CodeName == category.CodeName, tracked: false);
                
                var subCategoriesToReturn = subCategories
                    .Select(s => new 
                    {
                        s.Name,
                        s.CodeName
                    })
                    .ToList();

                returnObjects.Add(new
                {
                    Name = category.Name,
                    CodeName = category.CodeName,
                    SubCategories = subCategoriesToReturn
                });

                response.Body = returnObjects;
            }
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }
        
        return response;
    }
}
