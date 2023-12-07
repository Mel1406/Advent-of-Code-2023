Part1("File.txt");
Part2("File.txt");
void Part1(string filename)
{
    var lines = File.ReadAllLines(filename);
    var Hands = new List<Hand>();
    foreach (var line in lines)
    {
        var parts = line.Split(" ");
        var cards = new List<Card>();
        foreach (var card in parts[0])
        {
            cards.Add(new Card(card, card switch
            {
                'A' => 14,
                'K' => 13,
                'Q' => 12,
                'J' => 11,
                'T' => 10,
                _ => int.Parse(card.ToString())
            }));
        }
        Hands.Add(new Hand(cards, int.Parse(parts[1]), true));
    }
    bool swapped;
    for (int i = 0; i < Hands.Count; i++)
    {
        swapped = false;
        for (int j = 0; j < Hands.Count - 1 - i; j++)
        {
            var firstHandValue = Hands[j].Type.Value;
            var secondHandValue = Hands[j + 1].Type.Value;
            if (firstHandValue > secondHandValue)
            {
                (Hands[j], Hands[j + 1]) = (Hands[j + 1], Hands[j]);
                swapped = true;
            }
            else if (firstHandValue == secondHandValue)
            {
                for (int k = 0; k < Hands[j].Cards.Count; k++)
                {
                    if (Hands[j].Cards[k].Value > Hands[j + 1].Cards[k].Value)
                    {
                        (Hands[j], Hands[j + 1]) = (Hands[j + 1], Hands[j]);
                        swapped = true;
                        break;
                    }
                    else if (Hands[j].Cards[k].Value < Hands[j + 1].Cards[k].Value)
                    {
                        break;
                    }
                }
            }
        }
        if (!swapped) { break; }
    }
    var bidSum = 0;
    for (int i = 0; i < Hands.Count; i++)
    {
        bidSum += Hands[i].Bid * (i + 1);
    }
    Console.WriteLine(bidSum);
}
void Part2(string filename)
{
    var lines = File.ReadAllLines(filename);
    var Hands = new List<Hand>();
    foreach (var line in lines)
    {
        var parts = line.Split(" ");
        var cards = new List<Card>();
        foreach (var card in parts[0])
        {
            cards.Add(new Card(card, card switch
            {
                'A' => 14,
                'K' => 13,
                'Q' => 12,
                'J' => 1,
                'T' => 10,
                _ => int.Parse(card.ToString())
            }));
        }
        Hands.Add(new Hand(cards, int.Parse(parts[1]), false));
    }
    bool swapped;
    for (int i = 0; i < Hands.Count; i++)
    {
        swapped = false;
        for (int j = 0; j < Hands.Count - 1 - i; j++)
        {
            var firstHandValue = Hands[j].Type.Value;
            var secondHandValue = Hands[j + 1].Type.Value;
            if (firstHandValue > secondHandValue)
            {
                (Hands[j], Hands[j + 1]) = (Hands[j + 1], Hands[j]);
                swapped = true;
            }
            else if (firstHandValue == secondHandValue)
            {
                for (int k = 0; k < Hands[j].Cards.Count; k++)
                {
                    if (Hands[j].Cards[k].Value > Hands[j + 1].Cards[k].Value)
                    {
                        (Hands[j], Hands[j + 1]) = (Hands[j + 1], Hands[j]);
                        swapped = true;
                        break;
                    }
                    else if (Hands[j].Cards[k].Value < Hands[j + 1].Cards[k].Value)
                    {
                        break;
                    }
                }
            }
        }
        if (!swapped) { break; }
    }
    var bidSum = 0;
    for (int i = 0; i < Hands.Count; i++)
    {
        bidSum += Hands[i].Bid * (i + 1);
    }
    Console.WriteLine(bidSum);
}


public class Hand
{
    public List<Card> Cards { get; set; }
    public int Bid { get; set; }
    public List<Card> CardsSorted { get; set; }
    public Type Type { get; set; }
    public Hand(List<Card> cards, int bid, bool part1)
    {
        Cards = cards;
        Bid = bid;
        CardsSorted = cards.OrderBy(card => card.Value).ToList();
        Type = GetType(part1);
    }
    private Type GetType(bool part1)
    {
        var groupedByValue = CardsSorted.GroupBy(card => card).ToList();
        if (CardsSorted.Contains(new Card('J', 1)))
        {
            groupedByValue.RemoveAt(0);
        }
        var subtract = 0;
        switch (CardsSorted.Where(c => c == new Card('J', 1)).Count())
        {
            case 1:
                subtract = 1;
                break;
            case 2:
                subtract = 2;
                break;
            case 3:
                subtract = 3;
                break;
            case 4:
                subtract = 4;
                break;
            case 5:
                return new Type("Five of a Kind", 6);
            
        }

        if (groupedByValue.Any(group => group.Count() == 5 - subtract))
        {
            return new Type("Five of a Kind", 6);
        }
        else if (groupedByValue.Any(group => group.Count() == 4 - subtract))
        {
            return new Type("Four of a Kind", 5);
        }
        else if (groupedByValue.Any(group => group.Count() == 3) && groupedByValue.Any(group => group.Count() == 2))
        {
            return new Type("Full House", 4);
        }
        else if (groupedByValue.Count(group => group.Count() == 2) == 2)
        {
            if (subtract == 1)
            {
                return new Type("Full House", 4);
            }
            else
            {
                return new Type("Two Pairs", 2);
            }
        }
        else if (groupedByValue.Any(group => group.Count() == 3 - subtract))
        {
            return new Type("Three of a Kind", 3);
        }
        else if (groupedByValue.Count(group => group.Count() == 2) == 1)
        {
            return new Type("One Pair", 1);
        }
        else
        {
            if (subtract == 1)
            {
                return new Type("One Pair", 1);
            }
            else
            {
                return new Type("High Card", 0);
            }
        }
    }
}
public record Card(char Symbol, int Value);
public record Type(string TypeOfCards, int Value);

