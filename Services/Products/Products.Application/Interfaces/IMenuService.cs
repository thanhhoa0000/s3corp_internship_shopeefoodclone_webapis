﻿namespace ShopeeFoodClone.WebApi.Products.Application.Interfaces;

public interface IMenuService
{
    Task<Response> GetAsync(Guid menuId);
    Task<Response> GetMenusByStoreIdAsync(GetMenusRequest request);
    Task<Response> CreateAsync(CreateMenuRequest request);
    Task<Response> AddProductsToMenuAsync(VendorAddProductsToMenuRequest request);
    Task<Response> RemoveProductsFromMenuAsync(VendorRemoveProductsFromMenuRequest request);   
    Task<Response> RemoveAsync(Guid menuId);
    Task<Response> VendorDeleteAsync(Guid menuId);
    Task<Response> UpdateMenuAsync(VendorUpdateMenuRequest request);
    Task<Response> UpdateMenuStateAsync(VendorUpdateMenuStateRequest request);
}
