using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeX.Windows.Forms
{
    using System.Runtime.InteropServices;

    public delegate void GotFocusDelegate();
    public partial class Intelly : Form
    {
        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern IntPtr GetDesktopWindow();
        private List<string> listSource;

        public new event GotFocusDelegate GotFocus;

        private Color _foreColor = Color.Black;

        private Color _backColor = Color.White;

        public string Selectedtext
        {
            get
            {
                if (lstSource.SelectedItem != null)
                {
                    var toReturn = lstSource.SelectedItem.ToString();

                    lstSource.SelectedItem = null;
                    return toReturn;
                }
                return string.Empty;
            }
        }
        public override Color ForeColor
        {
            get
            {
                return _foreColor;
            }
            set
            {
                _foreColor = value;

                txtInput.ForeColor = lstSource.ForeColor = _foreColor;
                tblLayout.BorderStyle = BorderStyle.FixedSingle;

            }
        }

        public override Color BackColor
        {
            get
            {
                return _backColor;
            }
            set
            {
                _backColor = value;

                txtInput.BackColor = lstSource.BackColor = _backColor;
            }
        }

        public override string Text
        {
            get
            {
                if (txtInput == null) return string.Empty;
                return txtInput.Text;
            }
            set
            {
                txtInput.Text = value;
            }
        }

        public Intelly()
        {
            InitializeComponent();

            txtInput.BorderStyle = lstSource.BorderStyle = BorderStyle.None;
        }

        public void SetDataSource(List<string> source)
        {
            listSource = source;
            lstSource.DataSource = listSource.Where(x => x.ToLower().Contains(txtInput.Text.ToLower())).ToList();
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            if (listSource == null) return;
            lstSource.DataSource = listSource.Where(x => x.ToLower().Contains(txtInput.Text.ToLower())).ToList();

        }

        private void txtInput_Enter(object sender, EventArgs e)
        {
            if (this.GotFocus != null)
            {
                this.GotFocus.Invoke();
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.Style = 0x40000000 | 0x4000000;
                cp.ExStyle &= 0x00080000;
                cp.Parent = GetDesktopWindow();
                return cp;
            }
        }
    }
}
