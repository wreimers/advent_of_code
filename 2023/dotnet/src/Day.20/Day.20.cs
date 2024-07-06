using System.Reflection;

namespace Day20
{
    internal class Program
    {
        private static string DATA_FILE = "var/day_20/sample.txt";

        static void Main(string[] args)
        {
            var moduleDict = new Dictionary<string, CommunicationsModule>();
            var pulseQueue = new Queue<Pulse>();
            Console.WriteLine("Advent of Code 2023 Day 20");
            string? rawLine;
            using StreamReader reader = new(DATA_FILE);
            while ((rawLine = reader.ReadLine()) != null)
            {
                Console.WriteLine(rawLine);
                char[] splitters = [',', '-', '>', ' '];
                string[] tokens = rawLine.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine(String.Join(", ", tokens));
                string moduleName = tokens[0];
                CommunicationsModule b = new CommunicationsModule
                {
                    name = "",
                    type = ModuleType.None,
                };
                if (moduleName == "broadcaster")
                {
                    b = new CommunicationsModule
                    {
                        name = moduleName,
                        type = ModuleType.Broadcaster,
                    };
                    moduleDict[moduleName] = b;
                    pulseQueue.Enqueue(new Pulse
                    {
                        destinationModule = b,
                        frequency = PulseFrequency.Low,
                    });
                }
                else if (moduleName.StartsWith('&'))
                {
                    var name = moduleName.Substring(1);
                    b = new CommunicationsModule
                    {
                        name = name,
                        type = ModuleType.Conjunction,
                    };
                    moduleDict[moduleName] = b;
                }
                else if (moduleName.StartsWith('%'))
                {
                    var name = moduleName.Substring(1);
                    b = new CommunicationsModule
                    {
                        name = name,
                        type = ModuleType.FlipFlop,
                    };
                    moduleDict[moduleName] = b;
                }
                else
                {
                    throw new Exception("UNHANDLED MODULE TYPE");
                }
                for (int t = 1; t < tokens.Length; t += 1)
                {
                    b.downstreamModules.Add(tokens[t]);
                }
                moduleDict[b.name] = b;
                Console.WriteLine(b);
            }
            while (pulseQueue.Any())
            {
                Pulse pulse = pulseQueue.Dequeue();
                var m = pulse.destinationModule;
                m.SendPulse(pulse, moduleDict, pulseQueue);

            }
        }
    }
}

public class CommunicationsModule
{
    public required string name { get; set; }
    public required ModuleType type { get; set; }
    public bool on = false;
    public List<string> downstreamModules = new List<string>();
    public void SendPulse(Pulse p, Dictionary<string, CommunicationsModule> d, Queue<Pulse> q)
    {
        switch (type)
        {
            case ModuleType.Broadcaster:
                foreach (string m in downstreamModules)
                {
                    q.Enqueue(new Pulse
                    {
                        destinationModule = d[m],
                        frequency = p.frequency,
                    });
                }
                break;

            default:
                throw new Exception("CANNOT SEND A PULSE TO AN INVALID MODULE");
        }
    }
    public override string? ToString()
    {
        return $"<module name:{name} type:{type} downstreamModules:{String.Join(", ", downstreamModules)}>";
    }
}

public enum ModuleType
{
    None,
    Broadcaster,
    Conjunction,
    FlipFlop,

}

public class Pulse
{
    public required CommunicationsModule destinationModule { get; set; }
    public required PulseFrequency frequency { get; set; }
    public void Send()
    {

    }
}

public enum PulseFrequency
{
    None,
    High,
    Low,
}