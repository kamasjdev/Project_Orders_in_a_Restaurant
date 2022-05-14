using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant.UI.Dialog
{
    internal class DialogWindow : Form
    {
        private readonly Label textLabel;
        public readonly TextBox InputBox;
        private readonly Button confirmation;

        public DialogWindow(int width, int height, string caption, string text, EventHandler eventHandler)
        {
            this.Width = width;
            this.Height = height;
            this.Text = caption;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            textLabel = new Label() { Left = 50, Top = 20, Text = text };
            InputBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70 };
            this.Controls.Add(confirmation);
            this.Controls.Add(textLabel);
            this.Controls.Add(InputBox);
            confirmation.Click += (sender, e) => {
                eventHandler(this, e);
            };
        }
    }
}
