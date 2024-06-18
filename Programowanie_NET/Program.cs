using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Timers;

namespace Programowanie_NET
{
     [Serializable]
    public class AppConfig
    {
        public double MemoryThreshold { get; set; }
        public double CpuThreshold { get; set; }
        public int MonitoringInterval { get; set; } // in milliseconds
    }

    static class Program
    {
        private static readonly PerformanceCounter CpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private static readonly PerformanceCounter MemoryCounter = new PerformanceCounter("Memory", "Available MBytes");
        private static AppConfig _config = new AppConfig();
        private static readonly Timer Timer = new Timer();

        static void Main()
        {
            LoadConfig();
            ConfigureTimer();
            Console.WriteLine("System Monitoring Tool is running...");
            Console.WriteLine("Press [Enter] to exit.");
            Console.ReadLine();
        }

        private static void ConfigureTimer()
        {
            Timer.Interval = _config.MonitoringInterval;
            Timer.Elapsed += MonitorSystem;
            Timer.Start();
        }

        private static void MonitorSystem(object sender, ElapsedEventArgs e)
        {
            float cpuUsage = CpuCounter.NextValue();
            float availableMemory = MemoryCounter.NextValue();

            string logEntry = $"{DateTime.Now}: CPU Usage: {cpuUsage}% - Available Memory: {availableMemory}MB";
            Console.WriteLine(logEntry);
            LogToFile(logEntry);

            if (cpuUsage > _config.CpuThreshold)
            {
                LogEvent("CPU usage exceeded threshold.");
            }

            if (availableMemory < _config.MemoryThreshold)
            {
                LogEvent("Available memory below threshold.");
            }
        }

        private static void LogToFile(string logEntry)
        {
            using (StreamWriter sw = new StreamWriter("system_log.txt", true))
            {
                sw.WriteLine(logEntry);
            }
        }

        private static void LogEvent(string message)
        {
            EventLog eventLog = new EventLog();
            if (!EventLog.SourceExists("SystemMonitoringTool"))
            {
                EventLog.CreateEventSource("SystemMonitoringTool", "Application");
            }
            eventLog.Source = "SystemMonitoringTool";
            eventLog.WriteEntry(message, EventLogEntryType.Warning);
        }

        private static void LoadConfig()
        {
            if (File.Exists("config.bin"))
            {
                using (FileStream fs = new FileStream("config.bin", FileMode.Open))
                {
                    IFormatter formatter = new BinaryFormatter();
                    _config = (AppConfig)formatter.Deserialize(fs);
                }
            }
            else
            {
                // Default configuration
                _config.CpuThreshold = 75.0;
                _config.MemoryThreshold = 100.0;
                _config.MonitoringInterval = 5000;
                SaveConfig();
            }
        }

        private static void SaveConfig()
        {
            using (FileStream fs = new FileStream("config.bin", FileMode.Create))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, _config);
            }
        }
    }
}