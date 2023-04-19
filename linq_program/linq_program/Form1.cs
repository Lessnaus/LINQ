using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
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
            string czynnosc = "Sortowanie";

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
            var wynik = new Wynik
            {
                Czynnosc = "sortowanie",
                Wedlug = "firstname",
                Jak = "alfabetycznie",
                Wyniki = dataGridView1.Rows.Cast<DataGridViewRow>()
        .Where(r => !r.IsNewRow)
        .OrderBy(r => r.Cells["firstname"].Value.ToString())
        .Select(r => new Dane
        {
            id = Convert.ToInt32(r.Cells["id"].Value),
            firstname = r.Cells["firstname"].Value.ToString(),
            lastname = r.Cells["lastname"].Value.ToString(),
            email = r.Cells["email"].Value.ToString(),
            email2 = r.Cells["email2"].Value.ToString(),
            profession = r.Cells["profession"].Value.ToString()
        })
        .ToList()
            };

            // Serializacja do formatu JSON
            string json = JsonConvert.SerializeObject(wynik, Formatting.Indented);

            // Dodanie wyniku do pliku
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wyniki.json");
            File.AppendAllText(filePath, json + Environment.NewLine);



            try
            {
                string filePath = "dane.json";
                string jsonString = File.ReadAllText(filePath);
                JsonDocument jsonDocument = JsonDocument.Parse(jsonString);

                // Pobranie tablicy Wyniki z pliku JSON
                JsonElement wynikiElement = jsonDocument.RootElement.GetProperty("Wyniki");
                if (wynikiElement.ValueKind != JsonValueKind.Array)
                {
                    throw new Exception("Błąd formatu pliku JSON");
                }

                // Pobranie ostatniego wpisu z tablicy Wyniki
                JsonElement lastElement = wynikiElement.EnumerateArray().LastOrDefault();

                // Wyświetlenie danych z ostatniego wpisu w kontrolce Label
                lblLastResult.Text = $"id: {lastElement.GetProperty("id").GetInt32()}, " +
                    $"firstname: {lastElement.GetProperty("firstname").GetString()}, " +
                    $"lastname: {lastElement.GetProperty("lastname").GetString()}, " +
                    $"email: {lastElement.GetProperty("email").GetString()}, " +
                    $"email2: {lastElement.GetProperty("email2").GetString()}, " +
                    $"profession: {lastElement.GetProperty("profession").GetString()}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
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
        private void saveResult(string czynnosc, string wedlug, string jak, string result)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "linq.txt");


            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(czynnosc + ": Wedłóg: " + wedlug + "; Jak: " + jak + "; Wynik: " + result);
            }
            //display the last 5 lines of the file result.txt in label2
            string[] lines = File.ReadAllLines(filePath);
            int count = lines.Length;
            if (count > 5)
            {
                for (int i = count - 5; i < count; i++)
                {
                    label8.Text += lines[i] + Environment.NewLine;
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    label8.Text += lines[i] + Environment.NewLine;
                }
            }

        }
        public class Wynik
        {
            public string Czynnosc { get; set; }
            public string Wedlug { get; set; }
            public string Jak { get; set; }
            public List<Dane> Wyniki { get; set; }
        }

        // Definicja obiektu z danymi z DataGridView
        public class Dane
        {
            public int id { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string email { get; set; }
            public string email2 { get; set; }
            public string profession { get; set; }
        }
    }
}
