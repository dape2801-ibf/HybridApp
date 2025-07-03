using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using CommonLib.Contracts;
using CommonLib.DependencyInjection;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace HybridApp.AppBuilder;

internal class AppHostBuilder
{
    private readonly IDependencyInjectionContainer container;
    private readonly InitializerCollection appStartupInitializations;
    private readonly RegistrationsCollection registrationFunctions;
    private readonly InitializerCollection initializationFunctions;
    private Application app;
    private Type startupWindow;

    private AppHostBuilder(IDependencyInjectionContainer container)
    {
        appStartupInitializations = new InitializerCollection();
        initializationFunctions = new InitializerCollection();
        registrationFunctions = new RegistrationsCollection();

        this.container = container;
        DIServiceLocator.SetInstance(container);

        var applicationHost = new ApplicationHost(container);
        container.RegisterInstance(applicationHost);
    }

    public IDependencyInjectionContainer Container => container;

    /// <summary>
    /// Creates a new <see cref="AppHostBuilder"/> instance.
    /// </summary>
    /// <returns>The <see cref="AppHostBuilder"/> instance.</returns>
    public static AppHostBuilder Create()
    {
        var appHostBuilder = new AppHostBuilder(new SimpleContainer());

        return appHostBuilder;
    }

    /// <summary>
    /// Configure the application host to use WPF as technology:
    /// - the required themes are initialized,
    /// - handlers for unhandled exceptions are registered,
    /// - the shutdown mode is set.
    /// </summary>
    /// <returns>The <see cref="AppHostBuilder"/> instance.</returns>
    public AppHostBuilder UseWpf()
    {
        app = Application.Current ?? new Application();
        app.ShutdownMode = ShutdownMode.OnLastWindowClose;
        app.DispatcherUnhandledException += OnUnhandledWpfException;
        AppDomain.CurrentDomain.UnhandledException += OnUnhandledAppDomainException;
        TaskScheduler.UnobservedTaskException += OnUnhandledTaskException;

        return this;
    }

    /// <summary>
    /// Configures the application host to provide a specified
    /// <see cref="IAppHostService"/> service for the application.
    /// </summary>
    /// <typeparam name="T">The service type to add.</typeparam>
    /// <returns>The <see cref="AppHostBuilder"/> instance.</returns>
    public AppHostBuilder AddHostService<T>()
        where T : IAppHostService
    {
        var hostedService = container.Resolve<T>();
        registrationFunctions.Add(() => { hostedService.RegisterTypes(container); });
        initializationFunctions.Add(async () =>
        {
            await hostedService.Initialize(container);
        });

        return this;
    }

    /// <summary>
    /// Configures the application host to create and register a specified
    /// <see cref="IAppHostApplication"/> hosted application.
    /// </summary>
    /// <remarks>
    /// A hosted application can be the NormManager, the CheckManager, etc.
    /// </remarks>
    /// <typeparam name="T">The hosted application type to add.</typeparam>
    /// <returns>The <see cref="AppHostBuilder"/> instance.</returns>
    public AppHostBuilder AddHostApplication<T>()
        where T : IAppHostApplication
    {
        var hostedApp = container.Resolve<T>();
        registrationFunctions.Add(() =>
        {
            hostedApp.RegisterTypes(container);
        });
        initializationFunctions.Add(async () =>
        {
            await hostedApp.Initialize(container);
        });

        return this;
    }

    /// <summary>
    /// Defines the startup window for the application, which is shown first.
    /// </summary>
    /// <typeparam name="T">The startup window type for the application.</typeparam>
    /// <returns>The <see cref="AppHostBuilder"/> instance.</returns>
    public AppHostBuilder AddStartupWindow<T>()
        where T : Window
    {
        startupWindow = typeof(T);
        return this;
    }

    public async Task Initialize()
    {
        await appStartupInitializations.Execute();
        registrationFunctions.Execute();
        await initializationFunctions.Execute();
    }

    /// <summary>
    /// Runs the configured application:
    /// 1. all configured <see cref="IAppHostStartup"/> startup initialization code is executed,
    /// 2. all configured, hosted services are registered and initialized,
    /// 3. all configured, hosted applications are registered and initialized.
    /// </summary>
    /// <returns>A task that completes when the application has stopped.</returns>
    public async Task Run()
    {
        try
        {
            await appStartupInitializations.Execute();

            await AppHostRunner.Run(container,
                registrationFunctions,
                initializationFunctions,
                startupWindow);
        }
        catch (OperationCanceledException)
        {
            Application.Current.Shutdown();
        }
        catch (Exception startupException)
        {
            MessageBox.Show("Critical error on startup.\r\n" + startupException.Message,
                "Startup error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            Application.Current.Shutdown();
        }
    }

    private void OnUnhandledTaskException(object sender, UnobservedTaskExceptionEventArgs e)
    {
        e.SetObserved();
        app.Dispatcher.Invoke(() => UnhandledException(e.Exception));
    }

    private void OnUnhandledAppDomainException(object sender, UnhandledExceptionEventArgs e)
    {
        app.Dispatcher.Invoke(() => UnhandledException(e.ExceptionObject as Exception));
    }

    private void OnUnhandledWpfException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        UnhandledException(e.Exception);
    }

    private void UnhandledException(Exception e)
    {
        if (e == null)
        {
            return;
        }

        Debugger.Break();
    }
}