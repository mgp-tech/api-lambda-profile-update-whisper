using ProfileUpdate.Application.Services;
using ProfileUpdate.Core.Adapters.Repository.Users;

namespace ProfileUpdate.Test.Unit.Application.Services
{
    public class UpdateUserServiceTests
    {
        private readonly Mock<IUpdateUserRepository> _mockRepo;
        private readonly UpdateUserService _service;
        private readonly Faker _faker;

        public UpdateUserServiceTests()
        {
            _mockRepo = new Mock<IUpdateUserRepository>();
            Mock<ILogger<UpdateUserService>> mockLogger = new();
            _service = new UpdateUserService(_mockRepo.Object, mockLogger.Object);
            _faker = new Faker();
        }

        private User CreateFakeUser()
        {
            return new User(
                Guid.NewGuid(),
                _faker.Random.Bool(),
                _faker.Person.FullName,
                _faker.Internet.UserName(),
                _faker.Internet.Email(),
                _faker.Random.String2(10),
                _faker.Random.String2(10),
                _faker.Phone.PhoneNumber(),
                _faker.Date.Past(30).ToString("yyyy-MM-dd"),
                _faker.Address.Country(),
                _faker.Address.ZipCode(),
                _faker.Address.StreetAddress(),
                _faker.Address.SecondaryAddress(),
                _faker.Address.CityPrefix(),
                _faker.Address.City(),
                _faker.Address.State());
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnTrue_WhenDataIsValid()
        {
            var user = CreateFakeUser();
            string jsonData = JsonConvert.SerializeObject(user);

            var result = await _service.ExecuteAsync(jsonData);

            result.Should().BeTrue();
        }


        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task ExecuteAsync_ShouldReturnFalse_WhenDataIsInvalid(string invalidData)
        {
            var result = await _service.ExecuteAsync(invalidData);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnFalse_WhenDeserializationFails()
        {
            var result = await _service.ExecuteAsync("Invalid JSON");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnFalse_WhenRepositoryThrowsException()
        {
            _mockRepo.Setup(repo => repo.ExecuteAsync(It.IsAny<User>()))
                .ThrowsAsync(new Exception("Database error"));

            var fakeUser = CreateFakeUser();
            string jsonFakeUser = JsonConvert.SerializeObject(fakeUser);

            var result = await _service.ExecuteAsync(jsonFakeUser);

            result.Should().BeFalse();
        }
    }
}