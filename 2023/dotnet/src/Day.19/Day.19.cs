using System.Text.RegularExpressions;

namespace Day19
{
    internal class Program
    {
        private static string DATA_FILE = "var/day_19/sample.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2023 Day 19");
            var workflows = new List<Workflow>();
            var workflowsDict = new Dictionary<string, Workflow>();
            var parts = new List<Part>();
            char readStatus = 'W';
            string? rawLine;
            using StreamReader reader = new(DATA_FILE);
            while ((rawLine = reader.ReadLine()) != null)
            {
                Console.WriteLine(rawLine);
                if (rawLine == "")
                {
                    readStatus = 'P';
                    continue;
                }
                switch (readStatus)
                {
                    case 'W':
                        char[] splitters = [',', '{', '}',];
                        string[] tokens = rawLine.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                        Console.WriteLine(string.Join(", ", tokens));
                        var workflow = new Workflow
                        {
                            name = tokens[0],
                            defaultResult = tokens[^1],
                        };
                        for (int r = 1; r < tokens.Length - 1; r += 1)
                        {
                            var token = tokens[r];
                            string pattern = @"^([xmas]+)([\<\>]+)(\d+):(\w+)$";
                            Regex rg = new Regex(pattern, RegexOptions.IgnoreCase);
                            Match m = rg.Match(token);
                            if (!m.Success)
                            {
                                throw new Exception("REGEX DIDN'T MATCH");
                            }
                            var category = m.Groups[1].ToString().ToCharArray()[0];
                            var operation = m.Groups[2].ToString().ToCharArray()[0];
                            var value = Int32.Parse(m.Groups[3].ToString());
                            var result = m.Groups[4].ToString();
                            Console.WriteLine($"category:{category} operation:{operation} value:{value} result:{result}");
                            var rule = new Rule
                            {
                                category = category,
                                operation = operation,
                                value = value,
                                result = result,
                            };
                            workflow.rules.Add(rule);
                        }
                        workflows.Add(workflow);
                        workflowsDict[workflow.name] = workflow;
                        break;
                    case 'P':
                        splitters = [',', '{', '}',];
                        tokens = rawLine.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                        Console.WriteLine(string.Join(", ", tokens));
                        var part = new Part();
                        foreach (string token in tokens)
                        {
                            char[] tokenSplitters = ['=',];
                            string[] subTokens = token.Split(tokenSplitters, StringSplitOptions.RemoveEmptyEntries);
                            var category = subTokens[0].ToCharArray()[0];
                            var value = Int32.Parse(subTokens[1]);
                            switch (category)
                            {
                                case 'x':
                                    part.x = value;
                                    break;
                                case 'm':
                                    part.m = value;
                                    break;
                                case 'a':
                                    part.a = value;
                                    break;
                                case 's':
                                    part.s = value;
                                    break;
                                default:
                                    throw new Exception("INVALID CATEGORY FOR PART");
                            }
                        }
                        parts.Add(part);
                        Console.WriteLine(part);
                        break;
                    default:
                        throw new Exception($"INVALID READSTATUS {readStatus}");
                }
            }
            foreach (Part p in parts)
            {
                var currentWorkflow = workflows[0];
                while (p.status != PartStatus.Accepted && p.status != PartStatus.Rejected)
                {
                    currentWorkflow.apply(p);
                    if (p.nextWorkflow is not null)
                    {
                        currentWorkflow = workflowsDict[p.nextWorkflow];
                    }

                }
            }
        }
    }
}

class Workflow
{
    public required string name { get; set; }
    public required string defaultResult = "";
    public List<Rule> rules = new List<Rule>();
    public void apply(Part p)
    {
        string result = "";
        foreach (Rule r in rules)
        {
            Console.WriteLine(r);
            if (r.category == 'x' && r.operation == '<' && p.x < r.value) { result = r.result; }
            else if (r.category == 'x' && r.operation == '>' && p.x > r.value) { result = r.result; }
            else if (r.category == 'm' && r.operation == '<' && p.x < r.value) { result = r.result; }
            else if (r.category == 'm' && r.operation == '>' && p.x > r.value) { result = r.result; }
            else if (r.category == 'a' && r.operation == '<' && p.x < r.value) { result = r.result; }
            else if (r.category == 'a' && r.operation == '>' && p.x > r.value) { result = r.result; }
            else if (r.category == 's' && r.operation == '<' && p.x < r.value) { result = r.result; }
            else if (r.category == 's' && r.operation == '>' && p.x > r.value) { result = r.result; }
            if (result == "A") { p.status = PartStatus.Accepted; break; }
            else if (result == "R") { p.status = PartStatus.Rejected; break; }
        }
        if (result == "")
        {
            p.nextWorkflow = defaultResult;
        }
    }
}

class Rule
{
    public required char category { get; set; }
    public required char operation { get; set; }
    public required int value { get; set; }
    public required string result { get; set; }
    public override string? ToString()
    {
        return $"<rule category:{category} operation:{operation} value:{value} result:{result}>";
    }
}

class Part
{
    public int x { get; set; }
    public int m { get; set; }
    public int a { get; set; }
    public int s { get; set; }
    public PartStatus status = PartStatus.None;
    public string? nextWorkflow = null;
    public override string? ToString()
    {
        return $"<part x:{x} m:{m} a:{a} s:{s} status:{status}>";
    }
}

public enum PartStatus
{
    None,
    Accepted,
    Rejected,
}