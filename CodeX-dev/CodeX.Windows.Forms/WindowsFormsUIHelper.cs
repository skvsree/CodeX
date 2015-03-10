using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

// ReSharper disable CheckNamespace
namespace CodeX.Windows.Forms
// ReSharper restore CheckNamespace
{
    /// <summary>
    /// Windows Form UI helper
    /// </summary>
    public static class WindowsFormsUIHelper
    {
        /// <summary>
        /// Shows the given string as error Messagebox.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="title">The title.</param>
        public static DialogResult ShowError(this string message, string title = "Error")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows the given string as Information Messagebox.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="title">The title.</param>
        public static DialogResult ShowInformation(this string message, string title = "Information")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Shows the given string as warning Messagebox.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="title">The title.</param>
        public static DialogResult ShowWarning(this string message, string title = "Warning")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Yes or No
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="title">The title.</param>
        /// <returns>DialogResult</returns>
        public static DialogResult YesNo(this string message, string title = "?")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        /// <summary>
        /// Yes, no or cancel.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="title">The title.</param>
        /// <returns>DialogResult</returns>
        public static DialogResult YesNoCancel(this string message, string title = "?")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }

        /// <summary>
        /// Determines whether [is current process is elevated] .
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>
        ///   <c>true</c> [is current process is elevated]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsProcessElevated(this Form form)
        {
            return UACHelper.IsProcessElevated;
        }

        /// <summary>
        /// Determines whether [is UAC enabled] [for current process].
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>
        ///   <c>true</c> if [is UAC enabled] [for current process]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUACEnabled(this Form form)
        {
            return UACHelper.IsUacEnabled;
        }

        /// <summary>
        /// Determines whether the specified color is dark.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>
        ///   <c>true</c> if the specified color is dark; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDark(this Color color)
        {
            var sum = color.R + color.G + color.B;
            return sum < 382;
        }

        /// <summary>
        /// Darkens the specified color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>Darkened Color</returns>
        public static Color Darken(this Color color)
        {
            return ControlPaint.Dark(color);
        }


        /// <summary>
        /// Lightens the specified color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>Lightened Color</returns>
        public static Color Lighten(this Color color)
        {
            return ControlPaint.Light(color);
        }

        /// <summary>
        /// Gets the Current word from textbox
        /// </summary>
        /// <param name="txtBox">textbox</param>
        /// <returns>string</returns>
        public static string GetCurrentWord(this TextBox txtbox)
        {
            var curPos = txtbox.SelectionStart -1;
            var startPos = curPos;
            if (startPos < 0) return string.Empty;
            var endPos = txtbox.Text.IndexOf(" ", startPos, System.StringComparison.Ordinal);
            if (endPos < 0)
            {
                endPos = txtbox.Text.Length;
            }

            if (startPos == txtbox.Text.Length)
            {
                return "";
            }

            startPos = txtbox.Text.LastIndexOf(" ", curPos, System.StringComparison.Ordinal);
            if (startPos < 0)
            {
                startPos = 0;
            }
            return txtbox.Text.Substring(startPos, endPos - startPos).Trim();
        }

    }

    /// <summary>
    /// Theme color
    /// </summary>
    public class ThemeColor
    {
        public static readonly Color Purple = Color.FromArgb(162, 0, 255);
        public static readonly Color Magneta = Color.FromArgb(255, 0, 151);
        public static readonly Color Teal = Color.FromArgb(0, 171, 169);
        public static readonly Color Lime = Color.FromArgb(140, 191, 38);
        public static readonly Color Brown = Color.FromArgb(160, 80, 0);
        public static readonly Color Pink = Color.FromArgb(230, 113, 184);
        public static readonly Color Orange = Color.FromArgb(240, 150, 9);
        public static readonly Color Blue = Color.FromArgb(27, 161, 226);
        public static readonly Color Red = Color.FromArgb(229, 20, 0);
        public static readonly Color Green = Color.FromArgb(51, 153, 51);
        public static readonly Color DarkGray = Color.FromArgb(64, 64, 64);
        public static readonly Color Black = Color.Black;
        private static readonly IDictionary<string, Color> ColorDict = new Dictionary<string, Color>
            {
                { "PurpleX",Purple},
                { "MagnetaX",Magneta},
                { "TealX",Teal},
                { "LimeX",Lime},
                { "BrownX",Brown},
                { "PinkX",Pink},
                { "OrangeX",Orange},
                { "BlueX",Blue},
                { "RedX",Red},
                { "GreenX",Green},
                { "DarkGrayX",DarkGray},
                { "BlackX",Black}
            };

        public static Color GetColor(string s)
        {
            if (ColorDict.Keys.Any(x => x.Equals(s)))
            {
                return ColorDict.First(x => x.Key.Equals(s)).Value;
            }
            var color = Color.FromName(s);
            return color;
        }

        public static string GetColorString(Color color)
        {
            if (ColorDict.Values.Any(x => x.Equals(color)))
            {
                return ColorDict.First(x => x.Value.Equals(color)).Key;
            }
            return color.ToString();
        }

        public static Color GetColor(int red, int green, int blue)
        {
            return Color.FromArgb(red, green, blue);
        }

        public static IList<string> GetColorList()
        {
            var retList = new List<string>
                {
                    "PurpleX",
                    "MagnetaX",
                    "TealX",
                    "LimeX",
                    "BrownX",
                    "PinkX",
                    "OrangeX",
                    "BlueX",
                    "RedX",
                    "GreenX",
                    "DarkGrayX",
                    "BlackX"
                };
            retList.AddRange(from prop in typeof(Color).GetProperties() where prop.PropertyType.FullName == "System.Drawing.Color" && prop.Name != "Transparent" select prop.Name);
            return retList.OrderBy(x => x).ToList();
        }

    }

    public enum FormAnimation
    {
        SlideDown,
        SlideUp,
        SlideLeft,
        SlideRight,
        Blend,
        Center,
        None
    }

    public class RichRenderer : ToolStripProfessionalRenderer
    {
        public Color BackColor { get; private set; }
        public Color ForeColor { get; private set; }
        public Color BorderColor { get; private set; }

        public RichRenderer(Color backColor, Color foreColor, Color borderColor)
        {
            BackColor = backColor;
            ForeColor = foreColor;
            BorderColor = borderColor;
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (!e.Item.Selected)
            {
                base.OnRenderMenuItemBackground(e);
                e.Item.BackColor = BackColor;
            }
            else
            {
                var rc = new Rectangle(Point.Empty, e.Item.Size);
                e.Graphics.FillRectangle(new SolidBrush(ForeColor), rc);
                e.Graphics.DrawRectangle(new Pen(BorderColor), 1, 0, rc.Width - 2, rc.Height - 1);
                e.Item.BackColor = ForeColor;
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            base.OnRenderItemText(e);
            if (!e.Item.Selected)
            {
                e.Item.ForeColor = ForeColor;
            }
            else
            {
                e.Item.ForeColor = BackColor;
            }
        }
    }

}
