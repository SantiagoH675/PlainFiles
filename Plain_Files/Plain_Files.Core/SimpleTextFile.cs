using System;
using System.IO;

namespace Plain_Files.Core;

public class SimpleTextFile
{
    private readonly string _path;

    public SimpleTextFile(string path)
    {
        this._path = path;
    }

    public void WriAllLines(string[] lines)
    {
        var directory = Path.GetDirectoryName(_path);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if (!File.Exists(_path))
        {
            using (File.Create(_path)) { }
        }

        File.WriteAllLines(_path, lines);
    }

    public string[] ReadAllLines()
    {
        if (!File.Exists(_path))
        {
            var directory = Path.GetDirectoryName(_path);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (File.Create(_path)) { }
            return Array.Empty<string>();
        }

        return File.ReadAllLines(_path);
    }
}