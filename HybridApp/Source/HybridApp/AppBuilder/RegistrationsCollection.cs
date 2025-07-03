using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace HybridApp.AppBuilder;

internal class RegistrationsCollection : IEnumerable<Action>
{
    private readonly List<Action> registrationFunctions;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegistrationsCollection"/> class.
    /// </summary>
    public RegistrationsCollection()
    {
        registrationFunctions = new List<Action>();
    }

    /// <summary>
    /// Adds a new item to the collection of registration functions.
    /// </summary>
    /// <param name="registrationFunction">The item to add.</param>
    public void Add([NotNull] Action registrationFunction)
    {
        if (registrationFunction == null)
        {
            throw new ArgumentNullException(nameof(registrationFunction));
        }

        registrationFunctions.Add(registrationFunction);
    }

    /// <summary>
    /// Executes all items in this registration function collection sequentially.
    /// </summary>
    /// <param name="progress">An optional progress update provider, reporting percentage completed (incrementally).</param>
    public void Execute(IProgress<double> progress = null)
    {
        if (!registrationFunctions.Any())
        {
            progress?.Report(100);
            return;
        }

        var step = 100 / (double)registrationFunctions.Count;
        foreach (var initializationFunction in registrationFunctions)
        {
            initializationFunction();
            progress?.Report(step);
        }
    }

    /// <inheritdoc />
    public IEnumerator<Action> GetEnumerator() => registrationFunctions.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
