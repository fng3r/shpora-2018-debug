using System;
using System.IO;
using System.Net.Sockets;
using Fclp;

namespace TaskManager
{
    public class MetricsCollectorConfig
    {
        public double RefreshPeriodSeconds { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public int Top { get; set; }

    }
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            var fclp = new FluentCommandLineParser<MetricsCollectorConfig>();
            fclp
                .Setup(c => c.RefreshPeriodSeconds)
                .As('s')
                .SetDefault(0.1);
            fclp
                .Setup(c => c.Host)
                .As('h');
            fclp
                .Setup(c => c.Port)
                .As('p');
            fclp
                .Setup(c => c.Top)
                .As('t')
                .SetDefault(20);

            var config = fclp.Parse(args);
            if (config.HasErrors)
                return;
            
            var metricsCollector = new TaskManager();
            var printer = new ConsoleMetricsPrinter();
            var streamWriter = new MetricsStreamWriter(GetStream(fclp.Object));
            metricsCollector.MetricsAvailable += (_, m) => printer.Print(m, fclp.Object.Top);
            metricsCollector.MetricsAvailable += (_, m) => streamWriter.Save(m);
            metricsCollector.Run(TimeSpan.FromSeconds(fclp.Object.RefreshPeriodSeconds));
        }

        private static Stream GetStream(MetricsCollectorConfig config)
        {
            if (string.IsNullOrWhiteSpace(config.Host))
                return Stream.Null;
            var tcp = new TcpClient();
            tcp.Connect(config.Host, config.Port);
            return tcp.GetStream();
        }
    }
}