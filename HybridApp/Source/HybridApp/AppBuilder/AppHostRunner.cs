using CommonLib.Contracts;

namespace HybridApp.AppBuilder;

internal class AppHostRunner
{
    public static async Task Run(
        IDependencyInjectionContainer container,
        RegistrationsCollection registrationFunctions,
        InitializerCollection initializationFunctions,
        Type startupWindow)
    {
        registrationFunctions.Execute();

        var applicationHost = (ApplicationHost)container.Resolve<ApplicationHost>();

        await initializationFunctions.Execute();

        ShowMainWindow(container, startupWindow);
        await applicationHost.WaitForExit();
    }

    private static void ShowMainWindow(IDependencyInjectionContainer mainContainer, Type startupWindow)
    {
        if (startupWindow == null)
        {
            return;
        }

        var mainDialogService = mainContainer.Resolve<IHybridAppDialogService>();

        _ = mainDialogService.ShowWindow(startupWindow);
    }
}
