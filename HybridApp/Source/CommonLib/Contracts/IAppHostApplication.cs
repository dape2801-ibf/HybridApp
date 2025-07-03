using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace CommonLib.Contracts;
/// <summary>
/// Defines application-specific type registrations
/// and initializations for a hosted application.
/// </summary>
public interface IAppHostApplication
{
    /// <summary>
    /// Gets the short application name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Registers application-specific types.
    /// </summary>
    /// <param name="container">DI container.</param>
    void RegisterTypes([NotNull] IDependencyInjectionContainer container);

    /// <summary>
    /// Executes application-specific initialization code.
    /// </summary>
    /// <param name="container">DI container.</param>
    /// <returns>A task signalling completion of the asynchronous operation.</returns>
    Task Initialize(
        [NotNull] IDependencyInjectionContainer container);
}
