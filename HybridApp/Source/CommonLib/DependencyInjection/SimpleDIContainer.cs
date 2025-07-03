using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

namespace CommonLib.DependencyInjection;

/// <summary>
/// DI container kernel.
/// </summary>
internal sealed class SimpleDIContainer
{
    private static int idCounter;
    private readonly ConcurrentDictionary<Type, Func<SimpleDIContainer, ResolverState, object>> mapping;
    private readonly ConcurrentDictionary<Type, Lazy<object>> singletonInstances;

    /// <summary>
    /// Creates a new instance of the <see cref="SimpleDIContainer"/> class.
    /// </summary>
    /// <param name="parentDiContainers">Parent containers from which to inherit type registrations.</param>
    public SimpleDIContainer(params SimpleDIContainer[] parentDiContainers)
    {
        mapping = new ConcurrentDictionary<Type, Func<SimpleDIContainer, ResolverState, object>>();
        singletonInstances = new ConcurrentDictionary<Type, Lazy<object>>();
        Id = idCounter++;

        foreach (var parentDiContainer in parentDiContainers)
        {
            foreach (var registration in parentDiContainer.mapping.ToList())
            {
                mapping[registration.Key] = registration.Value;
            }

            foreach (var singletonInstance in parentDiContainer.singletonInstances.ToList())
            {
                singletonInstances[singletonInstance.Key] = singletonInstance.Value;
            }
        }
    }

    /// <summary>
    /// Gets the container ID.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Registers a type with singleton lifestyle with the container.
    /// An instance of the implementation class will be created for the
    /// first request for the registered interface type, and every
    /// subsequent request will return the same instance (Singleton).
    /// </summary>
    /// <typeparam name="TInterface">The interface type.</typeparam>
    /// <typeparam name="TImplementation">The implementation type.</typeparam>
    public void RegisterSingleton<TInterface, TImplementation>()
        where TImplementation : TInterface
    {
        singletonInstances.TryRemove(typeof(TInterface), out _);
        mapping[typeof(TInterface)] = (_, state) => ResolveSingleton<TInterface, TImplementation>(state);
    }

    /// <summary>
    /// Registers a type with singleton lifestyle with the container.
    /// An instance of the implementation class will be created for the
    /// first request for the registered interface type using the specified
    /// factory method, and every subsequent request will return the same
    /// instance (Singleton).
    /// </summary>
    /// <typeparam name="TInterface">The interface type.</typeparam>
    /// <param name="factory">Factory method for creating the instance.</param>
    public void RegisterSingleton<TInterface>(Func<SimpleDIContainer, TInterface> factory)
    {
        singletonInstances.TryRemove(typeof(TInterface), out _);
        mapping[typeof(TInterface)] = (_, _) => ResolveSingleton(() => factory(this));
    }

    /// <summary>
    /// Registers a concrete instance of a type with the container.
    /// Every request for the type will return this instance.
    /// </summary>
    /// <param name="type">Type of the registered instance.</param>
    /// <param name="instance">the instance to register.</param>
    public void RegisterInstance(Type type, object instance)
    {
        mapping[type] = (_, _) => instance;
    }

    /// <summary>
    /// Registers a concrete instance of a type with the container.
    /// Every request for the type will return this instance.
    /// </summary>
    /// <typeparam name="T">Type of the registered instance.</typeparam>
    /// <param name="instance">the instance to register.</param>
    public void RegisterInstance<T>(T instance)
    {
        RegisterInstance(typeof(T), instance);
    }

    /// <summary>
    /// Registers a type with transient lifestyle with the container.
    /// A new instance of the implementation class will be created for
    /// every request for the registered interface type.
    /// </summary>
    /// <typeparam name="TInterface">The interface type.</typeparam>
    /// <typeparam name="TImplementation">The implementation type.</typeparam>
    public void Register<TInterface, TImplementation>()
        where TImplementation : TInterface
    {
        mapping[typeof(TInterface)] = (container, state) => container.CreateInstance(typeof(TImplementation), state);
    }

