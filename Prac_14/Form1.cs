using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using MaterialSkin.Controls;
using MaterialSkin;

namespace Prac_14
{
    public partial class Sale : MaterialForm
    {
        string connectionString = @"Data Source=PC310-4;Initial Catalog=Towari;Integrated Security=True;Pooling=False";

        SqlConnection connection1 = new SqlConnection(@"Data Source=PC310-4;Initial Catalog=Towari;Integrated Security=True;Pooling=False");

        public Sale()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Purple500, Primary.Purple800, Primary.Purple800, Accent.Yellow100, TextShade.WHITE);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Наименование FROM Канцтовары";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString(0)); // здесь 0 - номер столбца, который мы хотим добавить в ComboBox
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.ReadOnly = true;
            textBox5.ReadOnly = true;

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > 0)
            {
                connection1.Open();
                SqlCommand com = new SqlCommand($"select [Цена] from Канцтовары where [Наименование] = '{comboBox1.Text}'", connection1);
                int price = Convert.ToInt32(com.ExecuteScalar());
                connection1.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (textBox1.Text.Length > 0)
            {
                connection1.Open();
                SqlCommand com = new SqlCommand($"select [Цена] from Канцтовары where [Наименование] = '{comboBox1.Text}'", connection1);
                int price = Convert.ToInt32(com.ExecuteScalar());
                connection1.Close();
                int thing = Convert.ToInt32(textBox1.Text);
                textBox2.Text = (price * thing).ToString();
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double price = Convert.ToInt32(textBox2.Text);
            int thing = Convert.ToInt32(textBox1.Text);
            double dengi = Convert.ToInt32(textBox3.Text);
            double sale = Convert.ToInt32(textBox4.Text);
            double skidka = price * (sale / 100);
            double sdacha = dengi - (price - skidka);
            textBox5.Text = sdacha.ToString();

        }
    }
}

