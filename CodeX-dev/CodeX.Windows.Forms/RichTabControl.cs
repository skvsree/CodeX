namespace CodeX.Windows.Forms
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Drawing.Design;

    public class RichTabControl : TabControl
    {
        private Container components;
        private SubClass scUpDown;
        private bool bUpDown;
        private readonly ImageList leftRightImages;
        private const int NMargin = 5;
        private Color mBackColor = SystemColors.Control;

        public RichTabControl()
        {

            this.InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this.bUpDown = false;

            this.ControlAdded += this.FlatTabControlControlAdded;
            this.ControlRemoved += this.FlatTabControlControlRemoved;
            this.SelectedIndexChanged += this.FlatTabControlSelectedIndexChanged;

            this.leftRightImages = new ImageList();
            var updownImage = Resource1.TabIcons;
            if (updownImage == null)
            {
                return;
            }
            //updownImage.MakeTransparent(Color.White);
            this.leftRightImages.Images.AddStrip(updownImage);
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }

                this.leftRightImages.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            this.DrawControl(e.Graphics);
        }

        internal void DrawControl(Graphics g)
        {
            if (!this.Visible)
                return;

            var tabControlArea = this.ClientRectangle;
            var tabArea = this.DisplayRectangle;

            Brush br = new SolidBrush(this.mBackColor);
            g.FillRectangle(br, tabControlArea);
            br.Dispose();

            var nDelta = SystemInformation.Border3DSize.Width;

            var border = new Pen(SystemColors.ControlDark);
            tabArea.Inflate(nDelta, nDelta);
            g.DrawRectangle(border, tabArea);
            border.Dispose();

            var rsaved = g.Clip;

            var nWidth = tabArea.Width + NMargin;
            if (this.bUpDown)
            {
                // exclude updown control for painting
                if (Win32.IsWindowVisible(this.scUpDown.Handle))
                {
                    var rupdown = new Rectangle();
                    Win32.GetWindowRect(this.scUpDown.Handle, ref rupdown);
                    Rectangle rupdown2 = this.RectangleToClient(rupdown);

                    nWidth = rupdown2.X;
                }
            }

            var rreg = new Rectangle(tabArea.Left, tabControlArea.Top, nWidth - NMargin, tabControlArea.Height);

            g.SetClip(rreg);

            for (var i = 0; i < this.TabCount; i++)
                this.DrawTab(g, this.TabPages[i], i);

            g.Clip = rsaved;

            if (this.SelectedTab == null)
            {
                return;
            }
            var tabPage = this.SelectedTab;
            var color = tabPage.BackColor;
            border = new Pen(color);

            tabArea.Offset(1, 1);
            tabArea.Width -= 2;
            tabArea.Height -= 2;

            g.DrawRectangle(border, tabArea);
            tabArea.Width -= 1;
            tabArea.Height -= 1;
            g.DrawRectangle(border, tabArea);

            border.Dispose();
            //----------------------------
        }

        internal void DrawTab(Graphics g, TabPage tabPage, int nIndex)
        {
            var recBounds = this.GetTabRect(nIndex);
            var tabTextArea = (RectangleF)this.GetTabRect(nIndex);

            var bSelected = (this.SelectedIndex == nIndex);

            var pt = new Point[7];
            if (this.Alignment == TabAlignment.Top)
            {
                pt[0] = new Point(recBounds.Left, recBounds.Bottom);
                pt[1] = new Point(recBounds.Left, recBounds.Top + 3);
                pt[2] = new Point(recBounds.Left + 2, recBounds.Top);
                pt[3] = new Point(recBounds.Right - 2, recBounds.Top);
                pt[4] = new Point(recBounds.Right, recBounds.Top + 3);
                pt[5] = new Point(recBounds.Right, recBounds.Bottom);
                pt[6] = new Point(recBounds.Left, recBounds.Bottom);
            }
            else
            {
                pt[0] = new Point(recBounds.Left, recBounds.Top);
                pt[1] = new Point(recBounds.Right, recBounds.Top);
                pt[2] = new Point(recBounds.Right, recBounds.Bottom - 3);
                pt[3] = new Point(recBounds.Right - 2, recBounds.Bottom);
                pt[4] = new Point(recBounds.Left + 2, recBounds.Bottom);
                pt[5] = new Point(recBounds.Left, recBounds.Bottom - 3);
                pt[6] = new Point(recBounds.Left, recBounds.Top);
            }

            Brush br = new SolidBrush(tabPage.BackColor);
            g.FillPolygon(br, pt);
            br.Dispose();

            g.DrawPolygon(SystemPens.ControlDark, pt);

            if (bSelected)
            {
                var pen = new Pen(tabPage.BackColor);

                switch (this.Alignment)
                {
                    case TabAlignment.Top:
                        g.DrawLine(pen, recBounds.Left + 1, recBounds.Bottom, recBounds.Right - 1, recBounds.Bottom);
                        g.DrawLine(pen, recBounds.Left + 1, recBounds.Bottom + 1, recBounds.Right - 1, recBounds.Bottom + 1);
                        break;

                    case TabAlignment.Bottom:
                        g.DrawLine(pen, recBounds.Left + 1, recBounds.Top, recBounds.Right - 1, recBounds.Top);
                        g.DrawLine(pen, recBounds.Left + 1, recBounds.Top - 1, recBounds.Right - 1, recBounds.Top - 1);
                        g.DrawLine(pen, recBounds.Left + 1, recBounds.Top - 2, recBounds.Right - 1, recBounds.Top - 2);
                        break;
                }

                pen.Dispose();
            }

            if ((tabPage.ImageIndex >= 0) && (this.ImageList != null))
            {
                const int NLeftMargin = 8;
                const int NRightMargin = 2;

                var img = this.ImageList.Images[tabPage.ImageIndex];

                var rimage = new Rectangle(recBounds.X + NLeftMargin, recBounds.Y + 1, img.Width, img.Height);

                var nAdj = (float)(NLeftMargin + img.Width + NRightMargin);

                rimage.Y += (recBounds.Height - img.Height) / 2;
                tabTextArea.X += nAdj;
                tabTextArea.Width -= nAdj;

                g.DrawImage(img, rimage);
            }

            var stringFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

            br = new SolidBrush(tabPage.ForeColor);

            g.DrawString(tabPage.Text, this.Font, br, tabTextArea, stringFormat);
        }

        internal void DrawIcons(Graphics g)
        {
            if ((this.leftRightImages == null) || (this.leftRightImages.Images.Count != 4))
                return;


            var tabControlArea = this.ClientRectangle;

            var r0 = new Rectangle();
            Win32.GetClientRect(this.scUpDown.Handle, ref r0);

            Brush br = new SolidBrush(SystemColors.Control);
            g.FillRectangle(br, r0);
            br.Dispose();

            var border = new Pen(SystemColors.ControlDark);
            var rborder = r0;
            rborder.Inflate(-1, -1);
            g.DrawRectangle(border, rborder);
            border.Dispose();

            var nMiddle = (r0.Width / 2);
            var nTop = (r0.Height - 16) / 2;
            var nLeft = (nMiddle - 16) / 2;

            var r1 = new Rectangle(nLeft, nTop, 16, 16);
            var r2 = new Rectangle(nMiddle + nLeft, nTop, 16, 16);

            var img = this.leftRightImages.Images[1];
            Rectangle r3;
            if (this.TabCount > 0)
            {
                r3 = this.GetTabRect(0);
                if (r3.Left < tabControlArea.Left)
                    g.DrawImage(img, r1);
                else
                {
                    img = this.leftRightImages.Images[3];
                    g.DrawImage(img, r1);
                }
            }

            img = this.leftRightImages.Images[0];
            if (this.TabCount <= 0)
            {
                return;
            }
            r3 = this.GetTabRect(this.TabCount - 1);
            if (r3.Right > (tabControlArea.Width - r0.Width))
                g.DrawImage(img, r2);
            else
            {
                img = this.leftRightImages.Images[2];
                g.DrawImage(img, r2);
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.FindUpDown();
        }

        private void FlatTabControlControlAdded(object sender, ControlEventArgs e)
        {
            this.FindUpDown();
            this.UpdateUpDown();
        }

        private void FlatTabControlControlRemoved(object sender, ControlEventArgs e)
        {
            this.FindUpDown();
            this.UpdateUpDown();
        }

        private void FlatTabControlSelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateUpDown();
            this.Invalidate();	// we need to update border and background colors
        }

        private void FindUpDown()
        {
            var bFound = false;

            var pWnd = Win32.GetWindow(this.Handle, Win32.GwChild);

            while (pWnd != IntPtr.Zero)
            {
                var className = new char[33];

                var length = Win32.GetClassName(pWnd, className, 32);

                var s = new string(className, 0, length);

                if (s == "msctls_updown32")
                {
                    bFound = true;

                    if (!this.bUpDown)
                    {
                        this.scUpDown = new SubClass(pWnd, true);
                        this.scUpDown.SubClassedWndProc += this.ScUpDownSubClassedWndProc;

                        this.bUpDown = true;
                    }
                    break;
                }

                pWnd = Win32.GetWindow(pWnd, Win32.GwHwndnext);
            }

            if ((!bFound) && (this.bUpDown))
                this.bUpDown = false;
        }

        private void UpdateUpDown()
        {
            if (this.bUpDown)
            {
                if (Win32.IsWindowVisible(this.scUpDown.Handle))
                {
                    var rect = new Rectangle();

                    Win32.GetClientRect(this.scUpDown.Handle, ref rect);
                    Win32.InvalidateRect(this.scUpDown.Handle, ref rect, true);
                }
            }
            this.Invalidate();
        }

        private int ScUpDownSubClassedWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Win32.WmPaint:
                    {

                        var hDc = Win32.GetWindowDC(this.scUpDown.Handle);
                        var g = Graphics.FromHdc(hDc);

                        this.DrawIcons(g);

                        g.Dispose();
                        Win32.ReleaseDC(this.scUpDown.Handle, hDc);

                        m.Result = IntPtr.Zero;

                        var rect = new Rectangle();

                        Win32.GetClientRect(this.scUpDown.Handle, ref rect);
                        Win32.ValidateRect(this.scUpDown.Handle, ref rect);
                    }
                    return 1;
            }

            return 0;
        }

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new Container();
        }



        [Editor(typeof(TabpageExCollectionEditor), typeof(UITypeEditor))]
        public new TabPageCollection TabPages
        {
            get
            {
                return base.TabPages;
            }
        }

        new public TabAlignment Alignment
        {
            get { return base.Alignment; }
            set
            {
                TabAlignment ta = value;
                if ((ta != TabAlignment.Top) && (ta != TabAlignment.Bottom))
                    ta = TabAlignment.Top;

                base.Alignment = ta;
            }
        }

        [Browsable(false)]
        new public bool Multiline
        {
            get { return base.Multiline; }
            set
            {
                this.bUpDown = value;
                base.Multiline = false;
            }
        }

        [Browsable(true)]
        public Color MyBackColor
        {
            get { return this.mBackColor; }
            set { 
                this.mBackColor = value; 
                this.Invalidate();
            }
        }


        internal class TabpageExCollectionEditor : CollectionEditor
        {
            public TabpageExCollectionEditor(System.Type type)
                : base(type)
            {
            }

            protected override Type CreateCollectionItemType()
            {
                return typeof(TabPage);
            }
        }

    }

}
