using HybridApp.Views;
using LegacyWinForms.Connector;

namespace HybridApp.AppBuilder;

public static class HybridAppConfig
{
    public static async Task Run()
    {
        await CreateApp()
            .AddStartupWindow<MainWindow>()

            .Run();
    }

    private static AppHostBuilder CreateApp()
    {
        return AppHostBuilder
            .Create()
            .UseWpf()
            .AddHostService<HybridAppServices>()
            .AddHostApplication<LegacyWinFormsApp>();
    }
}
