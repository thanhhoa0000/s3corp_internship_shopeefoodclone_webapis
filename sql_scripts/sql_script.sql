DECLARE @__p_0 int = 0;
DECLARE @__p_1 int = 12;

SELECT 
    [t].[Id], 
    [t].[ClosingHour], 
    [t].[CoverImagePath], 
    [t].[IsPromoted], 
    [t].[Name], 
    [t].[OpeningHour], 
    [t].[Rating], 
    [t].[Sold], 
    [t].[StreetName], 
    [t].[UserId], 
    [t].[WardCode], 
    [w].[Code], 
    [w].[AdministrativeUnitId], 
    [w].[CodeName], 
    [w].[DistrictCode], 
    [w].[FullName], 
    [w].[FullNameEng], 
    [w].[Name], 
    [w].[NameEng], 
    [d].[Code], 
    [d].[AdministrativeUnitId], 
    [d].[CodeName], 
    [d].[FullName], 
    [d].[FullNameEng], 
    [d].[Name], 
    [d].[NameEng], 
    [d].[ProvinceCode], 
    [p].[Code], 
    [p].[AdministrativeRegionId], 
    [p].[AdministrativeUnitId], 
    [p].[CodeName], 
    [p].[FullName], 
    [p].[FullNameEng], 
    [p].[Name], 
    [p].[NameEng], 
    [t0].[StoresId], 
    [t0].[SubCategoriesId],
    [t0].[Id], 
    [t0].[CategoryId], 
    [t0].[CodeName], 
    [t0].[ConcurrencyStamp], 
    [t0].[Name], 
    [t0].[Id0], 
    [t0].[CodeName0], 
    [t0].[ConcurrencyStamp0], 
    [t0].[Name0]
FROM (
    SELECT 
        [s].[Id], 
        [s].[ClosingHour], 
        [s].[CoverImagePath], 
        [s].[IsPromoted], 
        [s].[Name], 
        [s].[OpeningHour], 
        [s].[Rating], 
        [s].[Sold], 
        [s].[StreetName], 
        [s].[UserId], 
        [s].[WardCode]
    FROM [Stores] AS [s]
    ORDER BY (SELECT 1)
    OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY
) AS [t]
LEFT JOIN [Wards] AS [w] ON [t].[WardCode] = [w].[Code]
LEFT JOIN [Districts] AS [d] ON [w].[DistrictCode] = [d].[Code]
LEFT JOIN [Provinces] AS [p] ON [d].[ProvinceCode] = [p].[Code]
LEFT JOIN (
    SELECT 
        [s0].[StoresId], 
        [s0].[SubCategoriesId], 
        [s1].[Id], 
        [s1].[CategoryId], 
        [s1].[CodeName], 
        [s1].[ConcurrencyStamp], 
        [s1].[Name], 
        [c].[Id] AS [Id0], 
        [c].[CodeName] AS [CodeName0], 
        [c].[ConcurrencyStamp] AS [ConcurrencyStamp0], 
        [c].[Name] AS [Name0]
    FROM [StoreSubCategory] AS [s0]
    INNER JOIN [SubCategories] AS [s1] ON [s0].[SubCategoriesId] = [s1].[Id]
    INNER JOIN [Categories] AS [c] ON [s1].[CategoryId] = [c].[Id]
) AS [t0] ON [t].[Id] = [t0].[StoresId]
ORDER BY [t].[Id], [w].[Code], [d].[Code], [p].[Code], [t0].[StoresId], [t0].[SubCategoriesId], [t0].[Id]