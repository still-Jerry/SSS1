using System;
using System.Drawing;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
namespace _Большая_пачка_.Modules
{
    /// <param>
    /// Класс работы с Sql запросами и бд
    /// </param>
    class SQLClass
    {
        public static MySqlConnection Connect() {
            try
            {
                string con = "host=localhost; uid=root; pwd=root; database=newschema;";
                MySqlConnection ConnectionCon = new MySqlConnection(con);
                ConnectionCon.Open();
                return ConnectionCon;
            }
            catch {
                MessageBox.Show("Ошибка подключения к бд", "Ошибка");
                return null;
            }
        }
        public static void GiveMaterialTable(DataGridView dg, string filterm, string order, string text) {
            try
            {
                dg.Columns.Clear();

                string cmd = "SELECT * FROM newschema.материалы ";
                
                if (filterm != "Все типы")
                {
                    cmd += " where `Тип материала` = '" + filterm+"'";
                    cmd += " and (`Наименование` like '" + text + "%' or `Описание` like '" + text + "%')";

                }
                else {
                    cmd += " where (`Наименование` like '" + text + "%' or `Описание` like '" + text + "%')";
                }
                switch (order){
                    case ("по возрастанию наименования"):
                        cmd += "order by `Наименование` asc;";
                        break;
                    case ("по убыванию наименования"):
                        cmd += "order by `Наименование` desc;";
                        break;
                    case ("по возрастанию стоимости"):
                        cmd += "order by `Цена` asc;";
                        break;
                    case ("по убыванию стоимости"):
                        cmd += "order by `Цена` desc;";
                        break;
                    case ("по возрастанию остатка на складе"):
                        cmd += "order by `Количество на складе` asc;";
                        break;
                    case ("по убыванию остатка на складе"):
                        cmd += "order by `Количество на складе` desc;";
                        break;
                }
                MySqlCommand Command = new MySqlCommand(cmd, Connect());
                MySqlDataAdapter adapt = new MySqlDataAdapter(Command);
                DataTable table = new DataTable();
                adapt.Fill(table);
                Command.ExecuteNonQuery();
                Command.Connection.Close();
                dg.DataSource = table;

                dg.Columns[0].Visible=false;

                SelectImage(dg, filterm, order, text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
               
            }
        }
        public static void SelectImage(DataGridView dg, string filterm, string order, string text)
        {
            try
            {
                string cmd = "SELECT * FROM newschema.материалы ";

                if (filterm != "Все типы")
                {
                    cmd += " where `Тип материала` = '" + filterm + "'";
                    cmd += " and (`Наименование` like '" + text + "%' or `Описание` like '" + text + "%')";

                }
                else
                {
                    cmd += " where (`Наименование` like '" + text + "%' or `Описание` like '" + text + "%')";
                }
                switch (order)
                {
                    case ("по возрастанию наименования"):
                        cmd += "order by `Наименование` asc;";
                        break;
                    case ("по убыванию наименования"):
                        cmd += "order by `Наименование` desc;";
                        break;
                    case ("по возрастанию стоимости"):
                        cmd += "order by `Цена` asc;";
                        break;
                    case ("по убыванию стоимости"):
                        cmd += "order by `Цена` desc;";
                        break;
                    case ("по возрастанию остатка на складе"):
                        cmd += "order by `Количество на складе` asc;";
                        break;
                    case ("по убыванию остатка на складе"):
                        cmd += "order by `Количество на складе` desc;";
                        break;
                }
                MySqlCommand Command = new MySqlCommand(cmd, Connect());

                DataGridViewImageColumn imagecol = new DataGridViewImageColumn();
                imagecol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                dg.Columns.Add(imagecol);

                MySqlDataReader reader = Command.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    var path = AppDomain.CurrentDomain.BaseDirectory + "\\Res";

                    if (reader[8].ToString() == "")
                    {
                        
                        dg.Rows[i].Cells[dg.Columns.Count - 1].Value = Image.FromFile(path + "\\materials\\picture.png");
                      
                    }
                    else
                    {
                        dg.Rows[i].Cells[dg.Columns.Count - 1].Value = Image.FromFile(path + reader[8].ToString());
                      
                    }
                    i++;
                }
                Command.Connection.Close();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Ошибка");
            
            }
        }
        public static void GiveMaterialTipe(ComboBox cb)
        {
            try
            {
                cb.Items.Clear();
                string cmd = "SELECT * FROM newschema.materialtype;";
                MySqlCommand Command = new MySqlCommand(cmd, Connect());
                MySqlDataReader reader = Command.ExecuteReader();
                cb.Items.Add("Все типы");
                while (reader.Read()) {
                    cb.Items.Add(reader[1].ToString());
                }
                cb.SelectedIndex=0;
                Command.Connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");

            }
        }
        public static void GiveMatInofo(List<string> info, string id)
        {
            try
            {
                info.Clear();
                string cmd = "SELECT * FROM newschema.материалы where ID=" + id;
                MySqlCommand Command = new MySqlCommand(cmd, Connect());
                MySqlDataReader reader = Command.ExecuteReader();
                while (reader.Read()) {
                    info.Add(reader[0].ToString());
                    info.Add(reader[1].ToString());
                    info.Add(reader[2].ToString());
                    info.Add(reader[3].ToString());
                    info.Add(reader[4].ToString());
                    info.Add(reader[5].ToString());
                    info.Add(reader[6].ToString());
                    info.Add(reader[7].ToString());
                    info.Add(reader[8].ToString());
                    info.Add(reader[9].ToString());

                }
                Command.Connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");

            }
    }
        public static bool EditMaterial(string id, string name, string counp, string uinit, 
                     string counstock, string mincost, string dis, string cost, string pict, string matirtype)
        {
            try
            {

                string cmd = "UPDATE `newschema`.`material` SET `Title` = '" + name +
                    "', `CountInPack` = '" + counp + "', `Unit` = '" + uinit +
                    "', `CountInStock` = '" + counstock + "', `MinCount` = '" + mincost +
                    "', `Description` = '" + dis + "', `Cost` = '" + cost.Replace(",",".") +
                    "', `Image` = '" + pict + "', `MaterialTypeID` = " + (Convert.ToInt32(matirtype)+1) + " WHERE (`ID` = '" + id + "');";
                MySqlCommand Command = new MySqlCommand(cmd, Connect());
                Command.ExecuteNonQuery();
                
                Command.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                return false;
            }
        }
        public static bool DeleteMaterial(string id)
        {
            try
            {

                string cmd = "DELETE FROM `newschema`.`material` WHERE (`ID` = '" + id + "');";
                MySqlCommand Command = new MySqlCommand(cmd, Connect());
                Command.ExecuteNonQuery();

                Command.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                return false;
            }
        }
    }
}
