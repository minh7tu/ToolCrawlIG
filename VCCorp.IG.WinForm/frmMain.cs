using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VCCorp.IG.WinForm
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnGetNewSource_Click(object sender, EventArgs e)
        {
            frmListSource frm = new frmListSource();
            frm.Show();
        }

        private void btnGetSource_Click(object sender, EventArgs e)
        {
            frmListSource frm = new frmListSource();
            frm.Show();
        }
    }
}
