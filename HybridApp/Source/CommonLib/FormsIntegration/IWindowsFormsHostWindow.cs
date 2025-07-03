namespace CommonLib.FormsIntegration;
public interface IWindowsFormsHostWindow
{
    /// <summary>
    /// The title of the window
    /// </summary>
    string Title { get; set; }
    /// <summary>
    /// Closes the window and disposes the hosted WinForms control if necessary.
    /// </summary>
    void Close();
}
