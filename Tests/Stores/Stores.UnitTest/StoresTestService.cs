namespace Stores.UnitTest;

public class StoresTestService
{
    private Mock<IStoreRepository> _mockStoreRepository;
    private Mock<ISubCategoryRepository> _mockSubCategoryRepository;
    private Mock<IWardRepository> _mockWardRepository;
    private Mock<IMapper> _mockMapper;
    private StoreService _storeService;

    [SetUp]
    public void Setup()
    {
        _mockStoreRepository = new Mock<IStoreRepository>();
        _mockSubCategoryRepository = new Mock<ISubCategoryRepository>();
        _mockWardRepository = new Mock<IWardRepository>();
        _mockMapper = new Mock<IMapper>();

        _storeService = new StoreService(
            _mockStoreRepository.Object,
            _mockSubCategoryRepository.Object,
            _mockWardRepository.Object,
            _mockMapper.Object
        );
    }

    [Test]
    public async Task TestGetStoresByLocationAndCategoryWithValidPayload()
    {
        // Arrange
        var request = new GetStoresRequest
        {
            LocationRequest = new LocationRequest
            {
                Province = "Province1",
                Districts = new List<string> { "District1" },
                Wards = new List<string> { "Ward1" }
            },
            CategoryName = "Food",
            SubCategoryNames = new List<string> { "Pizza" },
            IsPromoted = true,
            PageSize = 10,
            PageNumber = 1
        };

        var stores = new List<Store>
        {
            new() { Id = Guid.NewGuid(), Name = "Store 1", IsPromoted = true },
            new() { Id = Guid.NewGuid(), Name = "Store 2", IsPromoted = true }
        };

        _mockStoreRepository.Setup(
                repo => repo
                    .GetAllAsync(
                        It.IsAny<Expression<Func<Store, bool>>>(), 
                        It.IsAny<Func<IQueryable<Store>, IOrderedQueryable<Store>>>(), 
                        null, 
                        It.IsAny<bool>(), 
                        It.IsAny<bool>(), 
                        It.IsAny<int>(), 
                        It.IsAny<int>()))
                    .ReturnsAsync(stores);

        _mockMapper.Setup(m => m.Map<IEnumerable<StoreDto>>(It.IsAny<IEnumerable<Store>>()))
            .Returns(new List<StoreDto>
            {
                new() { Id = stores[0].Id, Name = stores[0].Name }, 
                new() { Id = stores[1].Id, Name = stores[1].Name }
            });

        // Act
        var response = await _storeService.GetByLocationAndCategoryAsync(request);

        // Assert
        Assert.That(response.IsSuccessful, Is.True);
        Assert.That(response.Body, Is.Not.Null);
    }

    [Test]
    public async Task TestCreateStoreWithValidPayload()
    {
        // Arrange
        Random random = new Random();
        
        var request = new CreateStoreRequest(
            new StoreDto
            {
                Name = "Test Store",
                StreetName = "123 Main St"
            },
            new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
            "123"
        );

        var storeEntity = new Store
        {
            Id = Guid.NewGuid(),
            Name = request.Store.Name ?? "",
            StreetName = request.Store.StreetName,
            WardCode = random.Next(100, 1000).ToString(),
        };

        var ward = new Ward
        {
            Code = request.WardCode,
            Name = "ward1",
            FullName = "ward1"
        };

        _mockMapper.Setup(m => m.Map<Store>(request.Store)).Returns(storeEntity);
        
        _mockWardRepository.Setup(
            repo => repo
                .GetByCodeAsync(It.IsAny<Expression<Func<Ward, bool>>>(), It.IsAny<bool>()))
                .ReturnsAsync(ward);
        
        _mockSubCategoryRepository
            .Setup(repo => repo.GetAllAsync(
                It.IsAny<Expression<Func<SubCategory, bool>>>(),
                null, null, false, true, 0, 1))
            .ReturnsAsync(request.SubCateIds.Select(id => new SubCategory { Id = id, Name = "name", CodeName = "codename"}));

        
        _mockStoreRepository
            .Setup(repo => repo.CreateAsync(storeEntity))
            .Returns(Task.CompletedTask);
        
        


        // Act
        var response = await _storeService.CreateAsync(request);
        
        Console.WriteLine(response.Message);

        // Assert
        Assert.That(response.IsSuccessful, Is.True);
    }
}