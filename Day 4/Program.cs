
var lines = File.ReadAllLines("File.txt");
Console.WriteLine(Part1(lines));
Console.WriteLine(Part2(lines));

int Part1(string[] lines)
{
    var sum = 0;
    Card[] myCards = new Card[lines.Length];
    for (int i = 0; i < lines.Length; i++)
    {
        myCards[i] = new Card(lines[i]);
    }
    for (int i = 0; i < lines.Length; i++)
    {
        var myCard = myCards[i];
        myCard.GetValue(true);
    }
    foreach (var card in myCards)
    { sum += card.Value; }
    return sum;
}
int Part2(string[] lines)
{
    var sum = 0;
    Card[] myCards = new Card[lines.Length];
    for (int i = 0; i < lines.Length; i++)
    {
        myCards[i] = new Card(lines[i]);
    }
    for (int i = 0; i < lines.Length; i++)
    {
        var myCard = myCards[i];
        myCard.GetValue(false);
        for (int j = i + 1; j < i + 1 + myCard.Value; j++)
        {
            myCards[j].AddInstances(myCard.Instances);
        }
    }
    foreach (var card in myCards)
    { sum += card.Instances; }
    return sum;
}
public class Card
{
    public int Instances { get; set; } = 1;
    public int Value { get; set; } = 0;
    private string[]? WinningNumbers { get; set; }
    private string[]? MyNumbers { get; set; }

    public Card(string card)
    {
        var numbers = card.Split(':')[1].Split('|');
        WinningNumbers = numbers[0].Replace("  ", " ").Trim().Split(' ');
        MyNumbers = numbers[1].Replace("  ", " ").Trim().Split(' ');
    }
    public void AddInstances(int value)
    {
        Instances += value * 1;
    }
    public void GetValue(bool part1)
    {
        foreach (var number in MyNumbers!)
        {
            if (WinningNumbers!.Contains(number))
            {
                if (part1)
                {
                    Value = Value == 0 ? 1 : Value * 2;
                }
                else
                {
                    Value++;
                }
            }
        }
    }
}