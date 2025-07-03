using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLib.DependencyInjection;

/// <summary>
/// Resolver state, to keep track of currently resolving types for cycle detection.
/// </summary>
internal sealed class ResolverState
{
    private readonly Stack<Type> currentlyResolvingTypes;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResolverState"/> class.
    /// </summary>
    public ResolverState()
    {
        currentlyResolvingTypes = new Stack<Type>();
    }

    /// <summary>
    /// Gets the type that is the initially requested type.
    /// </summary>
    public Type TargetType => currentlyResolvingTypes.LastOrDefault();

    /// <summary>
    /// Gets the currently resolving types, starting from the <see cref="TargetType"/> type.
    /// </summary>
    public IEnumerable<Type> CurrentlyResolvingTypes => currentlyResolvingTypes.Reverse();

    /// <summary>
    /// Checks if the specified type is contained in the collection of currently resolving types.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>
    /// True, if the specified type is contained in the collection of currently resolving types; false, otherwise.
    /// </returns>
    public bool IsCurrentlyResolving(Type type)
    {
        return currentlyResolvingTypes.Contains(type);
    }

    /// <summary>
    /// Adds a type to the collection of currently resolving types.
    /// </summary>
    /// <param name="type">The type to add to the collection of currently resolving types.</param>
    /// <returns>
    /// A token that, when disposed, removes the added type from the collection of currently resolving types.
    /// </returns>
    public IDisposable AddCurrentlyResolvingType(Type type)
    {
        currentlyResolvingTypes.Push(type);
        return new CurrentlyResolvingTypeRemoval(this);
    }

    private class CurrentlyResolvingTypeRemoval : IDisposable
    {
        private readonly ResolverState owner;

        public CurrentlyResolvingTypeRemoval(ResolverState owner)
        {
            this.owner = owner;
        }

        public void Dispose()
        {
            owner.currentlyResolvingTypes.Pop();
        }
    }
}
