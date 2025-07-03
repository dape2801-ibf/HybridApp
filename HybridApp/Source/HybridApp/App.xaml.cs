using System.Windows;
using DevExpress.Xpf.Core;
using HybridApp.AppBuilder;
using Application = System.Windows.Application;

namespace HybridApp;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    static App()
    {
        CompatibilitySettings.AllowThemePreload = true;
        NetFramework.ConfigureRuntime();
    }
    protected override async void OnStartup(StartupEventArgs e)
    {
        await HybridAppConfig.Run();
    }
}

