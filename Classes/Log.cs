namespace LogNameSpace;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class Log
{
    private static string logFilePath = "logs.json";
    public static void Write(string message)
    {
        string timestamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        LogData LogData = new LogData { Date = timestamp, Message = message };
        List<LogData> logEntries = LoadLogs();
        logEntries.Add(LogData);
        SaveLogs(logEntries);
    }
    public static void DisplayLogs()
    {
        List<LogData> logEntries = LoadLogs();
        foreach (var LogData in logEntries)
        {
            Console.WriteLine($"{LogData.Date}: {LogData.Message}");
        }
    }
    private static List<LogData> LoadLogs()
    {
        if (File.Exists(logFilePath))
        {
            string json = File.ReadAllText(logFilePath);
            return JsonSerializer.Deserialize<List<LogData>>(json);
        }
        else
            return new List<LogData>();
    }

    private static void SaveLogs(List<LogData> logEntries)
    {
        string json = JsonSerializer.Serialize(logEntries);
        File.WriteAllText(logFilePath, json);
    }
}

public class LogData
{
    public string Date { get; set; }
    public string Message { get; set; }
}
