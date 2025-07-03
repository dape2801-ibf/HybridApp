namespace LegacyWinForms.Forms;

partial class dlgConfirm
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
        labelControl1 = new DevExpress.XtraEditors.LabelControl();
        panel1 = new Panel();
        btnOK = new DevExpress.XtraEditors.SimpleButton();
        btnCancel = new DevExpress.XtraEditors.SimpleButton();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // labelControl1
        // 
        labelControl1.Appearance.Font = new Font("Tahoma", 18F);
        labelControl1.Appearance.Options.UseFont = true;
        labelControl1.Location = new Point(42, 136);
        labelControl1.Name = "labelControl1";
        labelControl1.Size = new Size(708, 36);
        labelControl1.TabIndex = 0;
        labelControl1.Text = "Etwas was der Anwender tun und bestätigen muss....";
        // 
        // panel1
        // 
        panel1.Controls.Add(btnOK);
        panel1.Controls.Add(btnCancel);
        panel1.Dock = DockStyle.Bottom;
        panel1.Location = new Point(0, 382);
        panel1.Name = "panel1";
        panel1.Size = new Size(800, 68);
        panel1.TabIndex = 1;
        // 
        // btnOK
        // 
        btnOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnOK.DialogResult = DialogResult.OK;
        btnOK.Location = new Point(546, 20);
        btnOK.Name = "btnOK";
        btnOK.Size = new Size(118, 36);
        btnOK.TabIndex = 1;
        btnOK.Text = "OK";
        // 
        // btnCancel
        // 
        btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(670, 20);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(118, 36);
        btnCancel.TabIndex = 0;
        btnCancel.Text = "Abbrechen";
        // 
        // dlgConfirm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(panel1);
        Controls.Add(labelControl1);
        Name = "dlgConfirm";
        Text = "dlgConfirm";
        panel1.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private DevExpress.XtraEditors.LabelControl labelControl1;
    private Panel panel1;
    private DevExpress.XtraEditors.SimpleButton btnOK;
    private DevExpress.XtraEditors.SimpleButton btnCancel;
}