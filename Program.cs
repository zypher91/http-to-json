using System.Xml;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args) {
        if (args.Length != 0) {
            foreach (string arg in args) {
                Console.WriteLine (arg);
            }

            Program p = new Program();
            Task t = p.GetHttpResponseAndWrite(args[0]);
            t.Wait();
            Console.ReadKey();
        }
    }

    async Task GetHttpResponseAndWrite(string arg) {
        HttpClient httpClient = new HttpClient();
            
        HttpResponseMessage response = await httpClient.GetAsync(arg);

        string result = await response.Content.ReadAsStringAsync();

        WriteJsonToFile(ToJsonFromXml(result));
    }

    string ToJsonFromXml(string response) {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(response);

        string json = JsonConvert.SerializeXmlNode(xmlDocument);

        return json;
    }

    void WriteJsonToFile(string json) {
        using (StreamWriter sw = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "json.txt"))) {
            sw.WriteLine(json);
        }
    }
}