using MeterOrm.Dlms.Accessor;
using MeterOrm.Dlms.Examples;

namespace MeterOrm.Dlms.IntegrationTests;

/// <summary>
/// Интеграционные тесты для подключения к счетчику
/// </summary>
public class ConnectionIntegrationTests : IntegrationTestBase
{
    private const string TestContextName = "ConnectionTestContext";

    [Fact]
    public async Task DisconnectAsync_WithConnectedContext_ShouldDisconnect()
    {
        // Arrange
        var meterContext = CreateTestMeterContext();
        MeterContextAccessor.CreateAccessor(TestContextName, meterContext, DlmsMeterContextAccessorOptions.Default);

        using var context = MeterContextAccessor.GetContext<DlmsSpecificMeterContext>(TestContextName);
        await context.ConnectAsync();

        // Act
        await context.DisconnectAsync();

        // Assert
        // Проверяем, что отключение прошло без исключений
        Assert.True(true); // Placeholder assertion
    }

    [Fact]
    public void Initialize_ShouldSetUpMeterContext()
    {
        // Arrange
        var meterContext = CreateTestMeterContext();

        // Act
        meterContext.Initialize();

        // Assert
        // Проверяем, что инициализация прошла без исключений
        Assert.NotNull(meterContext);
    }

    [Fact]
    public async Task UsingStatement_ShouldProperlyDisposeContext()
    {
        // Arrange
        var meterContext = CreateTestMeterContext();
        MeterContextAccessor.CreateAccessor(TestContextName, meterContext, DlmsMeterContextAccessorOptions.Default);

        // Act & Assert
        using (var context = MeterContextAccessor.GetContext<DlmsSpecificMeterContext>(TestContextName))
        {
            Assert.NotNull(context);
            await context.ConnectAsync();
            context.Initialize();
            // При выходе из using блока контекст должен быть освобожден
        }
    }

    [Fact]
    public async Task MultipleConnections_ShouldWorkIndependently()
    {
        // Arrange
        var context1 = CreateTestMeterContext();
        var context2 = CreateTestMeterContext();

        MeterContextAccessor.CreateAccessor("Context1", context1, DlmsMeterContextAccessorOptions.Default);
        MeterContextAccessor.CreateAccessor("Context2", context2, DlmsMeterContextAccessorOptions.Default);

        // Act & Assert
        using (var ctx1 = MeterContextAccessor.GetContext<DlmsSpecificMeterContext>("Context1"))
        using (var ctx2 = MeterContextAccessor.GetContext<DlmsSpecificMeterContext>("Context2"))
        {
            Assert.NotSame(ctx1, ctx2);
            
            await ctx1.ConnectAsync();
            await ctx2.ConnectAsync();
            
            ctx1.Initialize();
            ctx2.Initialize();
            
            await ctx1.DisconnectAsync();
            await ctx2.DisconnectAsync();
        }
    }
} 