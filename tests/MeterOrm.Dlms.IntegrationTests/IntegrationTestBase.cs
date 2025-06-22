using MeterOrm.Core;
using MeterOrm.Core.Accessor;
using MeterOrm.Dlms.Examples;
using MeterOrm.Dlms.Transport;

namespace MeterOrm.Dlms.IntegrationTests;

/// <summary>
/// Базовый класс для интеграционных тестов с настройкой DI
/// </summary>
public abstract class IntegrationTestBase : IDisposable
{
    protected readonly IServiceProvider ServiceProvider;
    protected readonly IMeterContextAccessor MeterContextAccessor;

    protected IntegrationTestBase()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                // Регистрируем MeterOrm
                services.AddMeterOrm();
                
                // Регистрируем тестовые настройки
                ConfigureTestServices(services);
            })
            .Build();

        ServiceProvider = host.Services;
        MeterContextAccessor = ServiceProvider.GetRequiredService<IMeterContextAccessor>();
    }

    /// <summary>
    /// Настройка дополнительных сервисов для тестов
    /// </summary>
    protected virtual void ConfigureTestServices(IServiceCollection services)
    {
        // Переопределяется в наследниках при необходимости
    }

    /// <summary>
    /// Создание тестового транспорта для подключения к счетчику
    /// </summary>
    protected virtual TcpIpTransport CreateTestTransport()
    {
        // В реальных тестах здесь должны быть настройки тестового счетчика
        // Для демонстрации используем заглушку
        return new TcpIpTransport(
            host: "localhost", // Замените на реальный адрес тестового счетчика
            port: 4059,
            connectionTimeout: TimeSpan.FromSeconds(5),
            readTimeout: TimeSpan.FromSeconds(5),
            writeTimeout: TimeSpan.FromSeconds(5)
        );
    }

    /// <summary>
    /// Создание тестового контекста счетчика
    /// </summary>
    protected virtual DlmsSpecificMeterContext CreateTestMeterContext()
    {
        var transport = CreateTestTransport();
        return new DlmsSpecificMeterContext(
            transport, 
            useKeepAlive: true, 
            keepAlivePeriod: TimeSpan.FromSeconds(2)
        );
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing) 
            return;
        
        if (ServiceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
} 