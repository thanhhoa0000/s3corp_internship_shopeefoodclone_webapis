using Microsoft.EntityFrameworkCore;

namespace ShopeeFoodClone.WebApi.Products.Application.Services;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public MenuService(
        IMenuRepository menuRepository,
        IProductRepository productRepository,
        IMapper mapper)
    {
        _menuRepository = menuRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Get menu by ID
    /// </summary>
    /// <param name="menuId">The menu ID to get</param>
    /// <returns>The menu</returns>
    public async Task<Response> GetAsync(Guid menuId)
    {
        var response = new Response();

        try
        {
            var menu = await _menuRepository.GetAsync(p => p.Id == menuId);

            if (menu is null)
            {
                response.IsSuccessful = false;
                response.Message = "Menu not found!";
                
                return response;
            }
            
            response.Body = _mapper.Map<MenuDto>(menu);
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }
        
        return response;
    }

    /// <summary>
    /// Get list of menus by store ID
    /// </summary>
    /// <param name="request">The store ID to get products</param>
    /// <returns>The menus list</returns>
    public async Task<Response> GetMenusByStoreIdAsync(GetMenusRequest request)
    {
        var response = new Response();

        try
        {
            var storeId = request.StoreId;
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;

            Expression<Func<Menu, bool>> filter = p => p.StoreId == storeId;
            
            Func<IQueryable<Menu>, IOrderedQueryable<Menu>> orderBy = query =>
                query
                    .OrderBy(i => i.Title.ToLower().Contains("bán chạy") ? 0 : 1)
                    .ThenBy(i => i.Title.ToLower().Contains("hot deal") ? 0 : 1)
                    .ThenBy(i => i.Title.ToLower().Contains("đang giảm") ? 0 : 1)
                    .ThenBy(i => i.Title.ToLower().Contains("món mới") ? 0 : 1)
                    .ThenBy(i => i.Products.Count == 0 ? 1 : 0);


            Func<IQueryable<Menu>, IQueryable<Menu>> include = query =>
                query.Include(i => i.Products.Where(p => p.State != ProductState.Deleted));

            var menus =
                await _menuRepository
                    .GetAllAsync(filter: filter, orderBy: orderBy, include: include, tracked: false, pageSize: pageSize, pageNumber: pageNumber);

            response.Body = _mapper.Map<IEnumerable<Menu>>(menus);
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }
        
        return response;
    }
    
    /// <summary>
    /// Create a menu
    /// </summary>
    /// <param name="request">The menu to create</param>
    /// <returns>The created menu</returns>
    public async Task<Response> CreateAsync(CreateMenuRequest request)
    {
        var response = new Response();

        try
        {
            if (await _menuRepository.GetAsync(p => p.Id == request.Id) is not null)
            {
                response.IsSuccessful = false;
                response.Message = "Menu already exists!";
                
                return response;
            }

            var menu = new Menu
            {
                Id = Guid.NewGuid(),
                StoreId = request.StoreId,
                Title = request.Title,
                State = request.State
            };
            
            menu.ConcurrencyStamp = Guid.NewGuid();
            menu.LastUpdatedAt = DateTime.UtcNow;
            
            await _menuRepository.CreateAsync(menu);
            
            response.Message = "Menu created successfully!";
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }
        
        return response;
    }

    /// <summary>
    /// Add product(s) to the menu
    /// </summary>
    /// <param name="request">The product ID(s) to add</param>
    /// <returns>The menu with the added product(s)</returns>
    public async Task<Response> AddProductsToMenuAsync(VendorAddProductsToMenuRequest request)
    {
        var response = new Response();

        try
        {
            var menu = await _menuRepository.GetAsync(p => p.Id == request.MenuId);

            if (menu is null)
            {
                response.IsSuccessful = false;
                response.Message = "Menu not found!";
                
                return response;
            }
            
            if (menu.ConcurrencyStamp != request.ConcurrencyStamp)
            {
                response.IsSuccessful = false;
                response.Message = "Concurrency conflict! Data was modified by another user!";
                
                return response;
            }

            if (menu.State != MenuState.Active)
            {
                response.IsSuccessful = false;
                response.Message = "Menu is not active!";
                
                return response;
            }
            
            var existingProducts =
                (await _productRepository.GetAllAsync(p => request.ProductIds!.Contains(p.Id))).ToList();

            if (existingProducts.Count != request.ProductIds!.Count)
            {
                response.IsSuccessful = false;
                response.Message = "One or more categories do not exist!";
            }
            
            menu.Products = existingProducts;
            menu.LastUpdatedAt = DateTime.UtcNow;
            menu.ConcurrencyStamp = Guid.NewGuid();

            await _menuRepository.UpdateAsync(menu);
            
            response.Message = "Product(s) added to menu successfully!";
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }
        
        return response;
    }
    
    /// <summary>
    /// Update the menu's metadata
    /// </summary>
    /// <param name="request">The menu to update</param>
    /// <returns>The updated menu</returns>
    public async Task<Response> UpdateMenuAsync(VendorUpdateMenuRequest request)
    {
        var response = new Response();

        try
        {
            var menu = await _menuRepository.GetAsync(s => s.Id == request.Id, tracked: false);

            if (menu is null)
            {
                response.IsSuccessful = false;
                response.Message = "Menu not found!";
                
                return response;
            }
            
            if (menu.ConcurrencyStamp != request.ConcurrencyStamp)
            {
                response.IsSuccessful = false;
                response.Message = "Concurrency conflict! Data was modified by another user!";
                
                return response;
            }
            
            if (menu.State != MenuState.Active)
            {
                response.IsSuccessful = false;
                response.Message = "Menu is not active!";
                
                return response;
            }

            var menuToUpdate = new Menu
            {
                Id = request.Id,
                StoreId = request.StoreId,
                Title = request.Title,
                State = request.State,
            };
            
            menuToUpdate.LastUpdatedAt = DateTime.UtcNow;
            menuToUpdate.ConcurrencyStamp = Guid.NewGuid();
            
            await _menuRepository.UpdateAsync(menuToUpdate);
            
            response.Message = "Menu updated successfully!";
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }
        return response;
    }
    
    /// <summary>
    /// Update the menu's state
    /// </summary>
    /// <param name="request">The menu state to update</param>
    /// <returns>The updated menu</returns>
    public async Task<Response> UpdateMenuStateAsync(VendorUpdateMenuStateRequest request)
    {
        var response = new Response();

        try
        {
            var menu = await _menuRepository.GetAsync(s => s.Id == request.MenuId, tracked: false);

            if (menu is null)
            {
                response.IsSuccessful = false;
                response.Message = "Menu not found!";
                
                return response;
            }
            
            if (menu.ConcurrencyStamp != request.ConcurrencyStamp)
            {
                response.IsSuccessful = false;
                response.Message = "Concurrency conflict! Data was modified by another user!";
                
                return response;
            }
            
            if (menu.State != MenuState.Active)
            {
                response.IsSuccessful = false;
                response.Message = "Menu is not active!";
                
                return response;
            }
            
            var menuToUpdate = _mapper.Map<MenuDto>(menu);
            
            menuToUpdate.LastUpdatedAt = DateTime.UtcNow;
            menuToUpdate.State = request.State;
            
            await _menuRepository.UpdateAsync(_mapper.Map<Menu>(menuToUpdate));

            response.Message = "Menu updated successfully!";
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }
        
        return response;
    }
    
    /// <summary>
    /// Deleted the menu (soft delete)
    /// </summary>
    /// <param name="menuId">The menu's ID to delete</param>
    /// <returns>The deleted product</returns>
    public async Task<Response> VendorDeleteAsync(Guid menuId)
    {
        var response = new Response();

        try
        {
            var menu = await _menuRepository.GetAsync(s => s.Id == menuId, tracked: false);
            
            if (menu is null)
            {
                response.IsSuccessful = false;
                response.Message = "Menu not found!";
                
                return response;
            }
            
            if (menu.State != MenuState.Active)
            {
                response.IsSuccessful = false;
                response.Message = "Menu is not active!";
                
                return response;
            }
            
            menu.LastUpdatedAt = DateTime.UtcNow;
            menu.State = MenuState.Inactive;
            
            await _menuRepository.UpdateAsync(menu);
            
            response.Message = "Menu deleted successfully!";
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }
        
        return response;
    }

    /// <summary>
    /// Deleted the menu (soft delete)
    /// </summary>
    /// <param name="menuId">The menu's ID to delete</param>
    /// <returns>The deleted product</returns>
    public async Task<Response> RemoveAsync(Guid menuId)
    {
        var response = new Response();

        try
        {
            var menu = await _menuRepository.GetAsync(s => s.Id == menuId, tracked: false);
            
            if (menu is null)
            {
                response.IsSuccessful = false;
                response.Message = "Menu not found!";
                
                return response;
            }
            
            await _menuRepository.RemoveAsync(menu);
            
            response.Message = "Menu deleted successfully!";
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }
        
        return response;
    }
}
