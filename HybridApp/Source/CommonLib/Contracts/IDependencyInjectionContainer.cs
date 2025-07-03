using System;
using System.Diagnostics.CodeAnalysis;

namespace CommonLib.Contracts;
/// <summary>
/// Container that can resolve dependencies of registered classes.
/// </summary>
public interface IDependencyInjectionContainer : IDisposable
{
    /// <summary>
    /// Registers a type with singleton lifestyle with the container.
    /// An instance of the implementation class will be created for the
    /// first request for the registered interface type, and every
    /// subsequent request will return the same instance (Singleton).
    /// </summary>
    /// <typeparam name="TInterface">The interface type.</typeparam>
    /// <typeparam name="TImplementation">The implementation type.</typeparam>
    IDependencyInjectionContainer RegisterSingleton<TInterface, TImplementation>()
        where TImplementation : TInterface;

    /// <summary>
    /// Registers a type with singleton lifestyle with the container.
    /// An instance of the type will be created for the first request,
    /// and every subsequent request will return the same instance (Singleton).
    /// </summary>
    /// <typeparam name="TImplementation">The type to register.</typeparam>
    IDependencyInjectionContainer RegisterSingleton<TImplementation>();

    /// <summary>
    /// Registers a type with singleton lifestyle with the container.
    /// An instance of the implementation class will be created for the
    /// first request for the registered interface type using the specified
    /// factory method, and every subsequent request will return the same
    /// instance (Singleton).
    /// </summary>
    /// <typeparam name="TInterface">The interface type.</typeparam>
    /// <param name="factory">Factory method for creating the instance.</param>
    IDependencyInjectionContainer RegisterSingleton<TInterface>(
        [NotNull] Func<IDependencyInjectionContainer, TInterface> factory);

    /// <summary>
    /// Registers a concrete instance of a type with the container.
    /// Every request for the type will return this instance.
    /// </summary>
    /// <typeparam name="TInterface">Type of the registered instance.</typeparam>
    /// <param name="instance">The instance to register.</param>
    IDependencyInjectionContainer RegisterInstance<TInterface>([NotNull] TInterface instance);

    /// <summary>
    /// Registers a type with transient lifestyle with the container.
    /// A new instance of the implementation class will be created for
    /// every request for the registered interface type.
    /// </summary>
    /// <typeparam name="TInterface">The interface type.</typeparam>
    /// <typeparam name="TImplementation">The implementation type.</typeparam>
    IDependencyInjectionContainer RegisterType<TInterface,TImplementation>()
        where TImplementation : TInterface;

    /// <summary>
    /// Registers a type with transient lifestyle with the container.
    /// A new instance of the implementation class will be created for
    /// every request for the registered interface type using the
    /// specified factory method.
    /// </summary>
    /// <typeparam name="TInterface">The interface type.</typeparam>
    /// <param name="factory">Factory method for creating instances of the specified interface type.</param>
    IDependencyInjectionContainer RegisterType<TInterface>(
        [NotNull] Func<IDependencyInjectionContainer, TInterface> factory);

    /// <summary>
    /// Resolves an instance for the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the instance to resolve.</typeparam>
    /// <returns>The resolved instance for the specified type.</returns>
    /// <remarks>
    /// The lifestyle (transient or singleton) of the requested type depends on the type registration.
    /// </remarks>
    T Resolve<T>();

    /// <summary>
    /// Resolves an instance for the specified type.
    /// </summary>
    /// <param name="type">The type of the instance to resolve.</param>
    /// <returns>The resolved instance for the specified type.</returns>
    /// <remarks>
    /// The lifestyle (transient or singleton) of the requested type depends on the type registration.
    /// </remarks>
    object Resolve([NotNull] Type type);

    /// <summary>
    /// Tries to resolve an instance for the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the instance to resolve.</typeparam>
    /// <returns>The resolved instance for the specified type if the type can be resolved; null, otherwise.</returns>
    /// <remarks>
    /// The lifestyle (transient or singleton) of the requested type depends on the type registration.
    /// </remarks>
    T TryResolve<T>();

    /// <summary>
    /// Creates a child container that inherits the registrations from its parent.
    /// </summary>
    /// <returns>a child container that inherits the registrations from this container instance.</returns>
    IDependencyInjectionContainer CreateChildContainer();
}
