namespace Sons_of_San_Jose
{
    partial class ExitForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExitForm));
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.panel = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.btnno = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.btnyes = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.lbltxt = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.shapeContainer2 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.panel});
            this.shapeContainer1.Size = new System.Drawing.Size(391, 122);
            this.shapeContainer1.TabIndex = 0;
            this.shapeContainer1.TabStop = false;
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.BackColor = System.Drawing.Color.Honeydew;
            this.panel.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.panel.Location = new System.Drawing.Point(5, 5);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(380, 109);
            this.panel.Click += new System.EventHandler(this.panel_Click);
            // 
            // btnno
            // 
            this.btnno.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnno.BackgroundImage")));
            this.btnno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnno.BorderColor = System.Drawing.Color.SeaGreen;
            this.btnno.BorderWidth = 5;
            this.btnno.CornerRadius = 13;
            this.btnno.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnno.FillColor = System.Drawing.Color.Green;
            this.btnno.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.btnno.Location = new System.Drawing.Point(219, 7);
            this.btnno.Name = "btnno";
            this.btnno.Size = new System.Drawing.Size(74, 27);
            this.btnno.Click += new System.EventHandler(this.btnno_Click);
            // 
            // btnyes
            // 
            this.btnyes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnyes.BackgroundImage")));
            this.btnyes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnyes.BorderColor = System.Drawing.Color.SeaGreen;
            this.btnyes.BorderWidth = 5;
            this.btnyes.CornerRadius = 13;
            this.btnyes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnyes.FillColor = System.Drawing.Color.Green;
            this.btnyes.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.btnyes.Location = new System.Drawing.Point(84, 7);
            this.btnyes.Name = "btnyes";
            this.btnyes.Size = new System.Drawing.Size(74, 27);
            this.btnyes.Click += new System.EventHandler(this.btnyes_Click);
            // 
            // lbltxt
            // 
            this.lbltxt.AutoSize = true;
            this.lbltxt.BackColor = System.Drawing.Color.Honeydew;
            this.lbltxt.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltxt.ForeColor = System.Drawing.Color.SeaGreen;
            this.lbltxt.Location = new System.Drawing.Point(90, 25);
            this.lbltxt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbltxt.Name = "lbltxt";
            this.lbltxt.Size = new System.Drawing.Size(222, 17);
            this.lbltxt.TabIndex = 3;
            this.lbltxt.Text = "Do you want to close this form?.";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Honeydew;
            this.panel1.Controls.Add(this.shapeContainer2);
            this.panel1.Location = new System.Drawing.Point(9, 51);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(373, 42);
            this.panel1.TabIndex = 4;
            // 
            // shapeContainer2
            // 
            this.shapeContainer2.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer2.Name = "shapeContainer2";
            this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.btnno,
            this.btnyes});
            this.shapeContainer2.Size = new System.Drawing.Size(373, 42);
            this.shapeContainer2.TabIndex = 0;
            this.shapeContainer2.TabStop = false;
            // 
            // ExitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Green;
            this.ClientSize = new System.Drawing.Size(391, 122);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbltxt);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ExitForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExitForm";
            this.Load += new System.EventHandler(this.ExitForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape panel;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape btnyes;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape btnno;
        public System.Windows.Forms.Label lbltxt;
        private System.Windows.Forms.Panel panel1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer2;
    }
}