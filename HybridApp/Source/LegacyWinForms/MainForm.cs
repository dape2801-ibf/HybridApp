using LegacyWinForms.Data;
using LegacyWinForms.Forms;

namespace LegacyWinForms;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        DataModule.InitializeDataMode();
    }

    private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void adressverwaltungToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var tmpCursor = Cursor.Current;
        try
        {
            Cursor.Current = Cursors.WaitCursor;
            var form = new frmCustomers();
            form.MdiParent = this;
            form.Show();
        }
        finally
        {
            Cursor.Current = tmpCursor;
        }
    }

    private void auftragsverwaltungToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var tmpCursor = Cursor.Current;
        try
        {
            Cursor.Current = Cursors.WaitCursor;
            var form = new frmOrders();
            form.MdiParent = this;
            form.Show();
        }
        finally
        {
            Cursor.Current = tmpCursor;
        }
    }

    private void eineAktionToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var tmpCursor = Cursor.Current;
        try
        {
            Cursor.Current = Cursors.WaitCursor;
            var dialog = new dlgConfirm();
            var dialogResult = dialog.ShowDialog(this);
            MessageBox.Show($"Dialog result: {dialogResult}", "Dialog Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            Cursor.Current = tmpCursor;
        }
    }
}