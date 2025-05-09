namespace Identity.UnitTest;

[TestFixture]
public class RegisterTestService
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
    public async Task TestRegister()
    {
        // Arrange
        var registrationRequest = new RegistrationRequest
        {
            UserName = "newuser",
            Email = "newuser@example.com",
            Password = "Valid@123",
            ConfirmPassword = "Valid@123",
            Role = Role.Customer
        };

        var appUser = new AppUser
        {
            Id = Guid.NewGuid(),
            UserName = registrationRequest.UserName,
            Email = registrationRequest.Email
        };

        _mockMapper
            .Setup(m => m.Map<AppUser>(It.IsAny<RegistrationRequest>()))
            .Returns(appUser);

        _mockUserService
            .Setup(s => s.CreateUserAsync(It.IsAny<AppUser>(), registrationRequest.Password))
            .ReturnsAsync(true);

        _mockUserService
            .Setup(s => s.AddUserToRoleAsync(It.IsAny<AppUser>(), registrationRequest.Role.ToString()))
            .ReturnsAsync(true);

        // Act
        var result = await _service.RegisterAsync(registrationRequest);

        // Assert
        Assert.That(result.IsSuccessful, Is.True);
        Assert.That(result.Message, Is.EqualTo("Register successfully!"));
    }
}