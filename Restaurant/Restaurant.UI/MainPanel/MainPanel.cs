using System;
using System.Windows.Forms;
using Restaurant.Infrastructure.Requests;

namespace Restaurant.UI
{
    public partial class MainPanel : Form
    {
        private readonly IRequestHandler _requestHandler;

        public MainPanel(IRequestHandler requestHandler, Menu menu, Settings settings, History history)
        {
            _requestHandler = requestHandler;
            menu1 = menu;
            settings1 = settings;
            history1 = history;
            InitializeComponent();
            panel3.Controls.Add(menu1);
            panel3.Controls.Add(settings1);
            panel3.Controls.Add(history1);
    }

        private void ShowMenu(object sender, EventArgs e)
        {
            settings1.Visible = false;
            history1.Visible = false;
            menu1.Visible = true;
            menu1.BringToFront();
        }

        private void ShowSettings(object sender, EventArgs e)
        {
            menu1.Visible = false;
            history1.Visible = false;
            settings1.Visible = true;
            settings1.BringToFront();
        }

        private void ShowHistory(object sender, EventArgs e)
        {
            menu1.Visible = false;
            settings1.Visible = false;
            history1.Visible = true;
            history1.BringToFront();
        }

        private void Close(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
