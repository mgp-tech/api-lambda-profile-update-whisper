using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileUpdate.Core.Adapters.Repository.Users;
using ProfileUpdate.Core.Adapters.Services;
using ProfileUpdate.Core.Domain.Entity;

namespace ProfileUpdate.Application.Services
{
    public class UpdateUserService : IUpdateUserService
    {
        private readonly IUpdateUserRepository _updateUserRepository;
        private readonly ILogger<UpdateUserService> _logger;

        public UpdateUserService(IUpdateUserRepository updateUserRepository, ILogger<UpdateUserService> logger)
        {
            _updateUserRepository = updateUserRepository ?? throw new ArgumentNullException(nameof(updateUserRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> ExecuteAsync(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                _logger.LogError("Received empty data for UpdateUserService.ExecuteAsync.");
                return false;
            }

            try
            {
                var user = JsonConvert.DeserializeObject<User>(data);
                if (user == null)
                {
                    _logger.LogError("Failed to deserialize data to a User object in UpdateUserService.ExecuteAsync.");
                    return false;
                }

                await _updateUserRepository.ExecuteAsync(user);
                return true;
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, $"JSON deserialization error in UpdateUserService.ExecuteAsync: {jsonEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in UpdateUserService.ExecuteAsync: {ex.Message}");
                return false;
            }
        }
    }
}