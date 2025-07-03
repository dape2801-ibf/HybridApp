using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Windows.Threading;
using CommonLib.Contracts;
using CommonLib.FormsIntegration;
using Application = System.Windows.Application;

namespace CommonLib.Services;
public sealed class HybridAppDialogService : IHybridAppDialogService
{
    private readonly IDependencyInjectionContainer container;
    private readonly CultureInfo activeCulture;

    /// <summary>
    /// Initializes a new instance of the <see cref="IHybridAppDialogService"/> class.
    /// </summary>
    public HybridAppDialogService(
        [NotNull] IDependencyInjectionContainer container)
    {
        this.container =
            container ?? throw new ArgumentNullException(nameof(container));
        activeCulture = CultureInfo.CurrentCulture;
    }


    /// <inheritdoc />
    public async Task ShowWindow(Type windowType, Action<Window, IDependencyInjectionContainer> onLoaded = null, Action<Window> onClosed = null,
        CancellationToken cancellationToken = default)
    {
        var tcs = await CreateWindowOnOwnThread(windowType, _ => { }, onLoaded, onClosed, false, cancellationToken);
        await tcs.Task;
    }

    /// <inheritdoc />
    public void ShowWindow(Form form,
        IWin32Window win32Window)
    {
        if (form.InvokeRequired)
        {
            form.Invoke(() => ShowWindow(form, win32Window));
            return;
        }

        if (Thread.CurrentThread.GetApartmentState() == ApartmentState.MTA)
        {
            Application.Current.Dispatcher.Invoke(() => ShowWindow(form, win32Window));
            return;
        }

        new WindowsFormsInteropManager(form).ShowWpfWindowWithFormsContent(container);
    }

    public DialogResult ShowWindowDialog(Form form, IWin32Window win32Window = null)
    {
        if (form.InvokeRequired)
        {
            return form.Invoke(() =>
                ShowWindowDialog(form, win32Window));
        }

        if (Thread.CurrentThread.GetApartmentState() == ApartmentState.MTA)
        {
            return Application.Current.Dispatcher.Invoke(() =>
                ShowWindowDialog(form, win32Window));
        }

        return new WindowsFormsInteropManager(form).ShowWpfWindowWithFormsContentDialog(container);
    }

    private async Task<TaskCompletionSource<object>> CreateWindowOnOwnThread(Type windowType,
        Action<Window> beforeShow,
        Action<Window, IDependencyInjectionContainer> onLoaded,
        Action<Window> onClose,
        bool showModal,
        CancellationToken cancellationToken = default)
    {
        var creationTaskCompletionSource = new TaskCompletionSource<object>();
        TaskCompletionSource<object> tcs = null;
        var windowThread = new Thread(() =>
        {
            var synchronizationContext =
                new DispatcherSynchronizationContext(Dispatcher.CurrentDispatcher);
            SynchronizationContext.SetSynchronizationContext(synchronizationContext);

            if (SetupWindow(windowType, beforeShow, onLoaded, onClose, showModal, cancellationToken,
                    creationTaskCompletionSource, out tcs))
            {
                return;
            }

            RunWindowDispatcher(tcs);
        })
        {
            CurrentCulture = activeCulture,
            CurrentUICulture = activeCulture,
            IsBackground = true
        };
        windowThread.SetApartmentState(ApartmentState.STA);
        windowThread.Name = $"WPF UI-Thread ({windowType.Name})";
        windowThread.IsBackground = true;
        windowThread.Start();

        await creationTaskCompletionSource.Task;
        return tcs;
    }

    /// <summary>
    /// Responsible for the creation and display of a window.
    /// </summary>
    /// <param name="windowType">The type of the Window that need to be created.</param>
    /// <param name="beforeShow">An action to be executed before showing the Window.</param>
    /// <param name="onLoaded">An action to be executed after the Window is loaded.</param>
    /// <param name="onClose">An action to be executed when the Window is closed.</param>
    /// <param name="showModal">A flag indicating whether the window is shown as a modal dialog.</param>
    /// <param name="cancellationToken">Cancellation token that is observed to close the window externally.</param>
    /// <param name="creationTaskCompletionSource">A <see cref="TaskCompletionSource"/> that receives a signal once the window is created.</param>
    /// <param name="tcs">A <see cref="TaskCompletionSource"/> that will receive the result of the Window operation.</param>
    /// <returns>True, if the window is shown as a modal dialog; false, otherwise.</returns>
    private bool SetupWindow(Type windowType,
        Action<Window> beforeShow,
        Action<Window, IDependencyInjectionContainer> onLoaded,
        Action<Window> onClose, bool showModal,
        CancellationToken cancellationToken,
        TaskCompletionSource<object> creationTaskCompletionSource,
        out TaskCompletionSource<object> tcs)
    {
        var wnd = CreateWindow(windowType, (w, c) => { onLoaded?.Invoke(w, c); }, onClose, out tcs);
        creationTaskCompletionSource.TrySetResult(true);

        beforeShow?.Invoke(wnd);
        if (showModal)
        {
            var result = wnd.ShowDialog();
            tcs.TrySetResult(result == true);
            return true;
        }

        wnd.Show();
        var registration = cancellationToken.Register(() => wnd?.Dispatcher.Invoke(() => wnd.Close()));
        wnd.Closed += (_, _) => { registration.Dispose(); };
        return false;
    }

    private Window CreateWindow(Type windowType,
        Action<Window, IDependencyInjectionContainer> onLoaded,
        Action<Window> onClose,
        out TaskCompletionSource<object> tcs)
    {
        var wnd = (Window)container.Resolve(windowType);
        wnd.Language = XmlLanguage.GetLanguage(activeCulture.IetfLanguageTag);
        wnd.WindowStartupLocation = WindowStartupLocation.CenterScreen;

        var taskCompletionSource = new TaskCompletionSource<object>();
        tcs = taskCompletionSource;

        wnd.Loaded += (_, _) =>
        {
            onLoaded?.Invoke(wnd, container);
        };

        wnd.Closed += (_, _) =>
        {
            onClose?.Invoke(wnd);
            taskCompletionSource.SetResult(true);
        };
        return wnd;
    }

    private void RunWindowDispatcher(TaskCompletionSource<object> tcs)
    {
        while (true)
        {
            try
            {
                Dispatcher.Run();
                return;
            }
            catch (OperationCanceledException)
            {
                tcs.TrySetCanceled();
                return;
            }
            catch (Exception e)
            {
                Debugger.Break();
                tcs.TrySetException(e);
            }
        }
    }
}