    /// <summary>
    /// Registers a type with transient lifestyle with the container.
    /// A new instance of the implementation class will be created for
    /// every request for the registered interface type using the
    /// specified factory method.
    /// </summary>
    /// <typeparam name="TInterface">The interface type.</typeparam>
    /// <param name="factory">Factory method for creating instances of the specified interface type.</param>
    public void Register<TInterface>(Func<SimpleDIContainer, TInterface> factory)
    {
        mapping[typeof(TInterface)] = (container, _) => factory(container);
    }

    /// <summary>
    /// Tries to resolve an instance for the specified type.
    /// </summary>
    /// <param name="type">The type of the instance to resolve.</param>
    /// <returns>The resolved instance for the specified type if the type can be resolved; null, otherwise.</returns>
    public object TryResolve(Type type)
    {
        if (type.IsInterface && !mapping.ContainsKey(type))
        {
            return null;
        }

        try
        {
            return Resolve(type);
        }
        catch (InvalidOperationException)
        {
            return default;
        }
    }

    /// <summary>
    /// Resolves an instance for the specified type.
    /// </summary>
    /// <param name="type">The type of the instance to resolve.</param>
    /// <returns>The resolved instance for the specified type.</returns>
    public object Resolve(Type type)
    {
        var initialState = new ResolverState();
        return Resolve(type, initialState);
    }

    private object Resolve(Type type, ResolverState state)
    {
        if (state == null)
        {
            throw new ArgumentNullException(nameof(state));
        }

        if (state.IsCurrentlyResolving(type))
        {
            var message = CreateErrorMessage($"A cyclic dependency involving type {type} has been detected.", state);
            throw new InvalidOperationException(message);
        }

        using (state.AddCurrentlyResolvingType(type))
        {
            if (type.IsInterface)
            {
                if (!mapping.TryGetValue(type, out var func))
                {
                    var message = CreateErrorMessage($"Type {type} is not registered with the DI container.", state);
                    throw new InvalidOperationException(message);
                }

                return func(this, state);
            }

            if (mapping.TryGetValue(type, out var mappingFunc))
            {
                return mappingFunc(this, state);
            }

            return CreateInstance(type, state);
        }
    }

    private object CreateInstance(Type type, ResolverState state)
    {
        if (state == null)
        {
            throw new ArgumentNullException(nameof(state));
        }

        var constructors = type
            .GetConstructors()
            .GroupBy(x => x.GetParameters().Length)
            .MaxBy(x => x.Key);
        if (constructors == null)
        {
            var message = CreateErrorMessage($"No public .ctor found for type {type}.", state);
            throw new InvalidOperationException(message);
        }

        if (constructors.Count() > 1)
        {
            var message = CreateErrorMessage($"Conflicting .ctor found for type {type}.", state)
                          + Environment.NewLine
                          + "Constructor details:"
                          + Environment.NewLine
                          + string.Join(Environment.NewLine, constructors.Select(c => c.ToString()));
            throw new InvalidOperationException(message);
        }

        var ctorParameters = constructors.Single().GetParameters();
        var args = ctorParameters
            .Select(parameter => Resolve(parameter.ParameterType, state))
            .ToArray();
        return Activator.CreateInstance(type, args);
    }

    private object ResolveSingleton<TInterface, TImplementation>(ResolverState state)
        where TImplementation : TInterface
    {
        var instance = singletonInstances
            .GetOrAdd(typeof(TInterface), new Lazy<object>(() => CreateInstance(typeof(TImplementation), state)));
        return instance.Value;
    }

    private object ResolveSingleton<TInterface>(Func<TInterface> factory)
    {
        var instance = singletonInstances
            .GetOrAdd(typeof(TInterface), new Lazy<object>(() => factory()));
        return instance.Value;
    }

    private static string CreateErrorMessage(string message, ResolverState state)
    {
        var sb = new StringBuilder(message);
        sb.AppendLine($" This type is needed to resolve the requested type {state.TargetType}:");
        foreach (var type in state.CurrentlyResolvingTypes)
        {
            sb.AppendLine($"=> {type}");
        }

        return sb.ToString();
    }
}
