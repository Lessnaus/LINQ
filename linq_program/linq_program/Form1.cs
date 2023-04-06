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
            
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "linq.csv");
            
            if (File.Exists(filePath))
            {
                DataTable dt = new DataTable();
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length > 1)
                {
                    string[] headers = lines[0].Split(',');
                    int numColumns = headers.Length;
                    for (int i = 0; i < numColumns; i++)
                    {
                        dt.Columns.Add(headers[i]);
                    }
                    for (int i = 1; i <= lines.Length - 1; i++)
                    {
                        string[] lineData = lines[i].Split(',');
                        DataRow row = dt.NewRow();
                        for (int j = 0; j < numColumns; j++)
                        {
                            row[j] = lineData[j];
                        }
                        dt.Rows.Add(row);
                    }
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
