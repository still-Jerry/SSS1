using System;
using System.IO;
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

    public partial class EditMaterialsForm : Form
    {
        List<string> info = new  List<string>();
        public EditMaterialsForm()
        {
            InitializeComponent();
            SQLClass.GiveMatInofo(info, ViewMaterialForm.id);
            
            SQLClass.GiveMaterialTipe(comboBox1);
            comboBox1.Items.RemoveAt(0);

        }

        private void EditMaterialsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

      

        private void EditMaterialsForm_Load(object sender, EventArgs e)
        {
            try
            {
                Idlable.Text = "ID = "+info[0];
                textBox3.Text = info[1];
                textBox4.Text = info[3];
                textBox2.Text = info[8];
                textBox1.Text = info[6];
                var path = Directory.GetCurrentDirectory() + "\\Res";
                if (textBox2.Text == "")
                {
                    pictureBox2.Image = Image.FromFile(path + "\\materials\\picture.png");

                }
                else
                {
                    pictureBox2.Image = Image.FromFile(path + info[8]);

                }
                comboBox1.Text = info[9];

                numericUpDown1.Value = Convert.ToDecimal(info[7]);
                numericUpDown2.Value = Convert.ToDecimal(info[5]);
                numericUpDown3.Value = Convert.ToDecimal(info[4]);
                numericUpDown4.Value = Convert.ToDecimal(info[2]);

            }
            catch { }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewMaterialForm form = new ViewMaterialForm();
            this.Visible = false;
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Пожалуйста, заполните все необходимые поля: Наименование и Ед.изм", "Информация");
            }
            else 
            {
                var res = MessageBox.Show("Уверены, что хотите сохранить измнения?", "Предупреждение", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    if (SQLClass.EditMaterial(info[0], textBox3.Text,
                        numericUpDown4.Value.ToString(), textBox4.Text,
                        numericUpDown3.Value.ToString(), numericUpDown2.Value.ToString(),
                        textBox1.Text, numericUpDown1.Value.ToString(),
                        textBox2.Text.Replace(@"\", @"\\"), comboBox1.SelectedIndex.ToString())) {
                            MessageBox.Show("Успешное редактирование!", "Информация");

                            ViewMaterialForm form = new ViewMaterialForm();
                            this.Visible = false;
                            form.ShowDialog();
                    }
                    
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Уверены, что хотите удалить материал?", "Предупреждение", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                if (SQLClass.DeleteMaterial(info[0]))
                {
                    MessageBox.Show("Успешное удаление!", "Информация");
                    ViewMaterialForm form = new ViewMaterialForm();
                    this.Visible = false;
                    form.ShowDialog();
                }

            }
        }
    }
}
