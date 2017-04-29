namespace ChessBasedGame
{
    partial class Form1
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
            this.Board_Panel = new System.Windows.Forms.Panel();
            this.UnitInfo_Panel = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.vScrollBar2 = new System.Windows.Forms.VScrollBar();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Board_Panel
            // 
            this.Board_Panel.Location = new System.Drawing.Point(12, 12);
            this.Board_Panel.Name = "Board_Panel";
            this.Board_Panel.Size = new System.Drawing.Size(432, 367);
            this.Board_Panel.TabIndex = 0;
            // 
            // UnitInfo_Panel
            // 
            this.UnitInfo_Panel.Location = new System.Drawing.Point(850, 12);
            this.UnitInfo_Panel.Name = "UnitInfo_Panel";
            this.UnitInfo_Panel.Size = new System.Drawing.Size(130, 178);
            this.UnitInfo_Panel.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Controls.Add(this.vScrollBar2);
            this.panel3.Location = new System.Drawing.Point(850, 196);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(130, 558);
            this.panel3.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(107, 44);
            this.textBox1.TabIndex = 1;
            // 
            // vScrollBar2
            // 
            this.vScrollBar2.Location = new System.Drawing.Point(113, 3);
            this.vScrollBar2.Name = "vScrollBar2";
            this.vScrollBar2.Size = new System.Drawing.Size(17, 44);
            this.vScrollBar2.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 766);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.UnitInfo_Panel);
            this.Controls.Add(this.Board_Panel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel Board_Panel;
        private System.Windows.Forms.Panel UnitInfo_Panel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.VScrollBar vScrollBar2;
    }
}

