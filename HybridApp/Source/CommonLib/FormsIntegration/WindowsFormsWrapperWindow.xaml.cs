using CommonLib.Contracts;

namespace CommonLib.FormsIntegration;

public partial class WindowsFormsWrapperWindow
{
    public WindowsFormsWrapperWindow(IDependencyInjectionContainer container) : base(container)
    {
        InitializeComponent();
        Closed += WindowsFormsWrapperWindow_Closed;
    }

    private void WindowsFormsWrapperWindow_Closed(object? sender, System.EventArgs e)
    {
        CloseForm();
    }

    public bool CloseForm()
    {
        return HwndHost.CloseForm();
    }

    /// <summary>
    ///     The win32 window host containing the winform.
    /// </summary>
    public WindowsFormsHost HwndHost { get; internal set; }
}