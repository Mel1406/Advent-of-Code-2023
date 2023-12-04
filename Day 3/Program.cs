//Part 1
/*var lines = File.ReadAllLines("File.txt");
var sum = 0;
var symbols = GetSymbol(lines);
for (int i = 0; i < lines.Length; i++)
{
    var line = lines[i];
    for (int j = 0; j < line.Length; j++)
    {
        var c = line[j];
        if (int.TryParse(c.ToString(), out int number))
        {
            sum += GetNumber(lines, i, j, ref j, symbols);
        }
    }
}
Console.WriteLine(sum);

List<char> GetSymbol(string[] lines)
{
    var list = new List<char>();
    foreach (var line in lines)
    {
        foreach (var c in line)
        {
            if (!int.TryParse(c.ToString(), out int number) && c != '.' && !list.Contains(c))
            {
                list.Add(c);
            }
        }
    }
    return list;
}

bool CheckForNumberValid(string[] lines, int x, int y, int length, List<char> symbols)
{
    for (int k = 0; k < length; k++)
    {
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (i >= 0 && i < lines.Length && j >= 0 && j < lines[i].Length)
                {
                    if (symbols.Contains(lines[i][j]))
                    {
                        return true;
                    }
                }
            }
        }
        y += 1;
    }
    return false;
}

int GetNumber(string[] lines, int x, int y, ref int j, List<char> symbols)
{
    var startIndex = y;
    var endIndex = 0;
    for (int i = y; i < lines.Length; i++)
    {
        if (int.TryParse(lines[x][i].ToString(), out int number))
        {
            endIndex = i;
        }
        else
        {
            break;
        }
    }

    var wholeNum = int.Parse(lines[x].Substring(startIndex, endIndex - startIndex + 1));
    j += endIndex - startIndex;
    return CheckForNumberValid(lines, x, startIndex, endIndex - startIndex + 1, symbols) ? wholeNum : 0;
}*/
//Part 2
var lines = File.ReadAllLines("input.txt");
long sum = 0;
for (int i = 0; i < lines.Length; i++)
{
    var line = lines[i];
    for (int j = 0; j < line.Length; j++)
    {
        var c = line[j];
        if (c == '*')
        {
            sum += (long)GetNumbersAround(lines, i, j);
        }
    }
}
Console.WriteLine(sum);

int GetNumbersAround(string[] lines, int x, int y)
{
    var numbers = 0;
    var numbersFound = 0;
    for (int i = x - 1; i <= x + 1; i++)
    {
        for (int j = y - 1; j <= y + 1; j++)
        {
            if (i >= 0 && i < lines.Length && j >= 0 && j < lines[i].Length)
            {
                if (int.TryParse(lines[i][j].ToString(), out int number))
                {
                    var num = GetNumber(lines, i, j, ref j, x, y);
                    numbersFound++;
                    numbers = numbersFound == 1 ? num : numbers * num;
                }
            }
        }
    }
    if (numbersFound == 2)
    {
        return numbers;
    }
    return 0;
}

int GetNumber(string[] lines, int x, int y, ref int j, int xSymbol, int ySymbol)
{
    var startIndex = 0;
    var endIndex = 0;
    for (int i = y; i >= 0; i--)
    {
        if (int.TryParse(lines[x][i].ToString(), out int number))
        {
            startIndex = i;
        }
        else
        {
            break;
        }
    }
    for (int i = y; i < lines[x].Length; i++)
    {
        if (int.TryParse(lines[x][i].ToString(), out int number))
        {
            endIndex = i;
        }
        else
        {
            break;
        }
    }
    var wholeNum = int.Parse(lines[x].Substring(startIndex, endIndex - startIndex + 1));
    if ((x != xSymbol && j == ySymbol - 1 && endIndex == ySymbol + 1) || x != xSymbol && j != ySymbol - 1)
    { j += endIndex - startIndex; }
    else if (j == ySymbol - 1 && x != xSymbol)
    { j++; }
    return wholeNum;
}