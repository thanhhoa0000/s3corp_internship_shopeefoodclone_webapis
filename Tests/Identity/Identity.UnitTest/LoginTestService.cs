namespace Identity.UnitTest;

[TestFixture]
public class LoginTestService
{
    private Mock<IIdentityRepository> _mockRepository;
    private Mock<IMapper> _mockMapper;
    private Mock<IUserService> _mockUserService;
    private Mock<ITokenProvider> _mockTokenProvider;
    private IIdentityService _service;
    
    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IIdentityRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockUserService = new Mock<IUserService>();
        _mockTokenProvider = new Mock<ITokenProvider>();
        _service = new IdentityService(
            _mockRepository.Object, 
            _mockMapper.Object, 
            _mockUserService.Object, 
            _mockTokenProvider.Object);
    }

    [Test]
    [TestCase("customer1", "right_password")]
    public async Task TestLoginWithMatchCredentials(string username, string password)
    {
        // Arrange
        var loginRequest = new LoginRequest
        {
            UserName = username,
            Password = password
        };

        var appUser = new AppUser
        {
            Id = Guid.NewGuid(),
            UserName = loginRequest.UserName,
            Email = $"{username}@example.com"
        };

        _mockRepository
            .Setup(r => r.GetUserAsync(It.IsAny<Expression<Func<AppUser, bool>>>(), It.IsAny<bool>()))
            .ReturnsAsync(appUser);

        _mockUserService
            .Setup(s => s.CheckPasswordAsync(It.IsAny<AppUser>(), loginRequest.Password))
            .ReturnsAsync(true);
        
        _mockUserService
            .Setup(s => s.GetUserRolesAsync(It.IsAny<AppUser>()))
            .ReturnsAsync(new List<string> { "Customer" });
        
        _mockRepository
            .Setup(r => r.GetRefreshTokenAsync(It.IsAny<Expression<Func<RefreshToken, bool>>>(), null, false))
            .ReturnsAsync(new RefreshToken 
            { 
                Token = "some-refresh-token",
                AppUserId = appUser.Id
            });
        
        _mockRepository
            .Setup(r => r.CreateRefreshTokenAsync(It.IsAny<RefreshToken>()))
            .Returns(Task.CompletedTask);

        _mockTokenProvider
            .Setup(p => p.CreateAccessToken(It.IsAny<AppUser>(), It.IsAny<IEnumerable<Role>>()))
            .Returns("mocked-jwt-token");
        
        _mockMapper
            .Setup(m => m.Map<LoginResponse>(It.IsAny<object>()))
            .Returns(new LoginResponse
            {
                AccessToken = "mocked-jwt-token",
                RefreshToken = "some-refresh-token"
            });

        int expirationDays = 7;

        // Act
        var response = await _service.LoginAsync(loginRequest, expirationDays);
        Console.WriteLine(response.Message);
        if (response.Body == null)
        {
            Console.WriteLine("Body is null");
        }

        // Assert
        Assert.That(response.IsSuccessful, Is.True);
    
        var body = response.Body as LoginResponse;
        
        Assert.That(body, Is.Not.Null);
    }

    [Test]
    [TestCase("customer1", "wrong_password")]
    public async Task TestLoginWithWrongCredentials(string username, string password)
    {
        // Arrange
        var loginRequest = new LoginRequest
        {
            UserName = username,
            Password = password
        };

        var appUser = new AppUser
        {
            Id = Guid.NewGuid(),
            UserName = loginRequest.UserName,
            Email = $"{username}@example.com"
        };

        _mockRepository
            .Setup(r => r.GetUserAsync(It.IsAny<Expression<Func<AppUser, bool>>>(), It.IsAny<bool>()))
            .ReturnsAsync(appUser);

        _mockUserService
            .Setup(s => s.CheckPasswordAsync(It.IsAny<AppUser>(), loginRequest.Password))
            .ReturnsAsync(false); // simulate wrong password

        int expirationDays = 7;

        // Act
        var response = await _service.LoginAsync(loginRequest, expirationDays);

        // Assert
        Assert.That(response.IsSuccessful, Is.False);
        Assert.That(response.Body, Is.Null);
        Assert.That(response.Message, Is.EqualTo("Username or password is incorrect!"));
    }
}