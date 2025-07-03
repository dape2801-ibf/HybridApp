using System.Windows.Forms;
using CommonLib.Contracts;
using CommonLib.DependencyInjection;

namespace CommonLib.FormsIntegration;
public static class HybridAppDialogServiceExtensions
{
    /// <summary>
    /// Shows a WinForms Form as a WPF Window using the HybridAppDialogService.
    /// </summary>
    /// <param name="form">The WinForms form that should be displayed</param>
    /// <param name="owner">Optional: An owner of the form</param>
    /// <param name="disposeOnClose">Determines if you dispose the WinForms form by yourself</param>
    public static void ShowWinFormsAsWpfWindow(
        this Form form,
        IWin32Window owner = null,
        bool disposeOnClose = true)
    {
        if (form.MdiParent != null)
        {
            form.Show();
            return;
        }

        DIServiceLocator
            .Instance
            .Resolve<IHybridAppDialogService>()
            .ShowWindow(form, owner);
    }
    /// <summary>
    /// Shows a WinForms Dialog as a WPF Window using the HybridAppDialogService.
    /// </summary>
    /// <param name="form">The WinForms form that should be displayed</param>
    /// <param name="owner">Optional: An owner of the form</param>
    /// <param name="disposeOnClose">Determines if you dispose the WinForms form by yourself</param>
    /// <returns>A DialogResult</returns>
    public static DialogResult ShowWinFormsAsWpfDialog(
        this Form form,
        IWin32Window owner = null,
        bool disposeOnClose = true)
    {
        return DIServiceLocator
            .Instance
            .Resolve<IHybridAppDialogService>()
            .ShowWindowDialog(form, owner);
    }
}
