using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace CommonLib.Contracts;
public interface IWindow
{
    /// <summary>
    ///     Is the window already closed
    /// </summary>
    bool IsClosed { get; }

    /// <summary>
    ///     Gets the window id.
    /// </summary>
    Guid WindowId { get; }

    /// <summary>
    ///     Gets or sets a value indicating the left position of the window
    /// </summary>
    double Left { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating the top position of the window
    /// </summary>
    double Top { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating the height of the window
    /// </summary>
    double Height { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating the width of the window
    /// </summary>
    double Width { get; set; }

    /// <summary>
    ///    Gets the current dispatcher of this window
    /// </summary>
    Dispatcher Dispatcher { get; }

    /// <summary>
    ///     Attempts to bring the window to the foreground and activates it.
    /// </summary>
    /// <returns>true if the Window was successfully activated; otherwise, false.</returns>
    bool Activate();

    /// <summary>
    ///     Attempts to bring the window to the foreground and activates it.
    /// </summary>
    /// <returns>A task indicating completion of the asynchronous operation.</returns>
    Task ActivateAsync();

    /// <summary>
    ///     Manually closes the window.
    /// </summary>
    void Close();

    /// <summary>
    ///     Manually closes the window.
    /// </summary>
    /// <returns>A task indicating completion of the asynchronous operation.</returns>
    Task CloseAsync();
}
