namespace ProfileUpdate.Core.Adapters.Services;

public interface IUpdateUserService
{
    Task<bool> ExecuteAsync(string data);
}