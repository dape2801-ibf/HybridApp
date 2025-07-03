namespace CommonLib.FormsIntegration;
internal class Win32FormsAdapter : IWindowsFormsHostWindow
{
    private readonly WindowsFormsWrapperWindow window;

    public Win32FormsAdapter(WindowsFormsWrapperWindow window)
    {
        this.window = window;
    }

    public string Title
    {
        get => window.Title;
        set => window.Title = value;
    }

    public void Close()
    {
        window.Close();
    }
}
