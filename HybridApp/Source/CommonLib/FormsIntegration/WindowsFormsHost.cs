using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;

namespace CommonLib.FormsIntegration;
/// <summary>
/// Represents a host for Windows Forms within a WPF application.
/// This class demonstrates how to integrate Windows Forms controls into a WPF application.
/// </summary>
public class WindowsFormsHost : HwndHost, IKeyboardInputSink
{
    /// <summary>
    /// A reflection-based method to set internal state of a Windows Forms <see cref="Form"/>.
    /// Used to manipulate internal modal flags and prevent double disposal.
    /// </summary>
    private static readonly MethodInfo setStateMethod = typeof(Form)
        .GetMethod("SetState", BindingFlags.Instance | BindingFlags.NonPublic);

    /// <summary>
    /// Represents the parent WPF window that hosts the Windows Forms control.
    /// </summary>
    private readonly IWindowsFormsHostWindow window;

    /// <summary>
    /// Tracks whether the hosted form has been closed.
    /// </summary>
    private bool isFormClosed;

    /// <summary>
    /// Tracks whether the parent window is in the process of closing.
    /// </summary>
    private bool isWindowClosing;

    /// <summary>
    /// Initializes a new instance of the <see cref="WindowsFormsHost"/> class.
    /// </summary>
    /// <param name="window">The parent WPF window hosting the form.</param>
    /// <param name="formToHost">The Windows Forms <see cref="Form"/> to be hosted.</param>
    public WindowsFormsHost(IWindowsFormsHostWindow window, Form formToHost)
    {
        this.window = window;
        Child = formToHost;

        // Subscribe to form events
        formToHost.TextChanged += OnTextChanged;
        formToHost.Closed += OnFormsClosed;

        // Install legacy keyboard manager for input handling
        LegacyKeyboardManager.CurrentThreadInstance.Install(this);
    }

    /// <summary>
    /// Gets or sets the hosted Windows Forms <see cref="Form"/>.
    /// </summary>
    public Form Child { get; set; }

    /// <summary>
    /// Handles keyboard focus navigation within the hosted form.
    /// </summary>
    /// <param name="request">The focus navigation request.</param>
    /// <returns>True if focus navigation succeeded; otherwise, false.</returns>
    bool IKeyboardInputSink.TabInto(TraversalRequest request)
    {


       var forward = true;
        var tabStopOnly = true;
        switch (request.FocusNavigationDirection)
        {
            case FocusNavigationDirection.Next:
            case FocusNavigationDirection.First:
                break;
            case FocusNavigationDirection.Previous:
            case FocusNavigationDirection.Last:
                forward = false;
                break;
            case FocusNavigationDirection.Left:
            case FocusNavigationDirection.Up:
                forward = false;
                tabStopOnly = false;
                break;
            case FocusNavigationDirection.Right:
            case FocusNavigationDirection.Down:
                tabStopOnly = false;
                break;
        }

        if (!Child.SelectNextControl(null, forward, tabStopOnly, true, false))
        {
            return false;
        }

        ContainerControl containerControl = Child;
        while (containerControl.ActiveControl is ContainerControl control)
        {
            containerControl = control;
        }

        if (!containerControl.ContainsFocus)
        {
            containerControl.Focus();
        }

        return true;
    }

    /// <summary>
    /// Initializes the hosted Windows Forms control.
    /// </summary>
    private void InitializeWindowsForms()
    {
        FakeWindowsFormsSetInternalModalFlagToAvoidDoubleDispose();
        Child.Visible = true;
        Child.CreateControl();
    }

    /// <summary>
    /// Polls the hosted form to check if it should be closed.
    /// </summary>
    private void CheckCloseDialog()
    {
        if (Child.DialogResult != DialogResult.None && !isFormClosed && !isWindowClosing)
        {
            isFormClosed = true;
            var childHandle = default(IntPtr);

            if (Child.IsHandleCreated)
            {
                childHandle = Child.Handle;
            }

            Child.Close();

            var wmCloseMethod = typeof(Form).GetMethod("WmClose", BindingFlags.Instance | BindingFlags.NonPublic);

            setStateMethod?.Invoke(Child, new object[]
            {
                       32,
                       false
            });
            wmCloseMethod?.Invoke(Child, new object[]
            {
                       new Message
                       {
                           HWnd = childHandle,
                           Msg = 16
                       }
            });

            window.Close();
        }
    }

    /// <summary>
    /// Builds the hosted window core and integrates the Windows Forms control into the WPF window.
    /// </summary>
    /// <param name="hwndParent">The handle of the parent WPF window.</param>
    /// <returns>A handle reference to the hosted form.</returns>
    protected override HandleRef BuildWindowCore(HandleRef hwndParent)
    {
        var childHwnd = new HandleRef(Child, Child.Handle);
        Win32Api.SetWindowStyle(childHwnd.Handle, WindowStyles.WS_CHILD |
                                                  WindowStyles.WS_VISIBLE |
                                                  WindowStyles.WS_CLIPCHILDREN |
                                                  WindowStyles.WS_CLIPSIBLINGS);
        Win32Api.SetParent(childHwnd.Handle, hwndParent.Handle);
        InitializeWindowsForms();
        MessageLoop();
        return childHwnd;
    }

    /// <summary>
    /// Starts a message loop to handle idle events and check for dialog closure.
    /// </summary>
    private async void MessageLoop()
    {
        Application.OleRequired();
        while (!isFormClosed)
        {
            await Task.Delay(50);
            Application.RaiseIdle(EventArgs.Empty);
            CheckCloseDialog();
        }
    }

    /// <summary>
    /// Sets an internal modal flag to prevent double disposal of the hosted form.
    /// </summary>
    private void FakeWindowsFormsSetInternalModalFlagToAvoidDoubleDispose()
    {
        setStateMethod.Invoke(Child, new object[]
        {
               32, true
        });
    }

    /// <summary>
    /// Handles the closure of the hosted form.
    /// </summary>
    private void OnFormsClosed(object sender, EventArgs e)
    {
        if (isFormClosed)
        {
            return;
        }

        isFormClosed = true;
        if (!isWindowClosing)
        {
            window?.Close();
        }
    }

    /// <summary>
    /// Attempts to close the hosted form.
    /// </summary>
    /// <returns>True if the form is closed; otherwise, false.</returns>
    public bool CloseForm()
    {
        if (isFormClosed)
        {
            return true;
        }

        isWindowClosing = true;
        Child.Close();
        isWindowClosing = false;
        CheckCloseDialog();

        if (isFormClosed)
        {
            window?.Close();
        }

        return isFormClosed;
    }

    /// <summary>
    /// Destroys the hosted window core.
    /// </summary>
    /// <param name="hwnd">The handle of the hosted form.</param>
    protected override void DestroyWindowCore(HandleRef hwnd)
    {
    }

    /// <summary>
    /// Updates the title and accessible name of the hosted form when its text changes.
    /// </summary>
    private void OnTextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(Child?.Text))
        {
            window.Title = Child.Text;
            Child.AccessibleName = Child.Text;
            Child.Text = @" ";
        }
    }
}
