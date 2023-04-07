using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;




namespace linq_program
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void letsClick(object sender, MouseEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "linq.csv");

           

            if (File.Exists(filePath))
            {
                DataTable dt = new DataTable();
                var lines = File.ReadLines(filePath).Select(line => line.Split(','));
                var headers = lines.First();
                int numColumns = headers.Length;
                foreach (var header in headers)
                {
                    dt.Columns.Add(header);
                }
                string filter1 = comboBox1.Text;
                string filter2 = comboBox2.Text;
                string filter3 = comboBox3.Text;
                label1.Text = filter1+ filter2;
                var data = lines.Skip(1)
     .Where(fields => (string.IsNullOrEmpty(filter2) || fields[1] == filter2) &&
                      (string.IsNullOrEmpty(filter1) || fields[5] == filter1) &&
                      (string.IsNullOrEmpty(filter3) || fields[2] == filter3));

                foreach (var line in data)
                {
                    DataRow row = dt.NewRow();
                    for (int i = 0; i < numColumns; i++)
                    {
                        row[i] = line[i];
                    }
                    dt.Rows.Add(row);
                }
                dataGridView1.DataSource = dt;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "linq.csv");
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                var headers = lines[0].Split(',');
                for (int i = 0; i < headers.Length; i++)
                {
                    var values = lines.Skip(1).Select(l => l.Split(',')[i]).Distinct().OrderBy(v => v);
                    if (i == 5)
                    {
                        comboBox1.Items.AddRange(values.ToArray());
                    }
                    else if (i == 1)
                    {
                        comboBox2.Items.AddRange(values.ToArray());
                    }
                    else if (i == 2)
                    {
                        comboBox3.Items.AddRange(values.ToArray());
                    }
                    // Dodaj więcej ComboBox-ów dla innych kolumn, jeśli jest to konieczne.
                }
            }
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {


            // Pobierz kolumnę, według której sortować, z combobox1
            string columnName = comboBox4.SelectedItem.ToString();
            string sortType = comboBox5.SelectedItem.ToString();
            // Znajdź kolumnę o podanej nazwie
            DataGridViewColumn column = dataGridView1.Columns
                .Cast<DataGridViewColumn>()
                .FirstOrDefault(col => col.HeaderText == columnName);

            // Sortuj dane w dataGridView1 według wybranej kolumny
            if (sortType == "Alfabetycznie")
            {
                dataGridView1.Sort(column, ListSortDirection.Ascending);
            }
            else
            {
                dataGridView1.Sort(column, ListSortDirection.Descending);

            }
        }

        private void letsClick(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "linq.csv");


            if (File.Exists(filePath))
            {
                DataTable dt = new DataTable();
                var lines = File.ReadLines(filePath).Select(line => line.Split(','));
                var headers = lines.First();
                int numColumns = headers.Length;
                foreach (var header in headers)
                {
                    dt.Columns.Add(header);
                }
                var data = lines.Skip(1);
                foreach (var line in data)
                {
                    DataRow row = dt.NewRow();
                    for (int i = 0; i < numColumns; i++)
                    {
                        row[i] = line[i];
                    }
                    dt.Rows.Add(row);
                }
                dataGridView1.DataSource = dt;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
