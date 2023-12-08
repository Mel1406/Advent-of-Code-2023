Part1("File.txt");
Part2("File.txt");
void Part1(string filename)
{

    var instructions = File.ReadAllLines(filename)[0];
    var nodeLines = File.ReadAllText(filename).Split("\r\n\r\n")[1].Split("\r\n").Where(i => i != "").Select(i => i).ToList();
    var nodes = new List<Node>();
    foreach (var line in nodeLines)
    {
        var split = line.Split("=");
        var name = split[0].Trim();
        var newNodes = split[1].Replace("(", "").Replace(")", "").Trim().Split(", ");
        var left = newNodes[0];
        var right = newNodes[1];
        var node = new Node(name, left, right);
        nodes.Add(node);
    }
    var currentNode = new Node("", "", "");
    foreach (var node in nodes)
    {
        if (node.Name == "AAA")
        {
            currentNode = node;
            break;
        }
    }
    var steps = 0;
    var currentInstructions = instructions.Select(x => x).ToList();
    while (currentNode.Name != "ZZZ")
    {
        if (currentInstructions[0] == 'R')
        {
            currentNode = nodes.Where(x => x.Name == currentNode.Right).First();
        }
        else if (currentInstructions[0] == 'L')
        {
            currentNode = nodes.Where(x => x.Name == currentNode.Left).First();
        }
        currentInstructions.RemoveAt(0);
        if (currentInstructions.Count == 0)
        {
            currentInstructions = instructions.Select(x => x).ToList();
        }
        steps++;
    }
    Console.WriteLine(steps);
}
void Part2(string filename)
{

    var instructions = File.ReadAllLines(filename)[0];
    var nodeLines = File.ReadAllText(filename).Split("\r\n\r\n")[1].Split("\r\n").Where(i => i != "").Select(i => i).ToList();
    var nodes = new List<Node>();
    foreach (var line in nodeLines)
    {
        var split = line.Split("=");
        var name = split[0].Trim();
        var newNodes = split[1].Replace("(", "").Replace(")", "").Trim().Split(", ");
        var left = newNodes[0];
        var right = newNodes[1];
        var node = new Node(name, left, right);
        nodes.Add(node);
    }
    var currentNodes =  new List<Node>();
    foreach (var node in nodes)
    {
        if (node.Name.EndsWith("A"))
        {
            currentNodes.Add(node);
        }
    }
    var steps = 0;
    var currentInstructions = instructions.Select(x => x).ToList();
    var firstZs = new long[currentNodes.Count];
    while (currentNodes.Where(x => x.Name.EndsWith("Z")).Select(x => x).Count() != currentNodes.Count())
    {
        for (int i = 0; i < currentNodes.Count(); i++)
        {
            if(firstZs[i] != 0) {continue;}
            var currentNode = currentNodes[i];
            if (currentInstructions[0] == 'R')
            {
                currentNode = nodes.Where(x => x.Name == currentNode.Right).First();
            }
            else if (currentInstructions[0] == 'L')
            {
                currentNode = nodes.Where(x => x.Name == currentNode.Left).First();
            }
            currentNodes.RemoveAt(i);
            currentNodes.Insert(i, currentNode);
            if(currentNodes[i].Name.EndsWith("Z")) {firstZs[i] = steps+1;}
        }
        currentInstructions.RemoveAt(0);
        if (currentInstructions.Count == 0)
        {
            currentInstructions = instructions.Select(x => x).ToList();
        }
        steps++;
    }
    Console.WriteLine(GetSmallesMultiple(firstZs));
}
static long GetSmallesMultiple(long[] numbers)
{
    var result = numbers[0];
    for (int i = 1; i < numbers.Length; i++)
    {
        result = LeastCommonMultiple(result, numbers[i]);
    }

    return result;
}

static long LeastCommonMultiple(long a, long b) => a * b / GreatestCommonDivisor(a, b);

static long GreatestCommonDivisor(long a, long b)
{
    while (b != 0)
    {
        long temp = b;
        b = a % b;
        a = temp;
    }
    return a;
}

record Node(string Name, string Left, string Right);