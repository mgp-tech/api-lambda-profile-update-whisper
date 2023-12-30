using ProfileUpdate.Core.Exceptions;

namespace ProfileUpdate.Core.Domain.Entity;

public class User : BaseEntity
{
    public bool Creator { get; private set; }
    public string Name { get; private set; }
    public string Nickname { get; private set; }
    public string Email { get; private set; }
    public string DocumentPrimary { get; private set; }
    public string DocumentSecondary { get; private set; }
    public string Phone { get; private set; }
    public string Birthday { get; private set; }
    public string Country { get; private set; }
    public string PostalCode { get; private set; }
    public string Address { get; private set; }
    public string Complement { get; private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }

    public User(Guid id, bool creator, string name, string nickname, string email, string documentPrimary,
        string documentSecondary, string phone, string birthday, string country, string postalCode, string address,
        string complement, string neighborhood, string city, string state) : base(id)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw new EntityException($"{nameof(Name)} can't be null or empty");
        if (string.IsNullOrEmpty(nickname) || string.IsNullOrWhiteSpace(nickname))
            throw new EntityException($"{nameof(Nickname)} can't be null or empty");
        if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
            throw new EntityException($"{nameof(Email)} can't be null or empty");
        if (string.IsNullOrEmpty(documentPrimary) || string.IsNullOrWhiteSpace(documentPrimary))
            throw new EntityException($"{nameof(DocumentPrimary)} can't be null or empty");
        if (string.IsNullOrEmpty(documentSecondary) || string.IsNullOrWhiteSpace(documentSecondary))
            throw new EntityException($"{nameof(DocumentSecondary)} can't be null or empty");
        if (string.IsNullOrEmpty(phone) || string.IsNullOrWhiteSpace(phone))
            throw new EntityException($"{nameof(Phone)} can't be null or empty");
        if (string.IsNullOrEmpty(birthday) || string.IsNullOrWhiteSpace(birthday))
            throw new EntityException($"{nameof(Birthday)} can't be null or empty");
        if (string.IsNullOrEmpty(country) || string.IsNullOrWhiteSpace(country))
            throw new EntityException($"{nameof(Country)} can't be null or empty");
        if (string.IsNullOrEmpty(postalCode) || string.IsNullOrWhiteSpace(postalCode))
            throw new EntityException($"{nameof(PostalCode)} can't be null or empty");
        if (string.IsNullOrEmpty(address) || string.IsNullOrWhiteSpace(address))
            throw new EntityException($"{nameof(Address)} can't be null or empty");
        if (string.IsNullOrEmpty(complement) || string.IsNullOrWhiteSpace(complement))
            throw new EntityException($"{nameof(Complement)} can't be null or empty");
        if (string.IsNullOrEmpty(neighborhood) || string.IsNullOrWhiteSpace(neighborhood))
            throw new EntityException($"{nameof(Neighborhood)} can't be null or empty");
        if (string.IsNullOrEmpty(city) || string.IsNullOrWhiteSpace(city))
            throw new EntityException($"{nameof(City)} can't be null or empty");
        if (string.IsNullOrEmpty(state) || string.IsNullOrWhiteSpace(state))
            throw new EntityException($"{nameof(State)} can't be null or empty");

        Creator = creator;
        Name = name;
        Nickname = nickname;
        Email = email;
        DocumentPrimary = documentPrimary;
        DocumentSecondary = documentSecondary;
        Phone = phone;
        Birthday = birthday;
        Country = country;
        PostalCode = postalCode;
        Address = address;
        Complement = complement;
        Neighborhood = neighborhood;
        City = city;
        State = state;
    }
}