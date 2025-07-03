using System.Diagnostics;
using System.Globalization;
using System.Windows.Threading;
using CommonLib.Contracts;
using LegacyWinForms.Contracts;

namespace LegacyWinForms.Connector;
public class LegacyWinFormsApp : IAppHostApplication
{
    public string Name => "LegacyWinFormsApp";
    public void RegisterTypes(IDependencyInjectionContainer container)
    {
        // Register the callbacks for the legacy WinForms integration
        container.RegisterSingleton<ILegacyWinFormsCallbacks, LegacyWinFormsCallbacks>();
    }

    public async Task Initialize(IDependencyInjectionContainer container)
    {
        var tcs = new TaskCompletionSource<bool>();
        var thread = new Thread(async () =>
        {
            try
            {
                SynchronizationContext.SetSynchronizationContext(
                    new DispatcherSynchronizationContext(Dispatcher.CurrentDispatcher));
                var startup = container.Resolve<LegacyWinFormsStartUp>();
                _ = startup.StartEmbedded().ContinueWith(t =>
                {
                    if (t.IsCanceled)
                    {
                        tcs.TrySetCanceled();
                        return;
                    }

                    tcs.TrySetResult(true);
                });

                if (!RunApplication(new ApplicationContext()))
                {
                    throw new OperationCanceledException("Fatal HybridApp error");
                }
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                if (ex is OperationCanceledException)
                {
                    throw;
                }
            }
        });
        thread.TrySetApartmentState(ApartmentState.STA);
        thread.IsBackground = true;
        thread.Name = "HybridApp UI Thread";

        {
            // CurrentCulture handling/updating
            void SetCulture(CultureInfo activeCulture)
            {
                if (activeCulture == null)
                {
                    return;
                }

                Dispatcher.FromThread(thread)?.Invoke(() =>
                {
                    thread.CurrentCulture = activeCulture;
                    thread.CurrentUICulture = activeCulture;
                });
            }
        }

        thread.Start();
        await tcs.Task;
    }

    private bool RunApplication(ApplicationContext context)
    {
        while (true)
        {
            try
            {
                Application.Run(context);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
