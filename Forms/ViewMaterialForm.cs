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
    /// <param>
    /// Отображение и работа главной формы 
    /// </param>
    using SQLClass = Modules.SQLClass;
    public partial class ViewMaterialForm : Form
    {
        public static string id = "";
        public ViewMaterialForm()
        {
            InitializeComponent();

            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            SQLClass.GiveMaterialTipe(comboBox1);
            comboBox2.Items.Clear();
            comboBox2.Items.Add("по возрастанию наименования");
            comboBox2.Items.Add("по убыванию наименования");

            comboBox2.Items.Add("по возрастанию стоимости");
            comboBox2.Items.Add("по убыванию стоимости");

            comboBox2.Items.Add("по возрастанию остатка на складе");
            comboBox2.Items.Add("по убыванию остатка на складе");
            comboBox2.SelectedIndex = 0;



        }

        private void ViewMaterialForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void редактироватьМатериалToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditMaterialsForm forn = new EditMaterialsForm();
            this.Visible = false;
            forn.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddMaterialsForm forn = new AddMaterialsForm();
            this.Visible = false;
            forn.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SQLClass.GiveMaterialTable(dataGridView1, comboBox1.Text, comboBox2.Text, textBox1.Text);

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            SQLClass.GiveMaterialTable(dataGridView1, comboBox1.Text, comboBox2.Text, textBox1.Text);

        }

        private void ViewMaterialForm_Load(object sender, EventArgs e)
        {
            SQLClass.GiveMaterialTable(dataGridView1, comboBox1.Text, comboBox2.Text, textBox1.Text);

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }
            catch { }
        }

 
    }
}
