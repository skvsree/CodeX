namespace CodeX.Windows.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;

    internal class Win32
    {
        //
        // GetWindow Constants
        // 
        public const int GwHwndfirst = 0;
        public const int GwHwndlast = 1;
        public const int GwHwndnext = 2;
        public const int GwHwndprev = 3;
        public const int GwOwner = 4;
        public const int GwChild = 5;

        public const int WmNccalcsize = 0x83;
        public const int WmWindowposchanging = 0x46;
        public const int WmPaint = 0xF;
        public const int WmCreate = 0x1;
        public const int WmNccreate = 0x81;
        public const int WmNcpaint = 0x85;
        public const int WmPrint = 0x317;
        public const int WmDestroy = 0x2;
        public const int WmShowwindow = 0x18;
        public const int WmSharedMenu = 0x1E2;
        public const int HcAction = 0;
        public const int WhCallwndproc = 4;
        public const int GwlWndproc = -4;
        
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowDC(IntPtr handle);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr ReleaseDC(IntPtr handle, IntPtr hDc);

        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hwnd, char[] className, int maxCount);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindow(IntPtr hwnd, int uCmd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool IsWindowVisible(IntPtr hwnd);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int GetClientRect(IntPtr hwnd, ref Rect lpRect);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int GetClientRect(IntPtr hwnd, [In, Out] ref Rectangle rect);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool UpdateWindow(IntPtr hwnd);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool InvalidateRect(IntPtr hwnd, ref Rectangle rect, bool bErase);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool ValidateRect(IntPtr hwnd, ref Rectangle rect);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool GetWindowRect(IntPtr hWnd, [In, Out] ref Rectangle rect);

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Windowpos
        {
            public IntPtr hwnd;
            public IntPtr hwndAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public uint flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NccalcsizeParams
        {
            public Rect rgc;
            public Windowpos wndpos;
        }
    }

    internal class SubClass : NativeWindow
    {
        public delegate int SubClassWndProcEventHandler(ref Message m);
        public event SubClassWndProcEventHandler SubClassedWndProc;
        private bool isSubClassed;

        public SubClass(IntPtr handle, bool subClass)
        {
            this.AssignHandle(handle);
            this.isSubClassed = subClass;
        }

        public bool SubClassed
        {
            get { return this.isSubClassed; }
            set { this.isSubClassed = value; }
        }

        protected override void WndProc(ref Message m)
        {
            if (this.isSubClassed)
            {
                if (this.OnSubClassedWndProc(ref m) != 0)
                    return;
            }
            base.WndProc(ref m);
        }

        public void CallDefaultWndProc(ref Message m)
        {
            base.WndProc(ref m);
        }

       public int HiWord(int number)
        {
            return ((number >> 16) & 0xffff);
        }

        public int LoWord(int number)
        {
            return (number & 0xffff);
        }

        public int MakeLong(int loWord, int hiWord)
        {
            return (hiWord << 16) | (loWord & 0xffff);
        }

        public IntPtr MakeLParam(int loWord, int hiWord)
        {
            return (IntPtr)((hiWord << 16) | (loWord & 0xffff));
        }

        private int OnSubClassedWndProc(ref Message m)
        {
            return this.SubClassedWndProc != null ? this.SubClassedWndProc(ref m) : 0;
        }
    }
}
