using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using Application = System.Windows.Forms.Application;

namespace CommonLib.FormsIntegration;
/// <summary>  
/// LegacyKeyboardManager is a helper class designed to manage keyboard input for Windows Forms controls hosted within a WPF application.  
/// This class ensures seamless integration of keyboard events between the two UI frameworks.  
///  
/// <para>  
/// When hosting Windows Forms controls in WPF, keyboard input handling can become challenging due to differences in event propagation.  
/// LegacyKeyboardManager addresses this by installing a message filter on the current thread to process keyboard-related messages.  
/// </para>  
///  
/// <para>  
/// This class is thread-specific and uses a thread-local instance to manage Windows Forms hosts on a per-thread basis.  
/// </para>  
/// </summary>  
internal class LegacyKeyboardManager
{
    /// <summary>  
    /// Thread-local instance of LegacyKeyboardManager for the current thread.  
    /// </summary>  
    [ThreadStatic] private static LegacyKeyboardManager currentThreadInstance;

    /// <summary>  
    /// A list of weak references to WindowsFormsHost instances managed by this class.  
    /// </summary>  
    private readonly WeakRefList<WindowsFormsHost> knownThreadWindows = new();

    /// <summary>  
    /// Indicates whether the message filter has been installed on the current thread.  
    /// </summary>  
    private bool messageFilterInstalledOnThread;

    /// <summary>  
    /// Initializes a new instance of the LegacyKeyboardManager class for the specified thread.  
    /// </summary>  
    /// <param name="currentThread">The thread on which this instance operates.</param>  
    private LegacyKeyboardManager(Thread currentThread)
    {
        CurrentThread = currentThread;
    }

    /// <summary>  
    /// Gets the thread associated with this LegacyKeyboardManager instance.  
    /// </summary>  
    public Thread CurrentThread { get; }

    /// <summary>  
    /// Gets the thread-local instance of LegacyKeyboardManager for the current thread.  
    /// If no instance exists, a new one is created.  
    /// </summary>  
    public static LegacyKeyboardManager CurrentThreadInstance => currentThreadInstance ??= new LegacyKeyboardManager(Thread.CurrentThread);

    /// <summary>  
    /// Installs the LegacyKeyboardManager for the specified WindowsFormsHost instance.  
    /// This method ensures that keyboard input is properly handled for the hosted Windows Forms control.  
    /// </summary>  
    /// <param name="ibfWindowsFormsHost">The WindowsFormsHost instance to manage.</param>  
    public void Install(WindowsFormsHost ibfWindowsFormsHost)
    {
        knownThreadWindows.Add(ibfWindowsFormsHost);
        if (messageFilterInstalledOnThread)
        {
            return;
        }

        messageFilterInstalledOnThread = true;
        ComponentDispatcher.ThreadFilterMessage += ThreadMessageFilter;
    }

    /// <summary>  
    /// Filters thread messages to process keyboard input for hosted Windows Forms controls.  
    /// </summary>  
    /// <param name="msg">The message to filter.</param>  
    /// <param name="outHandled">Indicates whether the message was handled.</param>  
    private void ThreadMessageFilter(ref MSG msg, ref bool outHandled)
    {
        var knownWindow = knownThreadWindows.FirstOrDefault(x => x.Child != null &&
                                                                 Window.GetWindow(x) != null);
        if (knownWindow == null)
        {
            return;
        }

        var windowsFormsMessage = new Message
        {
            HWnd = msg.hwnd,
            LParam = msg.lParam,
            Msg = msg.message,
            WParam = msg.wParam
        };

        if (Application.FilterMessage(ref windowsFormsMessage))
        {
            outHandled = true;
        }
        else
        {
            var flag = false;
            var control = Control.FromChildHandle(windowsFormsMessage.HWnd);
            if (control != null && !control.IsDisposed)
            {
                if (windowsFormsMessage.Msg == 262)
                {
                    flag = control.PreProcessMessage(ref windowsFormsMessage);
                }
                else
                {
                    switch (control.PreProcessControlMessage(ref windowsFormsMessage))
                    {
                        case PreProcessControlState.MessageProcessed:
                            flag = true;
                            break;
                        case PreProcessControlState.MessageNeeded:
                            TranslateMessage(ref msg);
                            DispatchMessage(ref msg);
                            flag = true;
                            break;
                    }
                }
            }
            else if (msg.message != 49827)
            {
                if (!knownWindow.Child.IsDisposed &&
                    knownWindow.Child.PreProcessMessage(ref windowsFormsMessage))
                {
                    flag = true;
                }
            }

            outHandled = flag;
        }
    }

    /// <summary>  
    /// Translates a message into a keyboard event.  
    /// </summary>  
    /// <param name="msg">The message to translate.</param>  
    /// <returns>True if the message was successfully translated; otherwise, false.</returns>  
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool TranslateMessage([In][Out] ref MSG msg);

    /// <summary>  
    /// Dispatches a message to the appropriate window procedure.  
    /// </summary>  
    /// <param name="msg">The message to dispatch.</param>  
    /// <returns>The result of the message processing.</returns>  
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr DispatchMessage([In] ref MSG msg);
}
