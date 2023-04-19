using Newtonsoft.Json;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Nazwa i miejsce do zapisu
            string name = "Jan Kowalski";
            string location = "Warszawa, Polska";

            // Ścieżka do pliku
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "dane.json");

            // Utworzenie obiektu do zapisu
            var data = new { Name = name, Location = location };

            // Zapis do pliku
            using (StreamWriter file = File.AppendText(filePath))
            {
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                serializer.Serialize(file, data);
            }

            // Odczyt ostatniego zapisu z pliku
            using (StreamReader file = File.OpenText(filePath))
            {
                string json = file.ReadToEnd();
                dynamic lastEntry = JsonConvert.DeserializeObject(json);
                Console.WriteLine("Ostatni zapis:");
                Console.WriteLine($"Nazwa: {lastEntry.Name}");
                Console.WriteLine($"Miejsce: {lastEntry.Location}");
            }
        }
    }
}