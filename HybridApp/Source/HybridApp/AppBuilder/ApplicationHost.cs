using System.Diagnostics.CodeAnalysis;
using CommonLib.Contracts;
using Application = System.Windows.Application;

namespace HybridApp.AppBuilder;

internal class ApplicationHost
{
    private readonly TaskCompletionSource<bool> tcs;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationHost" /> class.
    /// </summary>
    public ApplicationHost([NotNull] IDependencyInjectionContainer container)
    {
        tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        Container = container ?? throw new ArgumentNullException(nameof(container));
    }

    public IDependencyInjectionContainer Container { get; }

    public Task RequestShutdown()
    {
        var application = Application.Current;
        if (application == null)
        {
            return Task.CompletedTask;
        }
        application.Dispatcher.Invoke(() => application.Shutdown());
        Environment.Exit(0);
        return Task.CompletedTask;
    }

    public Task WaitForExit()
    {
        return tcs.Task;
    }
}
