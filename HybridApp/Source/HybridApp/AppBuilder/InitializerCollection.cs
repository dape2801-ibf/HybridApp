using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace HybridApp.AppBuilder;

/// <summary>
/// Implements a collection of initialization functions.
/// </summary>
internal class InitializerCollection : IEnumerable<Func<Task>>
{
    private readonly List<Func<Task>> initializationFunctions;

    /// <summary>
    /// Initializes a new instance of the <see cref="InitializerCollection"/> class.
    /// </summary>
    public InitializerCollection()
    {
        initializationFunctions = new List<Func<Task>>();
    }

    /// <summary>
    /// Adds a new item to the collection of initialization functions.
    /// </summary>
    /// <param name="initializationFunction">The item to add.</param>
    public void Add([NotNull] Func<Task> initializationFunction)
    {
        if (initializationFunction == null)
        {
            throw new ArgumentNullException(nameof(initializationFunction));
        }

        initializationFunctions.Add(initializationFunction);
    }

    /// <summary>
    /// Executes all items in this initialization function collection sequentially.
    /// </summary>
    /// <param name="progress">An optional progress update provider, reporting percentage completed (incrementally).</param>
    /// <returns>A task signalling completion of the initializations.</returns>
    public async Task Execute(IProgress<double> progress = null)
    {
        if (!initializationFunctions.Any())
        {
            progress?.Report(100);
            return;
        }

        var step = 100 / (double)initializationFunctions.Count;
        foreach (var initializationFunction in initializationFunctions)
        {
            await initializationFunction();
            progress?.Report(step);
        }
    }

    /// <inheritdoc />
    public IEnumerator<Func<Task>> GetEnumerator() => initializationFunctions.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
