using CodeX.Windows.Forms;

namespace WindowsFormsUIHelper_test
{
    partial class RichConsole
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
            this.richConsoleUserControl1 = new RichConsoleUserControl();
            this.SuspendLayout();
            // 
            // richConsoleUserControl1
            // 
            this.richConsoleUserControl1.Location = new System.Drawing.Point(12, 65);
            this.richConsoleUserControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.richConsoleUserControl1.Name = "richConsoleUserControl1";
            this.richConsoleUserControl1.Size = new System.Drawing.Size(651, 530);
            this.richConsoleUserControl1.TabIndex = 5;
            this.richConsoleUserControl1.Execute += new ExecuteDelegate(this.richConsoleUserControl1_Execute);
            this.richConsoleUserControl1.Load += new System.EventHandler(this.richConsoleUserControl1_Load);
            // 
            // RichConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 608);
            this.Controls.Add(this.richConsoleUserControl1);
            this.Name = "RichConsole";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RichConsole";
            this.Controls.SetChildIndex(this.richConsoleUserControl1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RichConsoleUserControl richConsoleUserControl1;



    }
}