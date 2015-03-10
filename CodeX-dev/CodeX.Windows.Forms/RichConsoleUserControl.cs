using System;
using System.Drawing;
using System.Windows.Forms;
using CodeX;

namespace CodeX.Windows.Forms
{
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;

    using CodeX.IO;
    using CodeX.Strings;

    public delegate void ExecuteDelegate(string input, RichOutputConsole console);
    public delegate void KeyPressDelegate(KeyEventArgs key, TextBox inputArea);
    public partial class RichConsoleUserControl : UserControl
    {
        [DllImport("user32.dll")]
        static extern bool GetCaretPos(out Point lpPoint);

        public event ExecuteDelegate Execute;
        public event KeyPressDelegate InputKeyPress;


        private Intelly intelly;

        private int curPosStart;

        private int curPosStop;
        public override Color ForeColor
        {
            set
            {
                base.ForeColor = value;
                this.SetFormat(this.ForeColor, this.BackColor);
            }
        }

        public override Color BackColor
        {
            set
            {
                base.BackColor = value;
                this.SetFormat(this.ForeColor, this.BackColor);
            }
        }

        public Font InputFont
        {
            get
            {
                return this.roConsole.Font;
            }
            set
            {
                this.roConsole.Font = value;
            }
        }

        public Font OutputFont
        {
            get
            {
                return this.txtInput.Font;
            }
            set
            {
                this.txtInput.Font = value;
            }
        }


        public RichConsoleUserControl()
        {
            this.InitializeComponent();

            this.intelly = new Intelly { Name = "intelly", Visible = false };
            this.intelly.KeyDown += (sender, args) =>
                {
                    switch (args.KeyCode)
                    {
                        case Keys.Escape:
                            this.intelly.Visible = false;
                            break;
                    }
                };

            this.intelly.GotFocus += () => this.txtInput.Focus();

            this.intelly.TextChanged += (sender, args) =>
                {

                };
            //intelly.
            //intelly.SelectedIndexChanged += (sender, args) =>
            //    { intelly.TopIndex = intelly.SelectedIndex; };
            //Controls.Add(this.intelly);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.SetFormat(this.ForeColor, this.BackColor);

        }


        private void SetFormat(Color forecolor, Color backcolor)
        {
            this.lblInput.ForeColor = forecolor;
            this.lblOutput.ForeColor = forecolor;
            this.roConsole.ForeColor = forecolor;
            this.txtInput.ForeColor = forecolor;
            this.lblInput.BackColor = backcolor;
            this.lblOutput.BackColor = backcolor;
            this.roConsole.BackColor = backcolor;
            this.txtInput.BackColor = backcolor;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.DrawRectangle(new Pen(ForeColor), new Rectangle(0, 0, Width - 1, Height - 10));
            // e.Graphics.DrawRectangle(new Pen(new SolidBrush(ForeColor), 2), e.ClipRectangle);
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            this.InvokeExecute();
        }

        private void InvokeExecute()
        {
            if (this.Execute == null) return;
            this.Execute.Invoke(this.txtInput.Text, this.roConsole);
            this.txtInput.Text = String.Empty;
        }

        private void RichConsoleUserControl_Load(object sender, EventArgs e)
        {

        }

        private void RichConsoleUserControl_Resize(object sender, EventArgs e)
        {
            //panel1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
            //lblInput.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            //lblOutput.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            // e.Graphics.DrawRectangle(new Pen(new SolidBrush(ForeColor), 2), e.ClipRectangle);
        }

        private void txtInput_Enter(object sender, EventArgs e)
        {

        }


        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void TxtInputKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    this.InvokeExecute();
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Escape:
                    intelly.Visible = false;
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Enter:
                    if (e.Control)
                    {
                        txtInput.Text += this.intelly.Selectedtext;
                        e.SuppressKeyPress = true;
                    }
                    break;
                case Keys.Space:
                    if (e.Control)
                    {
                        var point = Point.Empty;
                        GetCaretPos(out point);
                        this.ApplyThemeOnListBox(this.intelly);
                        this.intelly.SetDataSource(Directory.GetCurrentDirectory().AsFolder().GetFiles().Select(x => x.FullName).ToList());
                        var point2 = this.PointToClient(this.PointToScreen(point));
                        this.intelly.StartPosition = FormStartPosition.Manual;
                        // this.intelly.Location = new Point(point2.X + 2, point2.Y + this.Height - txtInput.Height + this.intelly.Font.Height);
                        var pt = this.GetPoint(txtInput);
                        //this.intelly.Location = new Point(pt.X + 2, pt.Y + this.txtInput.Font.Height);
                        Point point3 = this.txtInput.GetPositionFromCharIndex(txtInput.SelectionStart);
                        point3.Y += (int)Math.Ceiling(this.txtInput.Font.GetHeight()) + 2;
                        point3.X += 2;
                        this.intelly.Location = point3;
                        AutoSizeTextBox(this.intelly);
                        if (!intelly.Visible)
                            this.intelly.Show(this.Parent);
                        this.intelly.BringToFront();
                        // this.Controls.SetChildIndex(intelly, 0);
                        e.SuppressKeyPress = true;
                    }
                    break;
            }
            //txtInput.Select(curPosStart + this.intelly.Text.Length, this.intelly.Text.Length);
        }

        private void ApplyThemeOnListBox(Intelly listBox)
        {
            if (listBox != null)
            {
                listBox.ForeColor = txtInput.ForeColor;
                listBox.BackColor = txtInput.BackColor;

                listBox.Size = new Size(150, listBox.Height);

                // intelly.DropDownStyle = ComboBoxStyle.DropDown;
                //intelly.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private Point GetPoint(RichTextBox textBox)
        {
            using (Graphics g = Graphics.FromHwnd(textBox.Handle))
            {
                SizeF size = g.MeasureString(textBox.Text.Substring(0, textBox.SelectionStart), textBox.Font);

                Point pt = textBox.PointToScreen(new Point((int)size.Width, (int)0));
                return pt;
            }
        }


        private void txtInput_Leave(object sender, EventArgs e)
        {
            curPosStart = txtInput.SelectionStart + 1;
            curPosStop = txtInput.SelectionStart + txtInput.SelectionLength - 1;
        }

        private static void AutoSizeTextBox(Intelly textBox)
        {
            //var maxstring = string.Empty;
            //foreach (string item in textBox.AutoCompleteCustomSource)
            //{
            //    if (item.Length > maxstring.Length)
            //    {
            //        maxstring = item;
            //    }
            //}
            //textBox.Width = TextRenderer.MeasureText(maxstring, textBox.Font).Width + 10;
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
           // this.intelly.Text = txtInput.GetCurrentWord();
            var pt = this.GetPoint(txtInput);
            this.intelly.Location = new Point(pt.X + 2, pt.Y + this.txtInput.Font.Height);
            // intelly.DroppedDown = true;
            //txtInput.Focus();
            //txtInput.Select(curPosStart + this.intelly.Text.Length, this.intelly.Text.Length);
        }
    }
}
