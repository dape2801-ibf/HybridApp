using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.Integration;
using System.Windows.Interop;
using CommonLib.Contracts;
using CommonLib.DependencyInjection;
using Application = System.Windows.Application;

namespace CommonLib.Controls;

public class HybridWindow : Window, IWindow
{
    // ReSharper disable once InconsistentNaming
    public static readonly int WM_SHOWAPP = NativeMethods.RegisterWindowMessage("WM_SHOW_HYBRID_APPHOST");

    /// <summary>
    ///     Dependency property for the <see cref="WindowId" /> property.
    /// </summary>
    public static readonly DependencyProperty WindowIdProperty = DependencyProperty.Register(
        nameof(WindowId),
        typeof(Guid),
        typeof(HybridWindow),
        new PropertyMetadata(Guid.Empty));

    /// <summary>
    ///     Dependency property for the <see cref="WindowName" /> property.
    /// </summary>
    public static readonly DependencyProperty WindowNameProperty = DependencyProperty.Register(
        nameof(WindowName),
        typeof(string),
        typeof(HybridWindow),
        new PropertyMetadata(null));

    /// <summary>
    ///     Static constructor to initialize the <see cref="FrameworkElement.DefaultStyleKey" /> dependency property metadata.
    /// </summary>
    static HybridWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(HybridWindow),
            new FrameworkPropertyMetadata(typeof(HybridWindow)));
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="HybridWindow" /> class.
    /// </summary>
    /// <remarks>
    ///     Windows created here are not registered in the window manager!
    ///     This also means the user won't be prompted for unsaved changes!
    /// </remarks>
    protected HybridWindow()
    {
        ElementHost.EnableModelessKeyboardInterop(this);
        Style = Application.Current.TryFindResource("HybridWindowStyle") as Style;
        WindowId = Guid.NewGuid();

        Closed += OnClosed;
        WindowZoom.SetIsZoomWithMouseWheelEnabled(this, true);
    }


    /// <summary>
    ///     Initializes a new instance of the <see cref="HybridWindow" /> class.
    /// </summary>
    /// <param name="container">The DI container to use for the new window instance.</param>
    protected HybridWindow(IDependencyInjectionContainer container)
        : this()
    {
        DIServiceLocator.SetContainer(this, container);
    }

    /// <summary>
    ///     Gets or sets the window name.
    /// </summary>
    public string WindowName
    {
        get => (string)GetValue(WindowNameProperty);
        set => SetValue(WindowNameProperty, value);
    }

    public bool IsClosed { get; private set; }

    /// <inheritdoc />
    public Guid WindowId
    {
        get
        {
            if (!Dispatcher.CheckAccess())
            {
                return Dispatcher.Invoke(() => WindowId);
            }

            return (Guid)GetValue(WindowIdProperty);
        }
        private set => SetValue(WindowIdProperty, value);
    }

    /// <inheritdoc />
    public async Task ActivateAsync()
    {
        await Dispatcher.InvokeAsync(Activate);
    }

    /// <inheritdoc />
    public async Task CloseAsync()
    {
        await Dispatcher.InvokeAsync(Close);
    }


    /// <inheritdoc />
    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        var source = PresentationSource.FromVisual(this) as HwndSource;
        source?.AddHook(WndProcShowAppMessage);
        source?.AddHook(LogitechMouseHook);
    }

    protected virtual IntPtr WndProcShowAppMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam,
        ref bool handled)
    {
        if (msg == WM_SHOWAPP)
        {
            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
            }

            Activate();
        }

        return IntPtr.Zero;
    }

    protected virtual IntPtr LogitechMouseHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        if (msg == 0x0084 /*WM_NCHITTEST*/)
        {
            // This prevents a crash in WindowChromeWorker._HandleNCHitTest
            try
            {
                lParam.ToInt32();
            }
            catch (OverflowException)
            {
                handled = true;
            }
        }

        return IntPtr.Zero;
    }

    private void OnClosed(object sender, EventArgs e)
    {
        if (DIServiceLocator.Instance == null)
        {
            return;
        }

        IsClosed = true;
        if (Thread.CurrentThread.Name != null && !Thread.CurrentThread.Name.Contains("(Closed)"))
        {
            Thread.CurrentThread.Name += "(Closed)";
        }
    }

    private static class NativeMethods
    {
        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string message);
    }
}