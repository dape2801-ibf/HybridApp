using System.Threading.Tasks;

namespace CommonLib.Contracts;
/// <summary>
/// A service provider for the application.
/// </summary>
public interface IAppHostService
{
    /// <summary>
    /// Gets the service name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Registers the types that the service provides with the DI container.
    /// </summary>
    /// <param name="container">DI container.</param>
    void RegisterTypes(IDependencyInjectionContainer container);

    /// <summary>
    /// Initializes the service.
    /// </summary>
    /// <param name="container">DI container.</param>
    /// <returns>A task signalling completion of the asynchronous operation.</returns>
    Task Initialize(IDependencyInjectionContainer container);
}
