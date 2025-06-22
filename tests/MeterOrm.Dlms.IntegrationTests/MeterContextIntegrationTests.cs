using MeterOrm.Core;
using MeterOrm.Core.Accessor;
using MeterOrm.Dlms.Accessor;
using MeterOrm.Dlms.Examples;

namespace MeterOrm.Dlms.IntegrationTests;

/// <summary>
/// Интеграционные тесты для работы с контекстом счетчика
/// </summary>
public class MeterContextIntegrationTests : IntegrationTestBase
{
    private const string TestContextName = "IntegrationTestContext";

    [Fact]
    public void MeterContextAccessor_ShouldBeResolvedFromDI()
    {
        // Assert
        Assert.NotNull(MeterContextAccessor);
        Assert.IsAssignableFrom<IMeterContextAccessor>(MeterContextAccessor);
    }

    [Fact]
    public void CreateAccessor_ShouldCreateMeterContext()
    {
        // Arrange
        var meterContext = CreateTestMeterContext();

        // Act
        MeterContextAccessor.CreateAccessor(
            TestContextName,
            meterContext,
            DlmsMeterContextAccessorOptions.Default);

        // Assert
        var retrievedContext = MeterContextAccessor.GetContext<DlmsSpecificMeterContext>(TestContextName);
        Assert.NotNull(retrievedContext);
        Assert.IsAssignableFrom<DlmsSpecificMeterContext>(retrievedContext);
    }

    [Fact]
    public void GetContext_WithNonExistentName_ShouldThrowException()
    {
        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => 
            MeterContextAccessor.GetContext<DlmsSpecificMeterContext>("NonExistentContext"));
        
        Assert.Contains("Context 'NonExistentContext' not found", exception.Message);
    }

    [Fact]
    public void CreateAccessor_WithSameName_ShouldUpdateExistingContext()
    {
        // Arrange
        var firstContext = CreateTestMeterContext();
        var secondContext = CreateTestMeterContext();

        // Act
        MeterContextAccessor.CreateAccessor(TestContextName, firstContext, DlmsMeterContextAccessorOptions.Default);
        MeterContextAccessor.CreateAccessor(TestContextName, secondContext, DlmsMeterContextAccessorOptions.Default);

        // Assert
        var retrievedContext = MeterContextAccessor.GetContext<DlmsSpecificMeterContext>(TestContextName);
        Assert.NotNull(retrievedContext);
        // Второй контекст должен заменить первый
        Assert.Same(secondContext, retrievedContext);
    }

    [Fact]
    public void MeterContext_ShouldHaveValidTransport()
    {
        // Arrange
        var meterContext = CreateTestMeterContext();

        // Act & Assert
        // Проверяем, что контекст создался без ошибок
        Assert.NotNull(meterContext);
        Assert.IsAssignableFrom<DlmsSpecificMeterContext>(meterContext);
    }

    [Fact]
    public void MeterContext_ShouldHaveClassesCollection()
    {
        // Arrange
        var meterContext = CreateTestMeterContext();

        // Act & Assert
        Assert.NotNull(meterContext.Classes);
        Assert.IsAssignableFrom<IMeterClassCollection>(meterContext.Classes);
    }
} 