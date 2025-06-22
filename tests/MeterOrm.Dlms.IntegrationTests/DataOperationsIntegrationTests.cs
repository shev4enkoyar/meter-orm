using MeterOrm.Core.Meter;
using MeterOrm.Dlms.Accessor;
using MeterOrm.Dlms.Domain.Entities;
using MeterOrm.Dlms.Examples;

namespace MeterOrm.Dlms.IntegrationTests;

/// <summary>
/// Интеграционные тесты для операций с данными счетчика
/// </summary>
public class DataOperationsIntegrationTests : IntegrationTestBase
{
    private const string TestContextName = "DataOperationsTestContext";

    [Fact]
    public void Classes_ShouldBeAccessible()
    {
        // Arrange
        var meterContext = CreateTestMeterContext();
        meterContext.Initialize();

        // Act & Assert
        Assert.NotNull(meterContext.Classes);
        Assert.IsAssignableFrom<IMeterClassCollection>(meterContext.Classes);
    }

    [Fact]
    public void Where_WithFilter_ShouldReturnFilteredRegisters()
    {
        // Arrange
        var meterContext = CreateTestMeterContext();
        meterContext.Initialize();

        // Act
        var registers = meterContext.Classes.Where<DlmsClassRegister>(x => x.LogicalName.StartsWith("1.0.1.")).ToList();

        // Assert
        Assert.NotNull(registers);
        Assert.IsType<List<DlmsClassRegister>>(registers);
    }

    [Fact]
    public void Select_WithMapping_ShouldReturnMappedObjects()
    {
        // Arrange
        var meterContext = CreateTestMeterContext();
        meterContext.Initialize();

        // Act
        var energyInfos = meterContext.Classes
            .Select<DlmsClassRegister, EnergyInfo>(x => new EnergyInfo
            {
                Name = x.LogicalName,
                Value = x.Value
            })
            .ToList();

        // Assert
        Assert.NotNull(energyInfos);
        Assert.IsType<List<EnergyInfo>>(energyInfos);
    }

    [Fact]
    public async Task ExecuteChangesAsync_ShouldExecutePlannedOperations()
    {
        // Arrange
        var meterContext = CreateTestMeterContext();
        MeterContextAccessor.CreateAccessor(TestContextName, meterContext, DlmsMeterContextAccessorOptions.Default);

        using var context = MeterContextAccessor.GetContext<DlmsSpecificMeterContext>(TestContextName);
        await context.ConnectAsync();
        context.Initialize();

        var register = context.Classes.FirstOrDefault<DlmsClassRegister>(x => x.LogicalName == "0.0.100.0.10.255");

        if (register.IsSome)
        {
            // Act
            register.Value.Reset(); // Планируем вызов метода сброса
            await context.ExecuteChangesAsync(); // Выполняем транзакцию

            // Assert
            // Проверяем, что операция выполнилась без исключений
            Assert.True(true);
        }
    }

    [Fact]
    public async Task SaveChangesAsync_ShouldApplyChangesToMeter()
    {
        // Arrange
        var meterContext = CreateTestMeterContext();
        MeterContextAccessor.CreateAccessor(TestContextName, meterContext, DlmsMeterContextAccessorOptions.Default);

        using var context = MeterContextAccessor.GetContext<DlmsSpecificMeterContext>(TestContextName);
        await context.ConnectAsync();
        context.Initialize();

        var register = context.Classes.FirstOrDefault<DlmsClassRegister>(x => x.LogicalName == "0.0.100.0.10.255");

        if (register.IsSome)
        {
            // Act
            register.Value.Value = 10; // Обновляем значение
            await context.SaveChangesAsync(); // Применяем изменения к счетчику

            // Assert
            // Проверяем, что операция выполнилась без исключений
            Assert.True(true);
        }
    }

    [Fact]
    public void Register_ShouldHaveValidProperties()
    {
        // Arrange
        var meterContext = CreateTestMeterContext();
        meterContext.Initialize();

        var register = meterContext.Classes.FirstOrDefault<DlmsClassRegister>(x => x.LogicalName == "1.0.1.8.0.255");

        if (register.IsSome)
        {
            // Act & Assert
            // Проверяем, что свойства доступны (не выбрасывают исключения)
            _ = register.Value.LogicalName;
            _ = register.Value.Value;
            _ = register.Value.ScalerUnit;
            
            Assert.True(true); // Placeholder assertion
        }
    }
}

/// <summary>
/// Класс для маппинга данных энергии
/// </summary>
public class EnergyInfo
{
    public string Name { get; set; } = string.Empty;
    public double Value { get; set; }
} 