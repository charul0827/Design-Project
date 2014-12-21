using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotSwarmServer
{
    public partial class LogDisplay : Form
    {
        public LogDisplay(String title, String[] list){
            InitializeComponent();

            this.Text = title;

            this.list = list;
            fillList();
        }

        private void fillList()
        {
            LogList.Clear();

            foreach(String element in list){
                LogList.Items.Add(element);
            }
        }

        private void updateList(String[] list)
        {
            this.list = list;
            fillList();
        }

        String[] list;
    }
}
