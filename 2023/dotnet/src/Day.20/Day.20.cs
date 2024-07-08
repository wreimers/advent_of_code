using System.Reflection;

namespace Day20
{
    internal class Program
    {
        private static string DATA_FILE = "var/day_20/sample2.txt";
        private static int BUTTON_PUSHES = 1000;

        static void Main(string[] args)
        {
            var moduleDict = new Dictionary<string, CommunicationsModule>();
            var pulseQueue = new Queue<Pulse>();
            var conjunctionModules = new List<CommunicationsModule>();
            int lowPulsesSent = 0;
            int highPulsesSent = 0;
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
                    moduleDict[name] = b;
                }
                else if (moduleName.StartsWith('%'))
                {
                    var name = moduleName.Substring(1);
                    b = new CommunicationsModule
                    {
                        name = name,
                        type = ModuleType.FlipFlop,
                    };
                    moduleDict[name] = b;
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
                // moduleDict[b.name] = b;
                // Console.WriteLine(b);
            }
            // find all input modules for each conjunction module
            foreach (CommunicationsModule m in conjunctionModules)
            {
                foreach ((string key, CommunicationsModule m2) in moduleDict)
                {
                    if (m2.downstreamModules.Contains(m.name))
                    {
                        m.priorPulses[key] = PulseFrequency.Low;
                    }
                }
            }
            // process queue until done
            for (int push = 0; push < BUTTON_PUSHES; push += 1)
            {
                CommunicationsModule b = moduleDict["broadcaster"];
                pulseQueue.Enqueue(new Pulse
                {
                    sourceModule = b,
                    destinationModule = b,
                    frequency = PulseFrequency.Low,
                });
                while (pulseQueue.Any())
                {
                    Pulse pulse = pulseQueue.Dequeue();
                    if (pulse.frequency == PulseFrequency.High) { highPulsesSent += 1; }
                    if (pulse.frequency == PulseFrequency.Low) { lowPulsesSent += 1; }
                    Console.WriteLine(pulse);
                    var m = pulse.destinationModule;
                    m.SendPulse(pulse, moduleDict, pulseQueue);
                }
            }
            Console.WriteLine($"lowPulsesSent:{lowPulsesSent}");
            Console.WriteLine($"highPulsesSent:{highPulsesSent}");
            Console.WriteLine($"product:{highPulsesSent * lowPulsesSent}");
        }
    }
}

public class CommunicationsModule
{
    public required string name { get; set; }
    public required ModuleType type { get; set; }
    public bool on = false;
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
            case ModuleType.Conjunction:
                Console.WriteLine($"Conjunction pulse:{p}");
                priorPulses[p.sourceModule.name] = p.frequency;
                if (allPriorPulsesAreHigh())
                {
                    sendPulseDownstream(PulseFrequency.Low, q, d);
                }
                else
                {
                    sendPulseDownstream(PulseFrequency.High, q, d);
                }
                break;
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
            try
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
            catch (KeyNotFoundException)
            {
                continue;
            }
        }
    }
    public bool allPriorPulsesAreHigh()
    {
        // Console.WriteLine($"allPriorPulsesAreHigh");
        foreach ((string name, PulseFrequency frequency) in priorPulses)
        {
            Console.WriteLine($"allPriorPulsesAreHigh name:{name} frequency:{frequency}");
            if (frequency == PulseFrequency.Low)
            {
                return false;
            }
        }
        return true;
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