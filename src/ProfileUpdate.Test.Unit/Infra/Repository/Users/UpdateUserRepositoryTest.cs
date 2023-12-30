using ProfileUpdate.Infra.Repository.Users;

namespace ProfileUpdate.Test.Unit.Infra.Repository.Users;

public class UpdateUserRepositoryTest
{
    private readonly Faker _faker = new();

    private User CreateValidUser()
    {
        return new User(
            Guid.NewGuid(),
            _faker.Random.Bool(),
            _faker.Name.FullName(),
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

    private DatabaseContext CreateMockedDbContext()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var loggerMock = new Mock<ILogger<DatabaseContext>>();
        var credentialMock = new Mock<ICredential>();
        credentialMock.Setup(c => c.ExecuteAsync())
            .ReturnsAsync("Server=localhost;Database=local;Uid=root;Pwd=123;CharSet=utf8");

        return new DatabaseContext(options, loggerMock.Object, credentialMock.Object);
    }

    [Fact]
    public async Task Should_Update_User()
    {
        using var context = CreateMockedDbContext();
        var repository = new UpdateUserRepository(context, Mock.Of<ILogger<UpdateUserRepository>>());

        var user = CreateValidUser();
        await context.Set<User>().AddAsync(user);
        await context.SaveChangesAsync();

        var updatedUser = new User(
            user.Id,
            user.Creator,
            "Updated Name", // Novo nome
            user.Nickname,
            user.Email,
            user.DocumentPrimary,
            user.DocumentSecondary,
            user.Phone,
            user.Birthday,
            user.Country,
            user.PostalCode,
            user.Address,
            user.Complement,
            user.Neighborhood,
            user.City,
            user.State);

        await repository.ExecuteAsync(updatedUser);
        var result = await context.SaveChangesAsync();
        result.Should().Be(1);
    }

    [Fact]
    public async Task Should_Not_Update_When_Not_Exists_User()
    {
        using var context = CreateMockedDbContext();
        var repository = new UpdateUserRepository(context, Mock.Of<ILogger<UpdateUserRepository>>());

        var user = CreateValidUser();


        var action = async () => await repository.ExecuteAsync(user);
        await action.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"No entity record found with id: {user.Id}");
    }

    [Fact]
    public async Task Should_Throw_RepositoryException_When_Update_User_With_Id_Invalid()
    {
        using var context = CreateMockedDbContext();
        var repository = new UpdateUserRepository(context, Mock.Of<ILogger<UpdateUserRepository>>());

        var invalidUser = new User(
            Guid.Empty,
            _faker.Random.Bool(),
            _faker.Name.FullName(),
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

        var action = async () => await repository.ExecuteAsync(invalidUser);
        await action.Should().ThrowAsync<RepositoryException>()
            .WithMessage("User property Id can't be empty");
    }

    [Fact]
    public async Task Should_Throw_RepositoryException_When_Update_User_Null()
    {
        using var context = CreateMockedDbContext();
        var repository = new UpdateUserRepository(context, Mock.Of<ILogger<UpdateUserRepository>>());

        var action = async () => await repository.ExecuteAsync(null!);
        await action.Should().ThrowAsync<RepositoryException>()
            .WithMessage("User can't be null");
    }
}