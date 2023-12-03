using System.Text.Json;
using System.Xml.Serialization;

public class CompanyService
{
    // Метод для читання файлу JSON
    public Company ReadJsonFile(string filePath)
    {
        var jsonString = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<Company>(jsonString);
    }

    // Метод для читання файлу Xml
    public Company ReadXmlFile(string filePath)
    {
        var serializer = new XmlSerializer(typeof(Company));
        using (var reader = new StreamReader(filePath))
        {
            return (Company)serializer.Deserialize(reader);
        }
    }

    // Метод для читання файлу Ini
    public Company ReadIniFile(string filePath)
    {
        var iniData = File.ReadAllLines(filePath)
            .Select(line => line.Split('='))
            .ToDictionary(parts => parts[0], parts => parts[1]);

        return new Company
        {
            Company_name = iniData["Company_name"],
            Capitalization = iniData["Capitalization"],
            Year_of_foundation = int.Parse(iniData["Year_of_foundation"]),
            Number_of_employees = int.Parse(iniData["Number_of_employees"]),
            Country_of_origin = iniData["Country_of_origin"],
            CEO = iniData["CEO"]
        };
    }

    public Company CompanyWithMostEmployees(params Company[] companies)
    {
        return companies.OrderByDescending(c => c.Number_of_employees).First();
    }
}
