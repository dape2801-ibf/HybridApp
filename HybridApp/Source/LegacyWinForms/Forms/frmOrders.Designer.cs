namespace LegacyWinForms.Forms;

partial class frmOrders
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        treeList1 = new DevExpress.XtraTreeList.TreeList();
        textEdit1 = new DevExpress.XtraEditors.TextEdit();
        layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
        textEdit3 = new DevExpress.XtraEditors.TextEdit();
        textEdit2 = new DevExpress.XtraEditors.TextEdit();
        dateEdit1 = new DevExpress.XtraEditors.DateEdit();
        Root = new DevExpress.XtraLayout.LayoutControlGroup();
        layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
        splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
        ((System.ComponentModel.ISupportInitialize)treeList1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)textEdit1.Properties).BeginInit();
        ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
        layoutControl1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)textEdit3.Properties).BeginInit();
        ((System.ComponentModel.ISupportInitialize)textEdit2.Properties).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dateEdit1.Properties).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dateEdit1.Properties.CalendarTimeProperties).BeginInit();
        ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
        ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
        ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
        ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
        ((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
        ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)splitterItem1).BeginInit();
        SuspendLayout();
        // 
        // treeList1
        // 
        treeList1.Location = new Point(13, 16);
        treeList1.Margin = new Padding(3, 4, 3, 4);
        treeList1.MinWidth = 23;
        treeList1.Name = "treeList1";
        treeList1.Size = new Size(436, 568);
        treeList1.TabIndex = 0;
        treeList1.TreeLevelWidth = 21;
        // 
        // textEdit1
        // 
        textEdit1.Location = new Point(569, 16);
        textEdit1.Margin = new Padding(3, 4, 3, 4);
        textEdit1.Name = "textEdit1";
        textEdit1.Size = new Size(332, 22);
        textEdit1.StyleController = layoutControl1;
        textEdit1.TabIndex = 1;
        // 
        // layoutControl1
        // 
        layoutControl1.Controls.Add(treeList1);
        layoutControl1.Controls.Add(textEdit3);
        layoutControl1.Controls.Add(textEdit1);
        layoutControl1.Controls.Add(textEdit2);
        layoutControl1.Controls.Add(dateEdit1);
        layoutControl1.Dock = DockStyle.Fill;
        layoutControl1.Location = new Point(0, 0);
        layoutControl1.Margin = new Padding(3, 4, 3, 4);
        layoutControl1.Name = "layoutControl1";
        layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(855, 189, 650, 400);
        layoutControl1.Root = Root;
        layoutControl1.Size = new Size(914, 600);
        layoutControl1.TabIndex = 9;
        layoutControl1.Text = "layoutControl1";
        // 
        // textEdit3
        // 
        textEdit3.Location = new Point(569, 100);
        textEdit3.Margin = new Padding(3, 4, 3, 4);
        textEdit3.Name = "textEdit3";
        textEdit3.Properties.ReadOnly = true;
        textEdit3.Size = new Size(332, 22);
        textEdit3.StyleController = layoutControl1;
        textEdit3.TabIndex = 4;
        // 
        // textEdit2
        // 
        textEdit2.Location = new Point(569, 72);
        textEdit2.Margin = new Padding(3, 4, 3, 4);
        textEdit2.Name = "textEdit2";
        textEdit2.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
        textEdit2.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
        textEdit2.Size = new Size(332, 22);
        textEdit2.StyleController = layoutControl1;
        textEdit2.TabIndex = 3;
        // 
        // dateEdit1
        // 
        dateEdit1.EditValue = null;
        dateEdit1.Location = new Point(569, 44);
        dateEdit1.Margin = new Padding(3, 4, 3, 4);
        dateEdit1.Name = "dateEdit1";
        dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
        dateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
        dateEdit1.Size = new Size(332, 22);
        dateEdit1.StyleController = layoutControl1;
        dateEdit1.TabIndex = 2;
        // 
        // Root
        // 
        Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
        Root.GroupBordersVisible = false;
        Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2, layoutControlItem3, layoutControlItem4, layoutControlItem5, layoutControlItem1, splitterItem1 });
        Root.Name = "Root";
        Root.Size = new Size(914, 600);
        Root.TextVisible = false;
        // 
        // layoutControlItem2
        // 
        layoutControlItem2.Control = textEdit1;
        layoutControlItem2.Location = new Point(452, 0);
        layoutControlItem2.Name = "layoutControlItem2";
        layoutControlItem2.Size = new Size(440, 28);
        layoutControlItem2.Text = "Bestellnummer:";
        layoutControlItem2.TextSize = new Size(90, 16);
        // 
        // layoutControlItem3
        // 
        layoutControlItem3.Control = dateEdit1;
        layoutControlItem3.Location = new Point(452, 28);
        layoutControlItem3.Name = "layoutControlItem3";
        layoutControlItem3.Size = new Size(440, 28);
        layoutControlItem3.Text = "Bestelldatum:";
        layoutControlItem3.TextSize = new Size(90, 16);
        // 
        // layoutControlItem4
        // 
        layoutControlItem4.Control = textEdit2;
        layoutControlItem4.Location = new Point(452, 56);
        layoutControlItem4.Name = "layoutControlItem4";
        layoutControlItem4.Size = new Size(440, 28);
        layoutControlItem4.Text = "Betrag:";
        layoutControlItem4.TextSize = new Size(90, 16);
        // 
        // layoutControlItem5
        // 
        layoutControlItem5.Control = textEdit3;
        layoutControlItem5.Location = new Point(452, 84);
        layoutControlItem5.Name = "layoutControlItem5";
        layoutControlItem5.Size = new Size(440, 490);
        layoutControlItem5.Text = "Status:";
        layoutControlItem5.TextSize = new Size(90, 16);
        // 
        // layoutControlItem1
        // 
        layoutControlItem1.Control = treeList1;
        layoutControlItem1.Location = new Point(0, 0);
        layoutControlItem1.Name = "layoutControlItem1";
        layoutControlItem1.Size = new Size(440, 574);
        layoutControlItem1.TextSize = new Size(0, 0);
        layoutControlItem1.TextVisible = false;
        // 
        // splitterItem1
        // 
        splitterItem1.AllowHotTrack = true;
        splitterItem1.Location = new Point(440, 0);
        splitterItem1.Name = "splitterItem1";
        splitterItem1.Size = new Size(12, 574);
        // 
        // frmOrders
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(914, 600);
        Controls.Add(layoutControl1);
        Margin = new Padding(3, 4, 3, 4);
        Name = "frmOrders";
        Text = "Auftragsverwaltung";
        ((System.ComponentModel.ISupportInitialize)treeList1).EndInit();
        ((System.ComponentModel.ISupportInitialize)textEdit1.Properties).EndInit();
        ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
        layoutControl1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)textEdit3.Properties).EndInit();
        ((System.ComponentModel.ISupportInitialize)textEdit2.Properties).EndInit();
        ((System.ComponentModel.ISupportInitialize)dateEdit1.Properties.CalendarTimeProperties).EndInit();
        ((System.ComponentModel.ISupportInitialize)dateEdit1.Properties).EndInit();
        ((System.ComponentModel.ISupportInitialize)Root).EndInit();
        ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
        ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
        ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
        ((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
        ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
        ((System.ComponentModel.ISupportInitialize)splitterItem1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private DevExpress.XtraTreeList.TreeList treeList1;
    private DevExpress.XtraEditors.TextEdit textEdit1;
    private DevExpress.XtraEditors.DateEdit dateEdit1;
    private DevExpress.XtraEditors.TextEdit textEdit2;
    private DevExpress.XtraEditors.TextEdit textEdit3;
    private DevExpress.XtraLayout.LayoutControl layoutControl1;
    private DevExpress.XtraLayout.LayoutControlGroup Root;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    private DevExpress.XtraLayout.SplitterItem splitterItem1;
}