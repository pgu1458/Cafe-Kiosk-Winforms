using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiosK
{
    public partial class NaverpayForm : Form
    {
        public NaverpayForm()
        {
            InitializeComponent();
        }
        


        private void pictureBox1_Click(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
