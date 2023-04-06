using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

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
                var data = lines.Skip(1).Where(fields => fields[5] == "developer");
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
    }
}
