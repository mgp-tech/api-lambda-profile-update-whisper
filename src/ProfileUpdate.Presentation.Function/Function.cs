using System.Diagnostics.CodeAnalysis;
using Amazon.Lambda.APIGatewayEvents;
using ProfileUpdate.Core.Adapters.Services;


[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ProfileUpdate.Presentation.Function;

public class Function
{
    private readonly ServiceProvider _provider;

    public Function(IServiceProvider serviceProvider)
    {
        var service = new ServiceCollection();
        service.RegisterDependencies();
        _provider = service.BuildServiceProvider();
    }

    [ExcludeFromCodeCoverage]
    public Function()
    {
        var service = new ServiceCollection();
        service.RegisterDependencies();
        _provider = service.BuildServiceProvider();
    }

    public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
    {
        var logger = _provider.GetService<ILogger<Function>>();
        try
        {
            logger?.LogInformation("Function started");

            var service = _provider.GetService<IUpdateUserService>();
            if (service == null)
            {
                logger?.LogError("UpdateUserService not found.");
                return new APIGatewayProxyResponse
                {
                    StatusCode = 500,
                    Body = "Service not available"
                };
            }

            var isSuccess = await service.ExecuteAsync(input.Body);
            if (isSuccess)
            {
                logger?.LogInformation("User update successful.");
                return new APIGatewayProxyResponse
                {
                    StatusCode = 200,
                    Body = "User updated successfully."
                };
            }
            else
            {
                logger?.LogWarning("User update failed.");
                return new APIGatewayProxyResponse
                {
                    StatusCode = 400,
                    Body = "User update failed."
                };
            }
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, $"An error occurred: {ex.Message}");
            return new APIGatewayProxyResponse
            {
                StatusCode = 500,
                Body = $"An error occurred: {ex.Message}"
            };
        }
    }
}