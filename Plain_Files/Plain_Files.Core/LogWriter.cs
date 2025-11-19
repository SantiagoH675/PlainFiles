using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plain_Files.Core;

public class LogWriter : IDisposable
{
    private readonly StreamWriter _writer;

    public LogWriter(string path)
    {
        _writer = new StreamWriter(path, append: true)
        {
            AutoFlush = true
        };
    }

    public void WriteLog(string level, string message)
    {
        var timestamp = DateTime.Now.ToString("s");
        _writer.WriteLine($"[{timestamp}] - [{level}]-[{message}]");
    }

    public void Dispose()
    {
        _writer?.Dispose();
    }
}