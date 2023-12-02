// Part 1
/*var gamesInput = File.ReadLines("File.txt");

int redCubes = 12;
int greenCubes = 13;
int blueCubes = 14;

int sumOfPossibleGames = 0;

foreach (var gameInput in gamesInput)
{
    string[] parts = gameInput.Split(':');
    int gameId = int.Parse(parts[0].Split(' ')[1]);

    bool isPossibleGame = parts[1].Split(';').All(subset =>
    {
        Dictionary<string, int> cubeCounts = new Dictionary<string, int>
        {
                    { "red", 0 },
                    { "green", 0 },
                    { "blue", 0 }
        };
        foreach (var cube in subset.Split(','))
        {
            string[] cubeInfo = cube.Trim().Split(' ');
            string color = cubeInfo[1].ToLower();
            int count = int.Parse(cubeInfo[0]);
            cubeCounts[color] += count;
        }
        return cubeCounts["red"] <= redCubes
            && cubeCounts["green"] <= greenCubes
            && cubeCounts["blue"] <= blueCubes;
    });

    if (isPossibleGame)
    {
        sumOfPossibleGames += gameId;
    }
}
Console.WriteLine($"Sum of IDs of possible games: {sumOfPossibleGames}");*/

//Part 2
var lines = File.ReadAllLines("File.txt");
var sum = 0;

for (int i = 1; i <= lines.Length; i++)
{
    var line = lines[i - 1];
    var games = line.Split(':')[1].Split(';');
    var numberRedCubes = 0;
    var numberGreenCubes = 0;
    var numberBlueCubes = 0;

    foreach (var game in games)
    {
        var cubes = game.Split(',');
        foreach (var cube in cubes)
        {
            var value = 0;
            int lengthOfNumber = 0;
            int endIndexNumber = 0;
            for (int k = 2, count = 1; k < cube.Length; k++, count++)
            {
                if (!int.TryParse(cube[k].ToString(), out var number2))
                {
                    lengthOfNumber = count;
                    endIndexNumber = k - 1;
                    value = int.Parse(cube.Substring(1, lengthOfNumber));
                    break;
                }
            }
            var cubeColor = cube.Substring(endIndexNumber + 2, 1);
            switch (cubeColor.ToUpper())
            {
                case "R":
                    if (value > numberRedCubes) numberRedCubes = value;
                    break;
                case "G":
                    if (value > numberGreenCubes) numberGreenCubes = value;
                    break;
                case "B":
                    if (value > numberBlueCubes) numberBlueCubes = value;
                    break;
            }
        }
    }
    sum += numberRedCubes * numberGreenCubes * numberBlueCubes;
}
Console.WriteLine(sum);



