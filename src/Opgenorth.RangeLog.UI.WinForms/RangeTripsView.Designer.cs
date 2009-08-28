namespace Opgenorth.RangeLog.WinForms
{
    partial class RangeTripsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RangeTripsView));
            this._toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._backFromNewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._databaseLocationStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._panel1 = new System.Windows.Forms.Panel();
            this._editToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._toolStrip1.SuspendLayout();
            this._statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolStrip1
            // 
            this._toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._backFromNewToolStripButton,
            this._newToolStripButton,
            this._deleteToolStripButton,
            this._editToolStripButton,
            this._saveToolStripButton});
            this._toolStrip1.Location = new System.Drawing.Point(0, 0);
            this._toolStrip1.Name = "_toolStrip1";
            this._toolStrip1.Size = new System.Drawing.Size(681, 25);
            this._toolStrip1.TabIndex = 0;
            this._toolStrip1.Text = "_toolStrip1";
            // 
            // _backFromNewToolStripButton
            // 
            this._backFromNewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._backFromNewToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_backFromNewToolStripButton.Image")));
            this._backFromNewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._backFromNewToolStripButton.Name = "_backFromNewToolStripButton";
            this._backFromNewToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._backFromNewToolStripButton.Text = "Cancel";
            this._backFromNewToolStripButton.Visible = false;
            this._backFromNewToolStripButton.Click += new System.EventHandler(this._backFromNewToolStripButton_Click);
            // 
            // _newToolStripButton
            // 
            this._newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_newToolStripButton.Image")));
            this._newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._newToolStripButton.Name = "_newToolStripButton";
            this._newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._newToolStripButton.Text = "&New";
            this._newToolStripButton.Click += new System.EventHandler(this._newToolStripButton_Click);
            // 
            // _deleteToolStripButton
            // 
            this._deleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._deleteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_deleteToolStripButton.Image")));
            this._deleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._deleteToolStripButton.Name = "_deleteToolStripButton";
            this._deleteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._deleteToolStripButton.Text = "toolStripButton1";
            this._deleteToolStripButton.ToolTipText = "Delete";
            this._deleteToolStripButton.Click += new System.EventHandler(this._deleteRow_Click);
            // 
            // _saveToolStripButton
            // 
            this._saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_saveToolStripButton.Image")));
            this._saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveToolStripButton.Name = "_saveToolStripButton";
            this._saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._saveToolStripButton.Text = "&Save";
            this._saveToolStripButton.Visible = false;
            this._saveToolStripButton.Click += new System.EventHandler(this._saveToolStripButton_Click);
            // 
            // _statusStrip1
            // 
            this._statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._databaseLocationStatusLabel});
            this._statusStrip1.Location = new System.Drawing.Point(0, 337);
            this._statusStrip1.Name = "_statusStrip1";
            this._statusStrip1.Size = new System.Drawing.Size(681, 22);
            this._statusStrip1.TabIndex = 1;
            this._statusStrip1.Text = "_statusStrip1";
            // 
            // _databaseLocationStatusLabel
            // 
            this._databaseLocationStatusLabel.Name = "_databaseLocationStatusLabel";
            this._databaseLocationStatusLabel.Size = new System.Drawing.Size(55, 17);
            this._databaseLocationStatusLabel.Text = "Database";
            // 
            // _panel1
            // 
            this._panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panel1.Location = new System.Drawing.Point(0, 25);
            this._panel1.Name = "_panel1";
            this._panel1.Size = new System.Drawing.Size(681, 312);
            this._panel1.TabIndex = 2;
            // 
            // _editToolStripButton
            // 
            this._editToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._editToolStripButton.Enabled = false;
            this._editToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_editToolStripButton.Image")));
            this._editToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._editToolStripButton.Name = "_editToolStripButton";
            this._editToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._editToolStripButton.Text = "Edit";
            this._editToolStripButton.ToolTipText = "Edit";
            // 
            // RangeTripsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 359);
            this.Controls.Add(this._panel1);
            this.Controls.Add(this._statusStrip1);
            this.Controls.Add(this._toolStrip1);
            this.Name = "RangeTripsView";
            this.Text = "My Trips To The Range";
            this.Load += new System.EventHandler(this.Form1_Load);
            this._toolStrip1.ResumeLayout(false);
            this._toolStrip1.PerformLayout();
            this._statusStrip1.ResumeLayout(false);
            this._statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _toolStrip1;
        private System.Windows.Forms.ToolStripButton _newToolStripButton;
        private System.Windows.Forms.ToolStripButton _deleteToolStripButton;
        private System.Windows.Forms.StatusStrip _statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _databaseLocationStatusLabel;
        private System.Windows.Forms.Panel _panel1;
        private System.Windows.Forms.ToolStripButton _saveToolStripButton;
        private System.Windows.Forms.ToolStripButton _backFromNewToolStripButton;
        private System.Windows.Forms.ToolStripButton _editToolStripButton;
    }
}