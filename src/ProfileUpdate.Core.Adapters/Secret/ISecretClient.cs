namespace ProfileUpdate.Core.Adapters.Secret;

public interface ISecretClient
{
    Task<object> GetAsync(string name);
}