using CommonLib.FormsIntegration;
using LegacyWinForms.Contracts;
using LegacyWinForms.Forms;

namespace LegacyWinForms.Connector;
public class LegacyWinFormsCallbacks : ILegacyWinFormsCallbacks
{
    public Task OpenOrderManagement()
    {
        var orderManagementForm = new frmOrders();
        orderManagementForm.ShowWinFormsAsWpfWindow();
        return Task.CompletedTask;
    }

    public Task OpenCustomerManagement()
    {
        var customerManagementForm = new frmCustomers();
        customerManagementForm.ShowWinFormsAsWpfWindow();
        return Task.CompletedTask;
    }

    public Task<bool> OpenActionDialog()
    {
        var dialog = new dlgConfirm();
        var dialogResult = dialog.ShowWinFormsAsWpfDialog();
        return Task.FromResult(dialogResult == DialogResult.OK);
    }

    public Task OpenMainApplication()
    {
        var mainForm = new MainForm();
        mainForm.ShowWinFormsAsWpfWindow();
        return Task.CompletedTask;
    }
}
