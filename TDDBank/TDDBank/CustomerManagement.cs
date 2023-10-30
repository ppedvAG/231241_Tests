using System.Globalization;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace TDDBank
{
    public class CustomerManagement
    {
        public IEnumerable<Customer> GetFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                // Return an empty collection if the file does not exist
                return Array.Empty<Customer>();
            }

            string jsonString = File.ReadAllText(filename);
            var customers = JsonSerializer.Deserialize<IEnumerable<Customer>>(jsonString);

            return customers;
        }

        public void SaveToFile(string filename, IEnumerable<Customer> customers)
        {
            string jsonString = JsonSerializer.Serialize(customers, new JsonSerializerOptions {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true });
            File.WriteAllText(filename, jsonString);
        }

    }

    public class Customer
    {
        public required string Name { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }
    }
}
