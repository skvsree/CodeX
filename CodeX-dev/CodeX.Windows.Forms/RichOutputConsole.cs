using System;
using System.Drawing;
using System.Windows.Forms;

// ReSharper disable CheckNamespace
namespace CodeX.Windows.Forms
// ReSharper restore CheckNamespace
{
    public class RichOutputConsole : RichTextBox
    {
        public void WriteError(string message)
        {
            WriteLine(message, Color.Red);
        }

        public void WriteInfo(string message)
        {
            WriteLine(message, Color.White);
        }

        public void WriteWarning(string message)
        {
            WriteLine(message, Color.Orange);
        }

        public void WriteLine(string message, Color color)
        {
            var richTextBox = this;
            if (richTextBox.InvokeRequired)
            {
                richTextBox.Invoke(new Action(() =>
                {
                    richTextBox.SelectionColor = color;
                    richTextBox.AppendText(string.Format("{0}{1}", message, Environment.NewLine));
                    richTextBox.SelectionStart = richTextBox.Text.Length;
                    richTextBox.ScrollToCaret();
                }));
            }
            else
            {
                //richTextBox.Invoke()
                richTextBox.SelectionColor = color;
                richTextBox.AppendText(string.Format("{0}{1}", message, Environment.NewLine));
                richTextBox.SelectionStart = richTextBox.Text.Length;
                richTextBox.ScrollToCaret();
            }
        }

    }
}