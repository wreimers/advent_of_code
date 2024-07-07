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
            var conjunctionModules = new List<CommunicationsModule>();
            Console.WriteLine("Advent of Code 2023 Day 20");
            string? rawLine;
            using StreamReader reader = new(DATA_FILE);
            while ((rawLine = reader.ReadLine()) != null)
            {
                // Console.WriteLine(rawLine);
                char[] splitters = [',', '-', '>', ' '];
                string[] tokens = rawLine.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                // Console.WriteLine(String.Join(", ", tokens));
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
                        sourceModule = b,
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
                    conjunctionModules.Add(b);
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
                // add downstream modules as strings
                for (int t = 1; t < tokens.Length; t += 1)
                {
                    b.downstreamModules.Add(tokens[t]);
                }
                // store module indexed by name string
                moduleDict[b.name] = b;
                // Console.WriteLine(b);
            }
            // find all input modules for each conjunction module
            foreach (CommunicationsModule m in conjunctionModules)
            {
                foreach ((string key, CommunicationsModule m2) in moduleDict)
                {
                    if (m2.downstreamModules.Contains(m.name))
                    {
                        m.inputModules[key] = m2;
                        m.priorPulses[key] = PulseFrequency.Low;
                    }
                }
            }
            // process queue until done
            while (pulseQueue.Any())
            {
                Pulse pulse = pulseQueue.Dequeue();
                Console.WriteLine(pulse);
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
    public Dictionary<string, CommunicationsModule> inputModules = new Dictionary<string, CommunicationsModule>();
    public Dictionary<string, PulseFrequency> priorPulses = new Dictionary<string, PulseFrequency>();
    public List<string> downstreamModules = new List<string>();
    public void SendPulse(Pulse p, Dictionary<string, CommunicationsModule> d, Queue<Pulse> q)
    {
        Console.WriteLine($"  {this}");
        switch (type)
        {
            case ModuleType.Broadcaster:
                sendPulseDownstream(p.frequency, q, d);
                break;
            case ModuleType.Conjunction: break;
            case ModuleType.FlipFlop:
                // high pulse, do nothing
                if (p.frequency == PulseFrequency.High) { break; }
                // low pulse, check internal state
                if (on)
                {
                    // module was on, turn off and send low pulse
                    on = false;
                    sendPulseDownstream(PulseFrequency.Low, q, d);
                }
                else
                {
                    // module was off, turn on and send high pulse
                    on = true;
                    sendPulseDownstream(PulseFrequency.High, q, d);
                }
                break;
            default:
                throw new Exception("CANNOT SEND A PULSE TO AN INVALID MODULE");
        }
    }
    public void sendPulseDownstream(PulseFrequency frequency, Queue<Pulse> queue, Dictionary<string, CommunicationsModule> moduleMap)
    {
        foreach (string module in downstreamModules)
        {
            CommunicationsModule destination = moduleMap[module];
            Pulse p = new Pulse
            {
                sourceModule = this,
                destinationModule = destination,
                frequency = frequency,
            };
            Console.WriteLine($"    enqueue {p}");
            queue.Enqueue(p);
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
    public required CommunicationsModule sourceModule { get; set; }
    public required CommunicationsModule destinationModule { get; set; }
    public required PulseFrequency frequency { get; set; }
    public override string? ToString()
    {
        return $"<pulse destinationModule:{destinationModule.name} frequency:{frequency}>";
    }
}

public enum PulseFrequency
{
    None,
    High,
    Low,
}