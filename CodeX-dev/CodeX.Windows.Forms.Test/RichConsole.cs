using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CodeX.Windows.Forms;

namespace WindowsFormsUIHelper_test
{
    public partial class RichConsole : RichForm
    {
        public RichConsole()
        {
            InitializeComponent();
        }

        private void richConsoleUserControl1_Execute(string input, RichOutputConsole console)
        {
            console.WriteLine(input, ThemeForeColor);
        }

        private void richConsoleUserControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
