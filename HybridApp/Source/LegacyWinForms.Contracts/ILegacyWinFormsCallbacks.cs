namespace LegacyWinForms.Contracts;
/// <summary>
/// Callbacks to interact from the hybrid application with the WinForms application.
/// </summary>
public interface ILegacyWinFormsCallbacks
{
    /// <summary>
    /// Opens the order management form.
    /// </summary>
    /// <returns></returns>
    Task OpenOrderManagement();


    /// <summary>
    /// Opens the customer management form.
    /// </summary>
    /// <returns></returns>
    Task OpenCustomerManagement();

    /// <summary>
    /// Opens a dialog for actions and returns if it is successful or not
    /// </summary>
    /// <returns></returns>
    Task<bool> OpenActionDialog();

    /// <summary>
    /// Opens the main WinForms application window. (MDI)
    /// </summary>
    /// <returns></returns>
    Task OpenMainApplication();
}
