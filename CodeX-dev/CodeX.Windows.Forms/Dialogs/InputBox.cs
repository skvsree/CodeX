using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeX.Windows.Forms.Dialogs
{
    public partial class InputBox : RichForm
    {
        public string InputString { get; private set; }
        private DialogResult result = DialogResult.Cancel;
        public InputBox()
        {
            InitializeComponent();
            Title.Text = "Input";
            MaximizeBox = false;
            txtInput.Focus();
            AcceptButton = btnOk;
        }

        protected override void OnLoad(EventArgs e)
        {
            AutoApplytThemeToControls = true;
            ApplyThemeFromConfiguration();
            result = DialogResult.Cancel;
            base.OnLoad(e);
            InputString = string.Empty;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            result = DialogResult.OK;
            InputString = txtInput.Text;
            Close();
        }

        private void InputBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = result;
        }

    }
}
