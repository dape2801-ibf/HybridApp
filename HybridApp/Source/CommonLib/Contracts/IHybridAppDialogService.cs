using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace CommonLib.Contracts;

public interface IHybridAppDialogService
{
    /// <summary>
    /// Shows a new Wpf window of the specified window type.
    /// </summary>
    /// <param name="windowType">The window type.</param>
    /// <param name="onLoaded">Optional delegate that is invoked when the window has been loaded.</param>
    /// <param name="onClosed">Optional delegate that is invoked when the window has been closed.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that completes when the window has closed.</returns>
    Task ShowWindow(
        Type windowType,
        Action<Window, IDependencyInjectionContainer> onLoaded = null,
        Action<Window> onClosed = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Shows a new Wpf window with WinForms content.
    /// </summary>
    /// <param name="form">The WinForms window</param>
    /// <param name="win32Window">Optional a win32 handle</param>
    void ShowWindow(Form form, IWin32Window win32Window = null);

    /// <summary>
    /// Shows a Wpf window in a dialog with forms content.
    /// </summary>
    /// <param name="form">The form to show.</param>
    /// <param name="win32Window">The owner window.</param>
    /// <returns></returns>
    DialogResult ShowWindowDialog(Form form,
        IWin32Window win32Window = null);
}
