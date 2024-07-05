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
                // Console.WriteLine(rawLine);
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
                        // Console.WriteLine(string.Join(", ", tokens));
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
                            // Console.WriteLine($"category:{category} operation:{operation} value:{value} result:{result}");
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
                        // Console.WriteLine(string.Join(", ", tokens));
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
                        // Console.WriteLine(part);
                        break;
                    default:
                        throw new Exception($"INVALID READSTATUS {readStatus}");
                }
            }
            foreach (Part p in parts)
            {
                Console.WriteLine(p);
                var currentWorkflow = workflowsDict["in"];
                while (p.status != PartStatus.Accepted && p.status != PartStatus.Rejected)
                {
                    Console.WriteLine($"  {currentWorkflow}");
                    currentWorkflow.apply(p);
                    if (p.nextWorkflow is not null)
                    {
                        currentWorkflow = workflowsDict[p.nextWorkflow];
                    }

                }
            }
            int numberOfAcceptedParts = 0;
            foreach (Part p in parts)
            {
                if (p.status == PartStatus.Accepted)
                {
                    numberOfAcceptedParts += 1;
                }
            }
            Console.WriteLine($"numberOfAcceptedParts:{numberOfAcceptedParts}");
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
            Console.WriteLine($"    {r}");
            if (r.category == 'x' && r.operation == '<' && p.x < r.value) { result = r.result; }
            else if (r.category == 'x' && r.operation == '>' && p.x > r.value) { result = r.result; }
            else if (r.category == 'm' && r.operation == '<' && p.m < r.value) { result = r.result; }
            else if (r.category == 'm' && r.operation == '>' && p.m > r.value) { result = r.result; }
            else if (r.category == 'a' && r.operation == '<' && p.a < r.value) { result = r.result; }
            else if (r.category == 'a' && r.operation == '>' && p.a > r.value) { result = r.result; }
            else if (r.category == 's' && r.operation == '<' && p.s < r.value) { result = r.result; }
            else if (r.category == 's' && r.operation == '>' && p.s > r.value) { result = r.result; }
            if (result == "A") { p.status = PartStatus.Accepted; p.nextWorkflow = null; break; }
            else if (result == "R") { p.status = PartStatus.Rejected; p.nextWorkflow = null; break; }
            else if (result != "") { p.status = PartStatus.None; p.nextWorkflow = result; break; }

        }
        if (result == "")
        {
            switch (defaultResult)
            {
                case "A":
                    p.status = PartStatus.Accepted;
                    p.nextWorkflow = null;
                    break;
                case "R":
                    p.status = PartStatus.Rejected;
                    p.nextWorkflow = null;
                    break;
                default:
                    p.nextWorkflow = defaultResult;
                    break;
            }

        }
        Console.WriteLine($"      result:{result} p.nextWorkflow:{p.nextWorkflow}");
    }
    public override string? ToString()
    {
        return $"<workflow name:{name} defaultResult:{defaultResult}>";
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