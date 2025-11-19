using CsvHelper;
using System.Globalization;

namespace PlainFiles.Core;

public class NugetCsvHelper
{
    // Escribe una lista de Person en un archivo CSV.
    public void Write(string path, IEnumerable<Person> people)
    {
        using var sw = new StreamWriter(path);
        using var cw = new CsvWriter(sw, CultureInfo.InvariantCulture);

        cw.WriteRecords(people);
    }

    //Lee un archivo CSV y lo convierte a List<Person>
    public IEnumerable<Person> Read(string path)
    {
        if (!File.Exists(path))
            return Enumerable.Empty<Person>();

        using var sr = new StreamReader(path);
        using var cr = new CsvReader(sr, CultureInfo.InvariantCulture);

        return cr.GetRecords<Person>().ToList();
    }
}