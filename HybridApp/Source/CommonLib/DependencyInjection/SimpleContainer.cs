using System;
using CommonLib.Contracts;

namespace CommonLib.DependencyInjection;

/// <summary>
/// DI container implementation.
/// </summary>
public class SimpleContainer : IDependencyInjectionContainer
{
    private readonly SimpleDIContainer kernel;

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleContainer"/> class.
    /// </summary>
    public SimpleContainer()
        : this(CreateDefaultKernel())
    {
    }

    private SimpleContainer(SimpleDIContainer kernel)
    {
        this.kernel = kernel;
        RegisterInstance<IDependencyInjectionContainer>(this);
    }

    /// <inheritdoc />
    public IDependencyInjectionContainer RegisterSingleton<TImplementation>()
    {
        kernel.RegisterSingleton<TImplementation, TImplementation>();
        return this;
    }

    /// <inheritdoc />
    public IDependencyInjectionContainer RegisterSingleton<TInterface, TImplementation>()
        where TImplementation : TInterface
    {
        kernel.RegisterSingleton<TInterface, TImplementation>();
        return this;
    }

    /// <inheritdoc />
    public IDependencyInjectionContainer RegisterSingleton<TInterface>(Func<IDependencyInjectionContainer, TInterface> factory)
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory));
        }

        kernel.RegisterSingleton(container =>
            factory((IDependencyInjectionContainer)container.Resolve(typeof(IDependencyInjectionContainer))));
        return this;
    }

    /// <inheritdoc />
    public IDependencyInjectionContainer RegisterInstance<T>(T instance)
    {
        if (instance == null)
        {
            throw new ArgumentNullException(nameof(instance));
        }

        kernel.RegisterInstance(instance);
        return this;
    }

    /// <inheritdoc />
    public IDependencyInjectionContainer RegisterType<TInterface, TImplementation>()
        where TImplementation : TInterface
    {
        kernel.Register<TInterface, TImplementation>();
        return this;
    }

    /// <inheritdoc />
    public IDependencyInjectionContainer RegisterType<TInterface>(
        Func<IDependencyInjectionContainer, TInterface> factory)
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory));
        }

        kernel.Register(container =>
            factory((IDependencyInjectionContainer)container.Resolve(typeof(IDependencyInjectionContainer))));
        return this;
    }

    /// <inheritdoc />
    public T Resolve<T>()
    {
        var obj = kernel.Resolve(typeof(T));
        return obj is T instance ? instance : throw new NullReferenceException();
    }

    /// <inheritdoc />
    public object Resolve(Type type)
    {
        if (type == null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        return kernel.Resolve(type);
    }

    /// <inheritdoc />
    public T TryResolve<T>()
    {
        var obj = kernel.TryResolve(typeof(T));
        return obj is T instance ? instance : default;
    }

    /// <inheritdoc />
    public IDependencyInjectionContainer CreateChildContainer()
    {
        var childContainer = new SimpleDIContainer(kernel);
        return new SimpleContainer(childContainer);
    }

    /// <inheritdoc />
    public void Dispose()
    {
    }

    private static SimpleDIContainer CreateDefaultKernel() => new();
}
