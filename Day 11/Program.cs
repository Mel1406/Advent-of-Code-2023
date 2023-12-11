Part1("File.txt");
Part2("File.txt");
void Part1(string filename)
{
    var lines = File.ReadAllLines(filename);
    var galaxies = new List<Galaxy>();
    var emptyRows = GetEmptyRows(lines);
    var emptyCols = GetEmptyCols(lines);

    var expandenGalaxy = lines.ToList();
    var rowEntered = 0;
    foreach (var emptyRow in emptyRows)
    {
        expandenGalaxy.Insert(emptyRow + rowEntered, MakeEmptyRow(expandenGalaxy[0].Length));
        rowEntered++;
    }
    var colEntered = 0;
    foreach (var emptyCol in emptyCols)
    {
        for (int i = 0; i < expandenGalaxy.Count; i++)
        {
            expandenGalaxy[i] = $"{expandenGalaxy[i][..(emptyCol + colEntered + 1)]}.{expandenGalaxy[i][(emptyCol + colEntered + 1)..]}";
        }
        colEntered++;
    }
    for (int i = 0; i < expandenGalaxy.Count; i++)
    {
        for (int j = 0; j < expandenGalaxy[i].Length; j++)
        {
            if (expandenGalaxy[i][j] == '#')
            {
                galaxies.Add(new Galaxy(i, j));
            }
        }
    }
    var sum = 0;
    for (int i = 0; i < galaxies.Count; i++)
    {
        var firstGalaxy = galaxies[i];
        for (int j = i + 1; j < galaxies.Count; j++)
        {
            var secondGalaxy = galaxies[j];
            sum += Math.Abs(firstGalaxy.X - secondGalaxy.X) + Math.Abs(firstGalaxy.Y - secondGalaxy.Y);
        }
    }
    Console.WriteLine(sum);
}
void Part2(string filename)
{
    var lines = File.ReadAllLines(filename);
    var galaxies = new List<Galaxy>();
    var emptyRows = GetEmptyRows(lines);
    var emptyCols = GetEmptyCols(lines);

    var expandenGalaxy = lines.ToList();
    var rowEntered = 0;
    for (int i = 0; i < expandenGalaxy.Count; i++)
    {
        for (int j = 0; j < expandenGalaxy[i].Length; j++)
        {
            if (expandenGalaxy[i][j] == '#')
            {
                galaxies.Add(new Galaxy(i, j));
            }
        }
    }
    foreach (var emptyRow in emptyRows)
    {
        var index = emptyRow + rowEntered;
        
        for(int i = 0; i < galaxies.Count; i++)
        {
            if(galaxies[i].X > index) {galaxies[i].X += 999_999;}
        }
        rowEntered += 999_999;

    }
    var colEntered = 0;
    foreach (var emptyCol in emptyCols)
    {
        var index = emptyCol + colEntered;
        
        for(int i = 0; i < galaxies.Count; i++)
        {
            if(galaxies[i].Y > index) {galaxies[i].Y += 999_999;}
        }
        colEntered += 999_999;
    }
    long sum = 0;
    for (int i = 0; i < galaxies.Count; i++)
    {
        var firstGalaxy = galaxies[i];
        for (int j = i + 1; j < galaxies.Count; j++)
        {
            var secondGalaxy = galaxies[j];
            sum += Math.Abs(firstGalaxy.X - secondGalaxy.X) + Math.Abs(firstGalaxy.Y - secondGalaxy.Y);
        }
    }
    Console.WriteLine(sum);
}
List<int> GetEmptyRows(string[] lines)
{
    var emptyRows = new List<int>();
    for (int i = 0; i < lines.Length; i++)
    {
        if (!lines[i].Contains("#"))
        {
            emptyRows.Add(i);
        }
    }
    return emptyRows;
}
List<int> GetEmptyCols(string[] lines)
{
    var emptyCols = new List<int>();
    for (int i = 0; i < lines[0].Length; i++)
    {
        if (lines[0][i] == '.')
        {
            var emptyCol = true;
            for (int j = 1; j < lines.Length; j++)
            {
                if (lines[j][i] != '.')
                {
                    emptyCol = false;
                    break;
                }
            }
            if (emptyCol)
            {
                emptyCols.Add(i);
            }
        }
    }
    return emptyCols;
}
string MakeEmptyRow(int length)
{
    var output = "";
    for (int i = 0; i < length; i++)
    {
        output += ".";
    }
    return output;
}
public class Galaxy(int x, int y)
{
    public int X {get; set;} = x;
    public int Y {get; set;} = y;
}