using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CodeX.Strings;

// ReSharper disable CheckNamespace
namespace CodeX.Windows.Forms
// ReSharper restore CheckNamespace
{
    using System.Globalization;

    public partial class RichForm : Form
    {

        [DllImport("user32.dll")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        [DllImport("uxtheme", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public extern static Int32 SetWindowTheme(IntPtr hWnd, String textSubAppName, String textSubIdList);

        //Button colors are inverted to make them prominent
        private Color _themeForeColor;
        private Color _themeBackColor;
        private Color _themeBorderColor = ThemeColor.Black;
        private bool _setStatusCountInIcon;
        private Dictionary<string, bool> _statuses = new Dictionary<string, bool>();

        private int cmbSelectedIndex = -1;
        private NotifyIcon _notifyIcon;

        public bool AutoApplytThemeToControls { get; set; }

        public List<ThemeConfiguration> ThemeList { get; set; }

        public bool SaveConfigurationOnClose { get; set; }

        public Image IconImage
        {
            set
            {
                btnImage.BackgroundImage = value;
                this.SetIcon(lblStatusCount.Text);
                if (btnImage.BackgroundImage != null)
                {
                    Icon = Icon.FromHandle(new Bitmap(btnImage.BackgroundImage).GetHicon());
                    _notifyIcon.Icon = Icon;
                }
            }
            get
            {
                return btnImage.BackgroundImage;
            }
        }

        public bool ShowMaximizeButton
        {
            get
            {
                return btnMaximize.Visible;
            }
            set
            {
                btnMaximize.Visible = value;
            }
        }

        public bool ShowStatusCountInIcon
        {
            get
            {
                return _setStatusCountInIcon;
            }

            set
            {
                _setStatusCountInIcon = value;
                SetIcon(lblStatusCount.Text);
            }
        }

        public bool AllowMaximize { get; set; }

        public bool ShowStatusBar
        {
            get
            {
                return flpBottom.Visible;
            }

            set
            {
                flpBottom.Visible = value;
            }
        }

        public Color ThemeForeColor
        {
            get
            {
                return _themeForeColor;
            }
            set
            {
                _themeForeColor = value;
                btnClose.BackColor = value;
                btnClose.FlatAppearance.MouseOverBackColor = ControlPaint.Light(value);
                btnMinimize.BackColor = value;
                btnMinimize.FlatAppearance.MouseOverBackColor = ControlPaint.Light(value);
                btnMaximize.BackColor = value;
                btnMaximize.FlatAppearance.MouseOverBackColor = ControlPaint.Light(value);
                Title.ForeColor = value;
                FormatControls();
                Refresh();
            }
        }

        public Color ThemeBorderColor
        {
            get
            {
                return _themeBorderColor;
            }
            set
            {
                _themeBorderColor = value;
                Refresh();
            }
        }

        public Color ThemeBackColor
        {
            get
            {
                return _themeBackColor;
            }
            set
            {
                _themeBackColor = value;
                btnClose.ForeColor = _themeBackColor;
                btnMinimize.ForeColor = _themeBackColor;
                btnMaximize.ForeColor = _themeBackColor;
                BackColor = _themeBackColor;
                FormatControls();
            }
        }
        public FormAnimation Animation { get; set; }
        public int AnimationSpeed = 100;

        // ReSharper disable ConvertToAutoProperty
        public Label Title
        // ReSharper restore ConvertToAutoProperty
        {
            set { lblTitle = value; }
            get { return lblTitle; }
        }



        private bool mouseIsDown;
        private Point firstPoint;




        const int AwHide = 0x00010000;
        const int AwActivate = 0X20000;

        const int AwHorPositive = 0X1;
        const int AwHorNegative = 0X2;

        const int AwVerPositive = 0x00000004;
        const int AwVerNegative = 0x00000008;

        const int AwSlide = 0X40000;
        const int AwBlend = 0X80000;
        const int AwCenter = 0x00000010;

        private const int CsDropshadow = 0x00020000;

        //protected override void OnLoad(System.EventArgs e)
        //{
        //    AnimateWindow(Handle, 100, AW_ACTIVATE | AW_SLIDE | AW_HOR_NEGATIVE);
        //    base.OnLoad(e);
        //}


        public RichForm()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            _themeForeColor = btnMinimize.BackColor;
            _themeBackColor = btnMinimize.ForeColor;
            ThemeList = new List<ThemeConfiguration>();
            if (LicenseManager.CurrentContext.UsageMode !=
                LicenseUsageMode.Designtime)
            {
                LoadConfiguration();
                MaximizedBounds = Screen.GetWorkingArea(this);
                if (btnImage.BackgroundImage != null)
                {
                    Icon = Icon.FromHandle(new Bitmap(btnImage.BackgroundImage).GetHicon());
                }
                _notifyIcon = new NotifyIcon { Icon = Icon, Visible = true };
            }
            AllowMaximize = true;
        }


        private void RichFormLoad(object sender, EventArgs e)
        {
            flowLayoutPanel1.Width = Width - 10;
            flpBottom.Width = Width - 10;
            tableLayoutPanel1.Width = Width - flowLayoutPanel2.Width - 25;
            ApplyThemeOnControl(cmbStatusLst);
            cmbStatusLst.BackColor = ThemeBackColor;
            cmbStatusLst.ForeColor = ThemeForeColor;
            ApplyThemeOnControl(lblStatusCount);
            btnImage.BackColor = ThemeBackColor;
            btnImage.FlatAppearance.BorderColor = ThemeBackColor;
            btnImage.FlatAppearance.MouseDownBackColor = ThemeBackColor;
            btnImage.FlatAppearance.MouseOverBackColor = ThemeBackColor;
            ShowStatusBar = false;
            btnImage.MouseDoubleClick += (s1, e1) =>
                {
                    Close();
                };
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            switch (Animation)
            {
                case FormAnimation.Blend:
                    AnimateWindow(Handle, AnimationSpeed, AwActivate | AwBlend | AwHorNegative);
                    break;
                case FormAnimation.Center:
                    AnimateWindow(Handle, AnimationSpeed, AwActivate | AwCenter | AwHorNegative);
                    break;
                case FormAnimation.None:
                    break;
                case FormAnimation.SlideRight:
                    AnimateWindow(Handle, AnimationSpeed, AwActivate | AwSlide | AwHorPositive);
                    break;
                case FormAnimation.SlideLeft:
                    AnimateWindow(Handle, AnimationSpeed, AwActivate | AwSlide | AwHorNegative);
                    break;
                case FormAnimation.SlideUp:
                    AnimateWindow(Handle, AnimationSpeed, AwActivate | AwSlide | AwVerNegative);
                    break;
                case FormAnimation.SlideDown:
                    AnimateWindow(Handle, AnimationSpeed, AwActivate | AwSlide | AwVerPositive);
                    break;
            }


        }

        private void btnImage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RichFormPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(ThemeBorderColor), new Rectangle(0, 0, Width - 1, Height - 1));
            //e.Graphics.DrawRectangle(new Pen(new SolidBrush(ThemeBorderColor), 20), e.ClipRectangle);
        }

        private void RichFormMouseDown(object sender, MouseEventArgs e)
        {
            this.firstPoint = e.Location;
            this.mouseIsDown = true;
        }

        private void RichFormMouseMove(object sender, MouseEventArgs e)
        {
            if (!this.mouseIsDown) return;
            var xDiff = this.firstPoint.X - e.Location.X;
            var yDiff = this.firstPoint.Y - e.Location.Y;
            var x = Location.X - xDiff;
            var y = Location.Y - yDiff;
            Location = new Point(x, y);
        }

        private void RichFormMouseUp(object sender, MouseEventArgs e)
        {
            this.mouseIsDown = false;
        }


        protected override CreateParams CreateParams
        {
            get
            {
                // add the drop shadow flag for automatically drawing
                // a drop shadow around the form
                var cp = base.CreateParams;
                cp.ClassStyle |= CsDropshadow;
                return cp;
            }
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            //Application.Exit();
            Close();
        }

        private void BtnMinimizeClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void FlowLayoutPanel1Paint(object sender, PaintEventArgs e)
        {

        }

        private void RichFormFormClosing(object sender, FormClosingEventArgs e)
        {
            switch (Animation)
            {
                case FormAnimation.Blend:
                    AnimateWindow(Handle, AnimationSpeed, AwHide | AwBlend | AwHorNegative);
                    break;
                case FormAnimation.Center:
                    AnimateWindow(Handle, AnimationSpeed, AwHide | AwCenter | AwHorNegative);
                    break;
                case FormAnimation.None:
                    break;
                case FormAnimation.SlideRight:
                    AnimateWindow(Handle, AnimationSpeed, AwHide | AwSlide | AwHorNegative);
                    break;
                case FormAnimation.SlideLeft:
                    AnimateWindow(Handle, AnimationSpeed, AwHide | AwSlide | AwHorPositive);
                    break;
                case FormAnimation.SlideUp:
                    AnimateWindow(Handle, AnimationSpeed, AwHide | AwSlide | AwVerPositive);
                    break;
                case FormAnimation.SlideDown:
                    AnimateWindow(Handle, AnimationSpeed, AwHide | AwSlide | AwVerNegative);
                    break;
            }
            if (SaveConfigurationOnClose)
            {
                this.SaveConfiguration();
            }
            _notifyIcon.Dispose();
        }

        public void SaveConfiguration()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var themeSection = config.Sections["themeSection"] as ThemeListConfiguration;
                if (themeSection == null) return;
                var newThemeSection = new ThemeListConfiguration();
                foreach (var themeConfiguration in ThemeList)
                {
                    newThemeSection.Themes.Add(themeConfiguration);
                }
                config.Sections.Remove("themeSection");
                config.Sections.Add("themeSection", newThemeSection);
                config.Save(ConfigurationSaveMode.Full);
            }
            catch (Exception ex)
            {
                "Error Occurred while updating themes  {0}".Fill(ex.Message).ShowError();
            }
        }



        private void LoadConfiguration()
        {
            try
            {
                ThemeList.Clear();
                var configSection = ConfigurationManager.GetSection("themeSection") as ThemeListConfiguration;
                if (configSection == null) return;
                foreach (var theme in from object theme in configSection.Themes
                                      let thisTheme = theme as ThemeConfiguration
                                      where thisTheme != null
                                      select theme)
                {
                    ThemeList.Add((ThemeConfiguration)theme);
                }
                ThemeList = ThemeList.OrderBy(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                "Error Occurred while loading themes  {0}".Fill(ex.Message).ShowInformation();
            }

        }

        public void ApplyThemeFromConfiguration()
        {
            try
            {
                LoadConfiguration();
                if (!ThemeList.Any()) return;
                if (!ThemeList.Any(x => x.Selected)) return;
                var selectedTheme = ThemeList.First(x => x.Selected);
                ApplyThemeFromConfiguration(selectedTheme);
            }
            catch (Exception ex)
            {
                "Error occurred while Applying theme Error: {0}".Fill(ex.Message).ShowError();
            }

        }

        public void ApplyThemeFromConfiguration(string themeName)
        {
            try
            {
                LoadConfiguration();
                if (!ThemeList.Any()) return;
                if (!ThemeList.Any(x => x.Name.Equals(themeName))) return;
                var selectedTheme = ThemeList.First(x => x.Name.Equals(themeName));
                ApplyThemeFromConfiguration(selectedTheme);
            }
            catch (Exception ex)
            {
                "Error occurred while Applying theme Error: {0}".Fill(ex.Message).ShowError();
            }
        }

        public void ApplyThemeFromConfiguration(ThemeConfiguration theme)
        {
            try
            {
                if (theme == null) return;
                var selectedTheme = theme;
                Title.Text = this.Text;
                ThemeForeColor = selectedTheme.Window.ForeColor;
                ThemeBackColor = selectedTheme.Window.BackColor;
                ThemeBorderColor = selectedTheme.Window.BorderColor;
                Animation = selectedTheme.Window.Animation;
                AnimationSpeed = selectedTheme.Window.AnimationSpeed;
                FormatControls();
                LoadConfiguration();
                if (!ThemeList.Any(x => x.Name.Equals(theme.Name))) return;
                foreach (var themeConfiguration in ThemeList)
                {
                    if (themeConfiguration.Name.Equals(selectedTheme.Name))
                    {
                        themeConfiguration.Selected = true;
                        continue;
                    }
                    themeConfiguration.Selected = false;
                }
            }
            catch (Exception ex)
            {
                "Error occurred while Applying theme Error: {0}".Fill(ex.Message).ShowError();
            }
        }

        public void ApplyThemeToControls()
        {
            var currentValue = AutoApplytThemeToControls;
            AutoApplytThemeToControls = true;
            FormatControls();
            AutoApplytThemeToControls = currentValue;
        }

        private void FormatControls()
        {
            if (!AutoApplytThemeToControls) return;
            ApplyThemeOnControls(this);
        }

        public void ApplyThemeOnControl(Control control)
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

            var radioButton = control as RadioButton;
            if (radioButton != null)
            {
                radioButton.ForeColor = ThemeForeColor;
            }

            var linkLabel = control as LinkLabel;
            if (linkLabel != null)
            {
                linkLabel.ForeColor = ThemeForeColor;
            }

            var comboBoxControl = control as ComboBox;
            if (comboBoxControl != null)
            {
                comboBoxControl.FlatStyle = FlatStyle.Flat;
                comboBoxControl.BackColor = ThemeForeColor;
                comboBoxControl.ForeColor = ThemeForeColor.IsDark() ? ThemeBackColor.Lighten().Lighten() : ThemeBackColor.Darken().Darken();
            }

            var buttonControl = control as Button;
            if (buttonControl != null)
            {
                buttonControl.FlatStyle = FlatStyle.Flat;
                buttonControl.BackColor = ThemeForeColor;
                buttonControl.ForeColor = ThemeForeColor.IsDark() ? ThemeBackColor.Lighten().Lighten() : ThemeBackColor.Darken().Darken();
                buttonControl.FlatAppearance.MouseOverBackColor = ThemeForeColor.Lighten();
                buttonControl.FlatAppearance.BorderColor = ThemeBorderColor;
            }

            var textBoxControl = control as TextBox;
            if (textBoxControl != null)
            {
                textBoxControl.BorderStyle = BorderStyle.FixedSingle;
                textBoxControl.BackColor = ThemeForeColor;
                textBoxControl.ForeColor = ThemeForeColor.IsDark() ? ThemeBackColor.Lighten().Lighten() : ThemeBackColor.Darken().Darken();
            }

            var numericControl = control as NumericUpDown;
            if (numericControl != null)
            {
                numericControl.BackColor = ThemeForeColor;
                numericControl.ForeColor = ThemeForeColor.IsDark() ? ThemeBackColor.Lighten().Lighten() : ThemeBackColor.Darken().Darken();
            }

            var progressBar = control as ProgressBar;
            if (progressBar != null)
            {
                SetWindowTheme(progressBar.Handle, "", "");
                progressBar.BackColor = ThemeForeColor;
                progressBar.ForeColor = ThemeForeColor.IsDark() ? ThemeBackColor.Lighten() : ThemeBackColor.Darken();
            }

            var listBox = control as ListBox;
            if (listBox != null)
            {
                listBox.ForeColor = ThemeForeColor.IsDark()
                                        ? ThemeBackColor.Lighten().Lighten()
                                        : ThemeBackColor.Darken().Darken();
                listBox.BackColor = ThemeForeColor;
                listBox.BorderStyle = BorderStyle.FixedSingle;
            }

            var checkedListBox = control as CheckedListBox;
            if (checkedListBox != null)
            {
                checkedListBox.ForeColor = ThemeForeColor.IsDark()
                                        ? ThemeBackColor.Lighten().Lighten()
                                        : ThemeBackColor.Darken().Darken();
                checkedListBox.BackColor = ThemeForeColor;
                checkedListBox.BorderStyle = BorderStyle.FixedSingle;
            }

            var menustrip = control as MenuStrip;
            if (menustrip != null)
            {
                menustrip.Dock = DockStyle.None;
                menustrip.Left = 1;
                menustrip.Top = flowLayoutPanel1.Height + 2;
                menustrip.Width = flowLayoutPanel1.Width;
                menustrip.AutoSize = false;
                menustrip.ForeColor = Color.Black;
                menustrip.BackColor = ThemeBackColor;
                menustrip.ForeColor = ThemeForeColor;
                SetWindowTheme(menustrip.Handle, "", "");
                menustrip.Renderer = new RichRenderer(ThemeBackColor, ThemeForeColor, ThemeForeColor);

            }

            var datagridview = control as DataGridView;
            if (datagridview != null)
            {
                datagridview.EnableHeadersVisualStyles = false;

                datagridview.ColumnHeadersDefaultCellStyle.BackColor = ThemeForeColor;
                datagridview.ColumnHeadersDefaultCellStyle.ForeColor = ThemeForeColor.IsDark() ? ThemeBackColor.Lighten().Lighten() : ThemeBackColor.Darken().Darken();
                datagridview.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

                datagridview.RowHeadersDefaultCellStyle.BackColor = ThemeForeColor;
                datagridview.RowHeadersDefaultCellStyle.ForeColor = ThemeForeColor.IsDark() ? ThemeBackColor.Lighten().Lighten() : ThemeBackColor.Darken().Darken();
                datagridview.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

                datagridview.GridColor = ThemeBackColor;

                datagridview.DefaultCellStyle.BackColor = ThemeForeColor.IsDark() ? ThemeBackColor.Lighten() : ThemeBackColor.Darken();
                datagridview.DefaultCellStyle.ForeColor = ThemeForeColor;
                datagridview.BackColor = ThemeBackColor;
                datagridview.BackgroundColor = ThemeBackColor;
                datagridview.ForeColor = ThemeForeColor;
            }

            var richConsole = control as RichConsoleUserControl;
            var isRichConsole = false;
            if (richConsole != null)
            {
                richConsole.ForeColor = ThemeForeColor;
                richConsole.BackColor = ThemeBackColor;
                isRichConsole = true;
            }

            var richTab = control as RichTabControl;
            if (richTab != null)
            {
                richTab.ForeColor = ThemeForeColor;
                richTab.BackColor = ThemeBackColor;
                richTab.MyBackColor = ThemeBackColor;
                for (var index = 0; index < richTab.TabPages.Count; index++)
                {
                    var tabPage = richTab.TabPages[index];
                    tabPage.ForeColor = this.ThemeForeColor;
                    tabPage.BackColor = this.ThemeBackColor;
                }
            }

            var tabPageCtrl = control as TabPage;
            if (tabPageCtrl != null)
            {
                tabPageCtrl.BackColor = ThemeBackColor;
                tabPageCtrl.ForeColor = ThemeForeColor;
            }


            var treeView = control as TreeView;
            if (treeView != null)
            {
                treeView.BackColor = ThemeBackColor;
                treeView.ForeColor = ThemeForeColor;
            }

            foreach (var childControl in control.Controls.Cast<Control>().TakeWhile(childControl => !isRichConsole))
            {
                SetWindowTheme(childControl.Handle, "", "");
                this.ApplyThemeOnControl(childControl);
            }


            btnImage.BackColor = ThemeBackColor;
            btnImage.FlatAppearance.BorderColor = ThemeBackColor;
            btnImage.FlatAppearance.MouseDownBackColor = ThemeBackColor;
            btnImage.FlatAppearance.MouseOverBackColor = ThemeBackColor;
            cmbStatusLst.BackColor = ThemeBackColor;
            cmbStatusLst.ForeColor = ThemeForeColor;
        }

        private void ApplyThemeOnControls(Control control)
        {
            foreach (var groupBox in control.Controls.OfType<GroupBox>())
            {
                groupBox.ForeColor = ThemeForeColor;
            }

            foreach (var labelControl in control.Controls.OfType<Label>())
            {
                labelControl.ForeColor = ThemeForeColor;
            }

            foreach (var checkBoxControl in control.Controls.OfType<CheckBox>())
            {
                checkBoxControl.ForeColor = ThemeForeColor;
            }

            foreach (var radioButton in control.Controls.OfType<RadioButton>())
            {
                radioButton.ForeColor = ThemeForeColor;
            }

            foreach (var linkLabel in control.Controls.OfType<LinkLabel>())
            {
                linkLabel.ForeColor = ThemeForeColor;
            }

            foreach (var comboBoxControl in control.Controls.OfType<ComboBox>())
            {
                comboBoxControl.FlatStyle = FlatStyle.Flat;
                comboBoxControl.BackColor = ThemeForeColor;
                comboBoxControl.ForeColor = ThemeForeColor.IsDark() ? ThemeBackColor.Lighten().Lighten() : ThemeBackColor.Darken().Darken();
            }

            foreach (var buttonControl in control.Controls.OfType<Button>())
            {
                buttonControl.FlatStyle = FlatStyle.Flat;
                buttonControl.BackColor = ThemeForeColor;
                buttonControl.ForeColor = ThemeForeColor.IsDark() ? ThemeBackColor.Lighten().Lighten() : ThemeBackColor.Darken().Darken();
                buttonControl.FlatAppearance.MouseOverBackColor = ThemeForeColor.Lighten();
                buttonControl.FlatAppearance.BorderColor = ThemeBorderColor;
            }

            foreach (var textBoxControl in control.Controls.OfType<TextBox>())
            {
                textBoxControl.BorderStyle = BorderStyle.FixedSingle;
                textBoxControl.BackColor = ThemeForeColor;
                textBoxControl.ForeColor = ThemeForeColor.IsDark() ? ThemeBackColor.Lighten().Lighten() : ThemeBackColor.Darken().Darken();
            }

            foreach (var numericControl in control.Controls.OfType<NumericUpDown>())
            {
                numericControl.BackColor = ThemeForeColor;
                numericControl.ForeColor = ThemeForeColor.IsDark() ? ThemeBackColor.Lighten().Lighten() : ThemeBackColor.Darken().Darken();
            }

            foreach (var progressBar in control.Controls.OfType<ProgressBar>())
            {
                SetWindowTheme(progressBar.Handle, "", "");
                progressBar.BackColor = ThemeForeColor;
                progressBar.ForeColor = ThemeForeColor.IsDark() ? ThemeBackColor.Lighten() : ThemeBackColor.Darken();
            }

            foreach (var listBox in control.Controls.OfType<ListBox>())
            {
                listBox.ForeColor = ThemeForeColor.IsDark()
                                        ? ThemeBackColor.Lighten().Lighten()
                                        : ThemeBackColor.Darken().Darken();
                listBox.BackColor = ThemeForeColor;
                listBox.BorderStyle = BorderStyle.FixedSingle;
            }

            foreach (var checkedListBox in control.Controls.OfType<CheckedListBox>())
            {
                checkedListBox.ForeColor = ThemeForeColor.IsDark()
                                        ? ThemeBackColor.Lighten().Lighten()
                                        : ThemeBackColor.Darken().Darken();
                checkedListBox.BackColor = ThemeForeColor;
                checkedListBox.BorderStyle = BorderStyle.FixedSingle;
            }

            foreach (var menustrip in control.Controls.OfType<MenuStrip>())
            {
                menustrip.Dock = DockStyle.None;
                menustrip.Left = 1;
                menustrip.Top = flowLayoutPanel1.Height + 2;
                menustrip.Width = flowLayoutPanel1.Width;
                menustrip.AutoSize = false;
                menustrip.ForeColor = Color.Black;
                menustrip.BackColor = ThemeBackColor;
                menustrip.ForeColor = ThemeForeColor;
                SetWindowTheme(menustrip.Handle, "", "");
                menustrip.Renderer = new RichRenderer(ThemeBackColor, ThemeForeColor, ThemeForeColor);

            }

            foreach (var datagridview in control.Controls.OfType<DataGridView>())
            {
                datagridview.EnableHeadersVisualStyles = false;

                datagridview.ColumnHeadersDefaultCellStyle.BackColor = ThemeForeColor;
                datagridview.ColumnHeadersDefaultCellStyle.ForeColor = ThemeForeColor.IsDark() ? ThemeBackColor.Lighten().Lighten() : ThemeBackColor.Darken().Darken();
                datagridview.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

                datagridview.RowHeadersDefaultCellStyle.BackColor = ThemeForeColor;
                datagridview.RowHeadersDefaultCellStyle.ForeColor = ThemeForeColor.IsDark() ? ThemeBackColor.Lighten().Lighten() : ThemeBackColor.Darken().Darken();
                datagridview.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

                datagridview.GridColor = ThemeBackColor;

                datagridview.DefaultCellStyle.BackColor = ThemeForeColor.IsDark() ? ThemeBackColor.Lighten() : ThemeBackColor.Darken();
                datagridview.DefaultCellStyle.ForeColor = ThemeForeColor;
                datagridview.BackColor = ThemeBackColor;
                datagridview.BackgroundColor = ThemeBackColor;
                datagridview.ForeColor = ThemeForeColor;
            }

            foreach (var treeView in control.Controls.OfType<TreeView>())
            {
                treeView.BackColor = ThemeBackColor;
                treeView.ForeColor = ThemeForeColor;
            }

            foreach (var richConsole in control.Controls.OfType<RichConsoleUserControl>())
            {
                richConsole.ForeColor = ThemeForeColor;
                richConsole.BackColor = ThemeBackColor;

                return;
            }

            foreach (var richTab in control.Controls.OfType<RichTabControl>())
            {
                richTab.ForeColor = ThemeForeColor;
                richTab.BackColor = ThemeBackColor;
                richTab.MyBackColor = ThemeBackColor;
                for (var index = 0; index < richTab.TabPages.Count; index++)
                {
                    var tabPage = richTab.TabPages[index];
                    tabPage.ForeColor = this.ThemeForeColor;
                    tabPage.BackColor = this.ThemeBackColor;
                }
            }

            foreach (Control childControl in control.Controls)
            {
                SetWindowTheme(childControl.Handle, "", "");
                ApplyThemeOnControls(childControl);
            }
            btnImage.BackColor = ThemeBackColor;
            btnImage.FlatAppearance.BorderColor = ThemeBackColor;
            btnImage.FlatAppearance.MouseDownBackColor = ThemeBackColor;
            btnImage.FlatAppearance.MouseOverBackColor = ThemeBackColor;

            cmbStatusLst.BackColor = ThemeBackColor;
            cmbStatusLst.ForeColor = ThemeForeColor;
        }


        private void BtnMaximizeClick(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
            Refresh();
        }

        private void FlowLayoutPanel1DoubleClick(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
            Refresh();
        }

        public void NotifyToBalloon(string title, string message, ToolTipIcon toolTipIcon, int timeout = 5000)
        {
            _notifyIcon.ShowBalloonTip(timeout, title, message, toolTipIcon);
        }

        public void NotifyToBalloon(string message, ToolTipIcon toolTipIcon, int timeout = 5000)
        {
            _notifyIcon.ShowBalloonTip(timeout, Title.Text, message, toolTipIcon);
        }

        public void NotifyToBalloon(string message, int timeout = 5000)
        {
            _notifyIcon.ShowBalloonTip(timeout, Title.Text, message, ToolTipIcon.Info);
        }

        private void RichFormResize(object sender, EventArgs e)
        {
            if (!AllowMaximize)
            {
                MaximizedBounds = new Rectangle(Location.X, Location.Y, this.Width, this.Height);
            }
            flowLayoutPanel1.Width = Width - 10;
            flpBottom.Width = Width - 10;
            //flpBottom.Location = new Point(Location.X + 5, Location.Y + Height - flpBottom.Height - 5);
            flpBottom.Left = 5;
            flpBottom.Top = Height - flpBottom.Height - 5;
            tableLayoutPanel1.Width = Width - flowLayoutPanel2.Width - 25;
            btnImage.BackColor = ThemeBackColor;
            btnImage.FlatAppearance.BorderColor = ThemeBackColor;
            btnImage.FlatAppearance.MouseDownBackColor = ThemeBackColor;
            btnImage.FlatAppearance.MouseOverBackColor = ThemeBackColor;
        }


        private void FlpBottomResize(object sender, EventArgs e)
        {
            tableLayoutPanel2.Width = flpBottom.Width - 1;
        }

        private void LblTitleDoubleClick(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
            Refresh();
        }

        private void TableLayoutPanel1DoubleClick(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
            Refresh();
        }

        public void SetStatusBarMessage(string message)
        {
            try
            {
                _statuses.Add(message, true);

                cmbStatusLst.Items.Add(message);
                cmbStatusLst.SelectedItem = message;
                cmbSelectedIndex = cmbStatusLst.SelectedIndex;
                lblStatusCount.Text = _statuses.Count(x => x.Value).ToString(CultureInfo.InvariantCulture);
                SetIcon(lblStatusCount.Text);
            }
            catch
            { }
        }

        private void SetIcon(string message)
        {
            var bitmap = new Bitmap(btnImage.BackgroundImage);
            var g = Graphics.FromImage(bitmap);
            if (_setStatusCountInIcon)
            {
                //RectangleF rectf = new RectangleF(10, 10, 20, 10);
                //g.FillRectangle(System.Drawing.Brushes.Black, 0, 0, 10, 10);
                //g.DrawRectangle(new Pen(Color.Red, 5), 0, 0, 20, 20);
                // ReSharper disable PossibleLossOfFraction
                g.DrawString(message, new Font("Courier New", 14, FontStyle.Bold), Brushes.Black, new PointF(bitmap.Width / 4, bitmap.Height / 4));
                // ReSharper restore PossibleLossOfFraction
                g.Flush();
                FlashWindowEx(this);
            }
            IntPtr hBitmap = bitmap.GetHicon();
            this.Icon = Icon.FromHandle(hBitmap);
        }

        public void ShowFlash()
        {
            FlashWindowEx(this);
        }


        #region FlashWindow
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FlashWindowEx(ref Flashwinfo pwfi);
        [StructLayout(LayoutKind.Sequential)]
        public struct Flashwinfo
        {
            public UInt32 cbSize;
            public IntPtr hwnd;
            public UInt32 dwFlags;
            public UInt32 uCount;
            public UInt32 dwTimeout;
        }


        //Stop flashing. The system restores the window to its original state. 
        public const UInt32 FlashwStop = 0;
        //Flash the window caption. 
        public const UInt32 FlashwCaption = 1;
        //Flash the taskbar button. 
        public const UInt32 FlashwTray = 2;
        //Flash both the window caption and taskbar button.
        //This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags. 
        public const UInt32 FlashwAll = 3;
        //Flash continuously, until the FLASHW_STOP flag is set. 
        public const UInt32 FlashwTimer = 4;
        //Flash continuously until the window comes to the foreground. 
        public const UInt32 FlashwTimernofg = 12;

        public static bool FlashWindowEx(Form frm)
        {
            var hWnd = frm.Handle;
            var fInfo = new Flashwinfo();

            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.hwnd = hWnd;
            fInfo.dwFlags = FlashwAll | FlashwTimernofg;
            fInfo.uCount = UInt32.MaxValue;
            fInfo.dwTimeout = 0;

            return FlashWindowEx(ref fInfo);
        }
        #endregion

        private void CmbStatusLstSelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CmbStatusLstClick(object sender, EventArgs e)
        {
            foreach (var key in new List<string>(this._statuses.Keys))
            {
                this._statuses[key] = false;
            }
            lblStatusCount.Text = _statuses.Count(x => x.Value).ToString(CultureInfo.InvariantCulture);
            SetIcon(lblStatusCount.Text);
        }

        private void CmbStatusLstSelectionChangeCommitted(object sender, EventArgs e)
        {
            cmbStatusLst.SelectedIndex = cmbSelectedIndex;
        }

        private void btnImage_Click(object sender, EventArgs e)
        {

        }
    }
}
