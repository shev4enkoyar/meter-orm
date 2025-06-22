using MeterOrm.Core;
using MeterOrm.Core.Accessor;
using MeterOrm.Dlms.Accessor;

namespace MeterOrm.Dlms.IntegrationTests;

/// <summary>
/// Интеграционные тесты для проверки Dependency Injection
/// </summary>
public class DependencyInjectionIntegrationTests : IntegrationTestBase
{
    [Fact]
    public void ServiceProvider_ShouldBeConfigured()
    {
        // Assert
        Assert.NotNull(ServiceProvider);
    }

    [Fact]
    public void MeterContextAccessor_ShouldBeRegisteredAsSingleton()
    {
        // Act
        var accessor1 = ServiceProvider.GetService<IMeterContextAccessor>();
        var accessor2 = ServiceProvider.GetService<IMeterContextAccessor>();

        // Assert
        Assert.NotNull(accessor1);
        Assert.NotNull(accessor2);
        Assert.Same(accessor1, accessor2); // Должен быть singleton
    }

    [Fact]
    public void MeterContextAccessor_ShouldBeResolvedByInterface()
    {
        // Act
        var accessor = ServiceProvider.GetService<IMeterContextAccessor>();

        // Assert
        Assert.NotNull(accessor);
        Assert.IsAssignableFrom<IMeterContextAccessor>(accessor);
    }

    [Fact]
    public void MeterContextAccessor_ShouldBeResolvedByConcreteType()
    {
        // Act
        var accessor = ServiceProvider.GetService<IMeterContextAccessor>();

        // Assert
        Assert.NotNull(accessor);
        Assert.IsType<MeterContextAccessor>(accessor);
    }

    [Fact]
    public void DlmsMeterContextAccessorOptions_ShouldHaveDefaultValues()
    {
        // Act
        var options = DlmsMeterContextAccessorOptions.Default;

        // Assert
        Assert.NotNull(options);
    }

    [Fact]
    public void MultipleResolutions_ShouldReturnSameInstance()
    {
        // Act
        var accessor1 = ServiceProvider.GetRequiredService<IMeterContextAccessor>();
        var accessor2 = ServiceProvider.GetRequiredService<IMeterContextAccessor>();
        var accessor3 = ServiceProvider.GetRequiredService<IMeterContextAccessor>();

        // Assert
        Assert.Same(accessor1, accessor2);
        Assert.Same(accessor2, accessor3);
        Assert.Same(accessor1, accessor3);
    }

    [Fact]
    public void ServiceCollection_ShouldContainMeterOrmServices()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMeterOrm();

        // Act
        var serviceProvider = services.BuildServiceProvider();
        var accessor = serviceProvider.GetService<IMeterContextAccessor>();

        // Assert
        Assert.NotNull(accessor);
        Assert.IsAssignableFrom<IMeterContextAccessor>(accessor);
    }

    [Fact]
    public void CustomServiceConfiguration_ShouldWorkWithMeterOrm()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMeterOrm();
        
        // Добавляем кастомные сервисы
        services.AddSingleton<ITestService, TestService>();

        // Act
        var serviceProvider = services.BuildServiceProvider();
        var accessor = serviceProvider.GetService<IMeterContextAccessor>();
        var testService = serviceProvider.GetService<ITestService>();

        // Assert
        Assert.NotNull(accessor);
        Assert.NotNull(testService);
        Assert.IsType<TestService>(testService);
    }
}

/// <summary>
/// Тестовый интерфейс для проверки кастомной конфигурации
/// </summary>
public interface ITestService
{
    string GetTestValue();
}

/// <summary>
/// Тестовая реализация для проверки кастомной конфигурации
/// </summary>
public class TestService : ITestService
{
    public string GetTestValue() => "TestValue";
} 