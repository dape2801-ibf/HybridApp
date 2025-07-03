using CommonLib.Contracts;
using CommonLib.Services;

namespace HybridApp.AppBuilder;

internal class HybridAppServices : IAppHostService
{
    /// <inheritdoc />
    public string Name => nameof(HybridAppServices);

    /// <inheritdoc />
    public void RegisterTypes(IDependencyInjectionContainer container)
    {
        container.RegisterSingleton<IHybridAppDialogService, HybridAppDialogService>();
    }

    /// <inheritdoc />
    public Task Initialize(IDependencyInjectionContainer container)
    {
        // DesktopServices overwrites some of our fake services, so simply set them again
        RegisterTypes(container);
        return Task.CompletedTask;
    }
}
