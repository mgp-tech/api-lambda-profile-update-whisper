namespace ProfileUpdate.Test.Unit.Core.Domain.Entity;

public class UserTests
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

    private void TestInvalidField(Action<string> action, string fieldName)
    {
        var invalidValues = new[] { "", null };
        fieldName.Should().NotBeNull();
        foreach (var value in invalidValues)
        {
            var act = () => action(value);

            act.Should().Throw<EntityException>()
                .WithMessage($"*{fieldName}*");
        }
    }

    [Fact]
    public void Should_ThrowException_When_NameIsInvalid()
    {
        TestInvalidField(value =>
        {
            var user = CreateValidUser();
            var newUser = new User(
                user.Id,
                user.Creator,
                value,
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
        }, nameof(User.Name));
    }


    [Fact]
    public void Should_ThrowException_When_NicknameIsInvalid()
    {
        TestInvalidField(value =>
        {
            var user = CreateValidUser();
            var newUser = new User(
                user.Id,
                user.Creator,
                user.Name,
                value,
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
        }, nameof(User.Nickname));
    }

    [Fact]
    public void Should_ThrowException_When_EmailIsInvalid()
    {
        TestInvalidField(value =>
        {
            var user = CreateValidUser();
            var newUser = new User(
                user.Id,
                user.Creator,
                user.Name,
                user.Nickname,
                value,
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
        }, nameof(User.Email));
    }

    [Fact]
    public void Should_ThrowException_When_DocumentPrimaryIsInvalid()
    {
        TestInvalidField(value =>
        {
            var user = CreateValidUser();
            var newUser = new User(
                user.Id,
                user.Creator,
                user.Name,
                user.Nickname,
                user.Email,
                value,
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
        }, nameof(User.DocumentPrimary));
    }

    [Fact]
    public void Should_ThrowException_When_DocumentSecondaryIsInvalid()
    {
        TestInvalidField(value =>
        {
            var user = CreateValidUser();
            var newUser = new User(
                user.Id,
                user.Creator,
                user.Name,
                user.Nickname,
                user.Email,
                user.DocumentPrimary,
                value,
                user.Phone,
                user.Birthday,
                user.Country,
                user.PostalCode,
                user.Address,
                user.Complement,
                user.Neighborhood,
                user.City,
                user.State);
        }, nameof(User.DocumentSecondary));
    }

    [Fact]
    public void Should_ThrowException_When_PhoneIsInvalid()
    {
        TestInvalidField(value =>
        {
            var user = CreateValidUser();
            var newUser = new User(
                user.Id,
                user.Creator,
                user.Name,
                user.Nickname,
                user.Email,
                user.DocumentPrimary,
                user.DocumentSecondary,
                value,
                user.Birthday,
                user.Country,
                user.PostalCode,
                user.Address,
                user.Complement,
                user.Neighborhood,
                user.City,
                user.State);
        }, nameof(User.Phone));
    }

    [Fact]
    public void Should_ThrowException_When_BirthdayIsInvalid()
    {
        TestInvalidField(value =>
        {
            var user = CreateValidUser();
            var newUser = new User(
                user.Id,
                user.Creator,
                user.Name,
                user.Nickname,
                user.Email,
                user.DocumentPrimary,
                user.DocumentSecondary,
                user.Phone,
                value,
                user.Country,
                user.PostalCode,
                user.Address,
                user.Complement,
                user.Neighborhood,
                user.City,
                user.State);
        }, nameof(User.Birthday));
    }

    [Fact]
    public void Should_ThrowException_When_CountryIsInvalid()
    {
        TestInvalidField(value =>
        {
            var user = CreateValidUser();
            var newUser = new User(
                user.Id,
                user.Creator,
                user.Name,
                user.Nickname,
                user.Email,
                user.DocumentPrimary,
                user.DocumentSecondary,
                user.Phone,
                user.Birthday,
                value,
                user.PostalCode,
                user.Address,
                user.Complement,
                user.Neighborhood,
                user.City,
                user.State);
        }, nameof(User.Country));
    }

    [Fact]
    public void Should_ThrowException_When_PostalCodeIsInvalid()
    {
        TestInvalidField(value =>
        {
            var user = CreateValidUser();
            var newUser = new User(
                user.Id,
                user.Creator,
                user.Name,
                user.Nickname,
                user.Email,
                user.DocumentPrimary,
                user.DocumentSecondary,
                user.Phone,
                user.Birthday,
                user.Country,
                value,
                user.Address,
                user.Complement,
                user.Neighborhood,
                user.City,
                user.State);
        }, nameof(User.PostalCode));
    }

    [Fact]
    public void Should_ThrowException_When_AddressIsInvalid()
    {
        TestInvalidField(value =>
        {
            var user = CreateValidUser();
            var newUser = new User(
                user.Id,
                user.Creator,
                user.Name,
                user.Nickname,
                user.Email,
                user.DocumentPrimary,
                user.DocumentSecondary,
                user.Phone,
                user.Birthday,
                user.Country,
                user.PostalCode,
                value,
                user.Complement,
                user.Neighborhood,
                user.City,
                user.State);
        }, nameof(User.Address));
    }

    [Fact]
    public void Should_ThrowException_When_ComplementIsInvalid()
    {
        TestInvalidField(value =>
        {
            var user = CreateValidUser();
            var newUser = new User(
                user.Id,
                user.Creator,
                user.Name,
                user.Nickname,
                user.Email,
                user.DocumentPrimary,
                user.DocumentSecondary,
                user.Phone,
                user.Birthday,
                user.Country,
                user.PostalCode,
                user.Address,
                value,
                user.Neighborhood,
                user.City,
                user.State);
        }, nameof(User.Complement));
    }

    [Fact]
    public void Should_ThrowException_When_NeighborhoodIsInvalid()
    {
        TestInvalidField(value =>
        {
            var user = CreateValidUser();
            var newUser = new User(
                user.Id,
                user.Creator,
                user.Name,
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
                value,
                user.City,
                user.State);
        }, nameof(User.Neighborhood));
    }

    [Fact]
    public void Should_ThrowException_When_CityIsInvalid()
    {
        TestInvalidField(value =>
        {
            var user = CreateValidUser();
            var newUser = new User(
                user.Id,
                user.Creator,
                user.Name,
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
                value,
                user.State);
        }, nameof(User.City));
    }

    [Fact]
    public void Should_ThrowException_When_StateIsInvalid()
    {
        TestInvalidField(value =>
        {
            var user = CreateValidUser();
            var newUser = new User(
                user.Id,
                user.Creator,
                user.Name,
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
                value);
        }, nameof(User.State));
    }
}