using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _Большая_пачка_.Forms
{
    using SQLClass = Modules.SQLClass;

    public partial class AddMaterialsForm : Form
    {
        public AddMaterialsForm()
        {
            InitializeComponent();
        }

        private void AddMaterialsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewMaterialForm form = new ViewMaterialForm();
            this.Visible = false;
            form.ShowDialog();
        }

        private void AddMaterialsForm_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
