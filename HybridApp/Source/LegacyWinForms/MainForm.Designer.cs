
namespace LegacyWinForms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            menuStrip1 = new MenuStrip();
            dateiToolStripMenuItem = new ToolStripMenuItem();
            beendenToolStripMenuItem = new ToolStripMenuItem();
            kundenToolStripMenuItem = new ToolStripMenuItem();
            adressverwaltungToolStripMenuItem = new ToolStripMenuItem();
            auftragsverwaltungToolStripMenuItem = new ToolStripMenuItem();
            xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(components);
            eineAktionToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)xtraTabbedMdiManager1).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { dateiToolStripMenuItem, kundenToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 3, 0, 3);
            menuStrip1.Size = new Size(914, 30);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            dateiToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { beendenToolStripMenuItem });
            dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            dateiToolStripMenuItem.Size = new Size(59, 24);
            dateiToolStripMenuItem.Text = "Datei";
            // 
            // beendenToolStripMenuItem
            // 
            beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            beendenToolStripMenuItem.Size = new Size(150, 26);
            beendenToolStripMenuItem.Text = "Beenden";
            beendenToolStripMenuItem.Click += beendenToolStripMenuItem_Click;
            // 
            // kundenToolStripMenuItem
            // 
            kundenToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { adressverwaltungToolStripMenuItem, auftragsverwaltungToolStripMenuItem, eineAktionToolStripMenuItem });
            kundenToolStripMenuItem.Name = "kundenToolStripMenuItem";
            kundenToolStripMenuItem.Size = new Size(73, 24);
            kundenToolStripMenuItem.Text = "Kunden";
            // 
            // adressverwaltungToolStripMenuItem
            // 
            adressverwaltungToolStripMenuItem.Name = "adressverwaltungToolStripMenuItem";
            adressverwaltungToolStripMenuItem.Size = new Size(224, 26);
            adressverwaltungToolStripMenuItem.Text = "Adressverwaltung";
            adressverwaltungToolStripMenuItem.Click += adressverwaltungToolStripMenuItem_Click;
            // 
            // auftragsverwaltungToolStripMenuItem
            // 
            auftragsverwaltungToolStripMenuItem.Name = "auftragsverwaltungToolStripMenuItem";
            auftragsverwaltungToolStripMenuItem.Size = new Size(224, 26);
            auftragsverwaltungToolStripMenuItem.Text = "Auftragsverwaltung";
            auftragsverwaltungToolStripMenuItem.Click += auftragsverwaltungToolStripMenuItem_Click;
            // 
            // xtraTabbedMdiManager1
            // 
            xtraTabbedMdiManager1.MdiParent = this;
            // 
            // eineAktionToolStripMenuItem
            // 
            eineAktionToolStripMenuItem.Name = "eineAktionToolStripMenuItem";
            eineAktionToolStripMenuItem.Size = new Size(224, 26);
            eineAktionToolStripMenuItem.Text = "Eine Aktion";
            eineAktionToolStripMenuItem.Click += eineAktionToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            Text = "LegacyWinFormsApp";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)xtraTabbedMdiManager1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem dateiToolStripMenuItem;
        private ToolStripMenuItem beendenToolStripMenuItem;
        private ToolStripMenuItem kundenToolStripMenuItem;
        private ToolStripMenuItem adressverwaltungToolStripMenuItem;
        private ToolStripMenuItem auftragsverwaltungToolStripMenuItem;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private ToolStripMenuItem eineAktionToolStripMenuItem;
    }
}
