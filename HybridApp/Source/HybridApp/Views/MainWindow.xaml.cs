using CommonLib.Contracts;
using CommonLib.DependencyInjection;
using HybridApp.AppBuilder;
using HybridApp.ViewModels;

namespace HybridApp.Views;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow 
{
    public MainWindow(MainWindowViewModel vm, IDependencyInjectionContainer container) : base(container)
    {
        InitializeComponent();
        DataContext = vm;
    }

    protected override async void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        var applicationHost = DIServiceLocator.Instance.TryResolve<ApplicationHost>();
        if (applicationHost == null)
        {
            return;
        }
        await applicationHost.RequestShutdown();
    }
}