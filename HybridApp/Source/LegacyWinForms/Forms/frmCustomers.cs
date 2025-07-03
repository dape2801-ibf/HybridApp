using LegacyWinForms.Data;

namespace LegacyWinForms.Forms;
public partial class frmCustomers : Form
{
    public frmCustomers()
    {
        InitializeComponent();
        gridControl1.DataSource = LwfDataContext.Customers;
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
    }
}
