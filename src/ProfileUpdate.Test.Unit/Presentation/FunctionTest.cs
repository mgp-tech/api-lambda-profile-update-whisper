using Amazon.Lambda.APIGatewayEvents;
using ProfileUpdate.Core.Adapters.Services;

namespace ProfileUpdate.Test.Unit.Presentation;

public class FunctionTest
{
    private readonly Mock<IServiceProvider> _serviceProviderMock;
    private readonly Mock<IServiceScope> _serviceScopeMock;
    private readonly Mock<IServiceScopeFactory> _serviceScopeFactoryMock;
    private readonly Mock<IUpdateUserService> _updateUserServiceMock;

    public FunctionTest()
    {
        _serviceProviderMock = new Mock<IServiceProvider>();
        _serviceScopeMock = new Mock<IServiceScope>();
        _serviceScopeFactoryMock = new Mock<IServiceScopeFactory>();
        _updateUserServiceMock = new Mock<IUpdateUserService>();

        _serviceScopeMock.Setup(x => x.ServiceProvider).Returns(_serviceProviderMock.Object);
        _serviceScopeFactoryMock.Setup(x => x.CreateScope()).Returns(_serviceScopeMock.Object);
        _serviceProviderMock.Setup(x => x.GetService(typeof(IServiceScopeFactory))).Returns(_serviceScopeFactoryMock.Object);
        _serviceProviderMock.Setup(x => x.GetService(typeof(IUpdateUserService))).Returns(_updateUserServiceMock.Object);
        _serviceProviderMock.Setup(x => x.GetService(typeof(ILogger<Function>))).Returns(Mock.Of<ILogger<Function>>());
    }

    public async Task FunctionHandler_ShouldReturn200_WhenUpdateIsSuccessful()
    {
        
        _updateUserServiceMock.Setup(service => service.ExecuteAsync(It.IsAny<string>())).ReturnsAsync(true);

        _serviceProviderMock.Setup(x => x.GetService(typeof(IUpdateUserService))).Returns(_updateUserServiceMock.Object);
        var function = new Function(_serviceProviderMock.Object);
        var input = new APIGatewayProxyRequest { Body = "test" };

        var result = await function.FunctionHandler(input, Mock.Of<ILambdaContext>());

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(200);
        result.Body.Should().Be("User updated successfully.");
        
        
    }
    public async Task FunctionHandler_ShouldReturn500_WhenServiceNotAvailable()
    {
        _serviceProviderMock.Setup(x => x.GetService(typeof(IUpdateUserService))).Returns(null); // Simulando serviço não disponível
        var function = new Function(_serviceProviderMock.Object);
        var input = new APIGatewayProxyRequest { Body = "test" };

        var result = await function.FunctionHandler(input, Mock.Of<ILambdaContext>());

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(500);
        result.Body.Should().Be("Service not available");
    }
    
    public async Task FunctionHandler_ShouldReturn400_WhenUpdateFails()
    {
        _updateUserServiceMock.Setup(service => service.ExecuteAsync(It.IsAny<string>())).ReturnsAsync(false);
        var function = new Function(_serviceProviderMock.Object);
        var input = new APIGatewayProxyRequest { Body = "test" };

        var result = await function.FunctionHandler(input, Mock.Of<ILambdaContext>());

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(400);
        result.Body.Should().Be("User update failed.");
    }


    public async Task FunctionHandler_ShouldReturn500_WhenExceptionOccurs()
    {
        _updateUserServiceMock.Setup(service => service.ExecuteAsync(It.IsAny<string>()))
            .ThrowsAsync(new Exception("Internal error"));

        var function = new Function(_serviceProviderMock.Object);
        var input = new APIGatewayProxyRequest { Body = "test" };

        var result = await function.FunctionHandler(input, Mock.Of<ILambdaContext>());

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(500);
        result.Body.Should().Contain("Internal error");
    }
}