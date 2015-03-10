using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using CodeX;
using CodeX.Core;
using System.Windows.Forms;
using CodeX.Windows.Forms;
using System.Threading;
using CodeX.Windows.Forms.Dialogs;

namespace WindowsFormsUIHelper_test
{
    public partial class Form1 : RichForm
    {
        private bool IsFormLoading;
        public Form1()
        {
            InitializeComponent();
            //ThemeForeColor = ThemeColor.Blue;
            //ThemeBackColor = ThemeColor.DarkGray;

            //Title.Text = "Main Form";
            ApplyThemeFromConfiguration();
        }

        private void btnProperties_Click(object sender, EventArgs e)
        {
            var testForm = new TestForm
                {
                    ThemeForeColor = StringToColor(cmbForeColor.SelectedItem.ToString()),
                    ThemeBackColor = StringToColor(cmbBackColor.SelectedItem.ToString()),
                    ThemeBorderColor = StringToColor(cmbBorderColor.SelectedItem.ToString()),
                    Animation = (FormAnimation)Enum.Parse(typeof(FormAnimation), cmbAnimation.SelectedItem.ToString()),
                    AnimationSpeed = (int)nudAnimationSpeed.Value,
                    Title = { Text = txtTitle.Text }
                };
            testForm.Show();
        }


        private Color StringToColor(string color)
        {
            return ThemeColor.GetColor(color);
        }

        private void cmbAnimation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkApplyForCurrentForm.Checked)
            {
                Animation = (FormAnimation)Enum.Parse(typeof(FormAnimation), cmbAnimation.SelectedItem.ToString());
            }
            //Thread.Sleep(5000);
            SetStatusBarMessage(cmbAnimation.SelectedItem.ToString());
            NotifyToBalloon("Animation changed");
            IconImage = Image.FromFile(@"D:\Community\ChocolateyAppStore\ChocolateyAppStore\Choco_ico_new.ico");

        }

        private void cmbForeColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkApplyForCurrentForm.Checked)
            {
                ThemeForeColor = StringToColor(cmbForeColor.SelectedItem.ToString());
                FormatControls();
            }
        }

        private void cmbBackColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkApplyForCurrentForm.Checked)
            {
                ThemeBackColor = StringToColor(cmbBackColor.SelectedItem.ToString());
            }
        }

        private void nudAnimationSpeed_ValueChanged(object sender, EventArgs e)
        {
            if (chkApplyForCurrentForm.Checked)
            {
                AnimationSpeed = (int)nudAnimationSpeed.Value;
            }
        }

        private void chkApplyForCurrentForm_CheckedChanged(object sender, EventArgs e)
        {
            if (chkApplyForCurrentForm.Checked)
            {
                ThemeForeColor = StringToColor(cmbForeColor.SelectedItem.ToString());
                ThemeBackColor = StringToColor(cmbBackColor.SelectedItem.ToString());
                Animation = (FormAnimation)Enum.Parse(typeof(FormAnimation), cmbAnimation.SelectedItem.ToString());
                AnimationSpeed = (int)nudAnimationSpeed.Value;
                Title.Text = txtTitle.Text;
                ApplyThemeToControls();
            }
        }

        private void FormatControls()
        {
            foreach (var control in Controls)
            {
                var labelControl = control as Label;
                if (labelControl != null)
                {
                    labelControl.ForeColor = ThemeForeColor;
                }
                var checkBoxControl = control as CheckBox;
                if (checkBoxControl != null)
                {
                    checkBoxControl.ForeColor = ThemeForeColor;
                }
            }
        }

        private void btnShowConsole_Click(object sender, EventArgs e)
        {
            var console = new RichConsole
                {
                    ForeColor = ThemeForeColor,
                    ThemeBackColor = ThemeBackColor,
                    ThemeForeColor = ThemeForeColor,
                    BackColor = ThemeBackColor,
                    Animation = Animation,
                    AnimationSpeed = AnimationSpeed,
                    Title = { Text = "Rich Console" }
                };

            console.ShowDialog();
        }

        private void cmbThemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsFormLoading) return;
            var selectedTheme = cmbThemes.SelectedItem as ThemeConfiguration;
            if (selectedTheme == null) return;
            ApplyThemeFromConfiguration(selectedTheme);

            txtTitle.Text = this.Text;
            cmbAnimation.SelectedItem = selectedTheme.Window.Animation.AsString();
            cmbBackColor.SelectedItem = selectedTheme.Window.BackColorString;
            cmbForeColor.SelectedItem = selectedTheme.Window.ForeColorString;
            cmbBorderColor.SelectedItem = selectedTheme.Window.BorderColorString;
            nudAnimationSpeed.Value = selectedTheme.Window.AnimationSpeed;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            IsFormLoading = true;
            cmbThemes.DataSource = ThemeList;
            cmbThemes.DisplayMember = "Name";
            cmbThemes.SelectedItem = ThemeList.FirstOrDefault(x => x.Selected);
            IsFormLoading = false;
            var selectedTheme = cmbThemes.SelectedItem as ThemeConfiguration;
            ApplyThemeFromConfiguration(selectedTheme);
            cmbAnimation.DataSource = new BindingList<string>(Enum.GetNames(typeof(FormAnimation)).ToList());
            cmbBackColor.DataSource = new BindingList<string>(ThemeColor.GetColorList());
            cmbForeColor.DataSource = new BindingList<string>(ThemeColor.GetColorList());
            cmbBorderColor.DataSource = new BindingList<string>(ThemeColor.GetColorList());

            chkElevatedProcess.Checked = this.IsProcessElevated();
            chkUACEnabled.Checked = this.IsUACEnabled();
            nudAnimationSpeed.Value = 100;
            AutoApplytThemeToControls = true;
            ApplyThemeToControls();

            txtTitle.Text = this.Text;
            cmbAnimation.SelectedItem = selectedTheme.Window.Animation.AsString();
            cmbBackColor.SelectedItem = selectedTheme.Window.BackColorString;
            cmbForeColor.SelectedItem = selectedTheme.Window.ForeColorString;
            cmbBorderColor.SelectedItem = selectedTheme.Window.BorderColorString;
            nudAnimationSpeed.Value = selectedTheme.Window.AnimationSpeed;

            WindowState = FormWindowState.Maximized;
        }

        private void cmbBorderColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkApplyForCurrentForm.Checked)
            {
                ThemeBorderColor = StringToColor(cmbBorderColor.SelectedItem.ToString());
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            contextMenuStrip1.Items.Add("Hello");
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show();
            }
        }

        private void chkShowStatusBar_CheckedChanged(object sender, EventArgs e)
        {
            ShowStatusBar = chkShowStatusBar.Checked;
        }

        private void chkShowStatusCountInICon_CheckedChanged(object sender, EventArgs e)
        {
            ShowStatusCountInIcon = chkShowStatusCountInICon.Checked;
        }

        private void btnShowDialog_Click(object sender, EventArgs e)
        {
            var input = new InputBox();
            input.ShowDialog();
            input.InputString.ShowInformation();

        }
    }
}
