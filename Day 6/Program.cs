const string FILE_PATH = "File.txt";
Part1(FILE_PATH);
Part2(FILE_PATH);

void Part1(string filename)
{
    var lines = File.ReadAllLines(filename);
    var times = lines[0].Split(":")[1].Trim().Split(" ").Where(x => x != "").Select(x => int.Parse(x)).ToList();
    var distances = lines[1].Split(":")[1].Trim().Split(" ").Where(x => x != "").Select(x => int.Parse(x)).ToList();
    var races = new List<Race>();
    var sum = 1;
    for (int i = 0; i < distances.Count; i++)
    {
        var buttonHeld = CalculateButtonHeld(times[i], distances[i]);
        races.Add(new Race(times[i], distances[i], buttonHeld, CalculateBetterRaces(buttonHeld)));
    }
    var numberFurtherRaces = races.Select(r => r.FurtherRaces).ToList();
    foreach (var r in numberFurtherRaces)
    {
        sum *= r;
    }
    Console.WriteLine(sum);
}

void Part2(string filename)
{
    var lines = File.ReadAllLines(filename);
    var time = long.Parse(lines[0].Split(":")[1].Replace(" ", ""));
    var distance = long.Parse(lines[1].Split(":")[1].Replace(" ", ""));

    var buttonHeld = CalculateButtonHeld(time, distance);
    var race = new Race(time, distance, buttonHeld, CalculateBetterRaces(buttonHeld));

    Console.WriteLine(race.FurtherRaces);
}
(double, double) CalculateButtonHeld(long time, long distance)
{
    var determinant = Math.Sqrt(Math.Pow(time, 2) / 4 - distance);
    var option1 = ((double)time / 2) + determinant;
    var option2 = ((double)time / 2) - determinant;
    return (option1, option2);
}
int CalculateBetterRaces((double, double) ButtonHeld)
{
    var smallerTime = Math.Min(ButtonHeld.Item1, ButtonHeld.Item2);
    if (int.TryParse(smallerTime.ToString(), out int number))
    {
        smallerTime++;
    }
    var biggerTime = Math.Max(ButtonHeld.Item1, ButtonHeld.Item2);
    if (int.TryParse(biggerTime.ToString(), out number))
    {
        biggerTime--;
    }
    return (int)Math.Floor(biggerTime) - (int)Math.Ceiling(smallerTime) + 1;
}
record Race(long Time, long Distance, (double, double) ButtonHeld, int FurtherRaces);