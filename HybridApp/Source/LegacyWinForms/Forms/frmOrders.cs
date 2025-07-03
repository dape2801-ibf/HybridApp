using LegacyWinForms.Data;

namespace LegacyWinForms.Forms;
public partial class frmOrders : Form
{
    private BindingSource bindingSource1 = new BindingSource();
    public frmOrders()
    {
        InitializeComponent();
        bindingSource1.DataSource = LwfDataContext.Orders;

        textEdit1.DataBindings.Add("Text", bindingSource1, "Bestellnummer", true, DataSourceUpdateMode.OnPropertyChanged);
        textEdit2.DataBindings.Add("Text", bindingSource1, "Betrag", true, DataSourceUpdateMode.OnPropertyChanged);
        textEdit3.DataBindings.Add("Text", bindingSource1, "Status", true, DataSourceUpdateMode.OnPropertyChanged);
        dateEdit1.DataBindings.Add("EditValue", bindingSource1, "Bestelldatum", true, DataSourceUpdateMode.OnPropertyChanged);

        treeList1.DataSource = bindingSource1;
        treeList1.KeyFieldName = "Id";
        treeList1.ParentFieldName = "CustomerId";
    }
}
