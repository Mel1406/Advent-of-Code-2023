/*// Part 1
var sum = 0;
var lines = File.ReadAllLines("file.txt");

foreach (var line in lines)
{
    var firstNum = 0;
    var secondNum = 0;
    foreach (char c in line)
    {
        if(int.TryParse(c.ToString(), out var num))
        {
            if (firstNum == 0)
            {
                firstNum = num;
            }
            else
            {
                secondNum = num;
            }
        }
        if(secondNum == 0)
        {
            secondNum = firstNum;
        }
    }
    sum += int.Parse($"{firstNum.ToString()}{secondNum.ToString()}");
}

Console.WriteLine(sum);*/

// Part 2
var sum = 0;
var lines = File.ReadAllLines("file.txt");

for (int k = 0; k < lines.Length; k++)
{
    var line = lines[k];
    var firstNum = 0;
    var secondNum = 0;
    for (int i = 0; i < line.Length;)
    {
        var c = line[0];
        if (int.TryParse(c.ToString(), out var num))
        {
            if (firstNum == 0)
            {
                firstNum = num;
            }
            else
            {
                secondNum = num;
            }
        }
        else if (ValidDigit(line, out int number))
        {
            if (firstNum == 0)
            {
                firstNum = number;
            }
            else
            {
                secondNum = number;
            }
        }
        line = line[1..];
    }
    if (secondNum == 0)
    {
        secondNum = firstNum;
    }
    sum += int.Parse($"{firstNum.ToString()}{secondNum.ToString()}");
}

Console.WriteLine(sum);

bool ValidDigit(string input, out int number)
{
    var validDigits = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
    for (int i = 0; i < validDigits.Length; i++)
    {
        if (input.StartsWith(validDigits[i]))
        {
            number = i + 1;
            return true;
        }
    }
    number = 0;
    return false;
}