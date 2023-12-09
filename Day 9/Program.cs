Part1("File.txt");
Part2("File.txt");
void Part1(string filename)
{
    var sequences = File.ReadAllLines(filename).Select(x => new Sequence(x.Split(" ").Select(y => int.Parse(y)).ToList())).ToList();
    var continuingNumbers = new List<int>();
    foreach (var sequence in sequences)
    {
        var mySequence = sequence;
        while (mySequence!.CurrentSequence.Where(x => x == 0).Select(x => x).Count() != mySequence.CurrentSequence.Count())
        {
            mySequence.GetSubSequences();
            var temp = mySequence;
            mySequence = mySequence.SubSequence;
            mySequence!.MotherSequence = temp;
        }
        var nextPrediction = 0;
        while (mySequence.MotherSequence != null)
        {
            var length = mySequence.CurrentSequence.Count();
            if (mySequence.SubSequence == null)
            {
                nextPrediction = mySequence.CurrentSequence[length - 1] + mySequence.MotherSequence.CurrentSequence[length];
            }
            else
            {
                nextPrediction = mySequence.CurrentSequence[length - 1] + mySequence.MotherSequence.CurrentSequence[length - 1];
            }
            mySequence = mySequence.MotherSequence;
            mySequence.CurrentSequence.Add(nextPrediction);
        }
        continuingNumbers.Add(nextPrediction);
    }
    Console.WriteLine(continuingNumbers.Sum());
}
void Part2(string filename)
{
    var sequences = File.ReadAllLines(filename).Select(x => new Sequence(x.Split(" ").Select(y => int.Parse(y)).ToList())).ToList();
    var previousNumbers = new List<int>();
    foreach (var sequence in sequences)
    {
        var mySequence = sequence;
        while (mySequence!.CurrentSequence.Where(x => x == 0).Select(x => x).Count() != mySequence.CurrentSequence.Count())
        {
            mySequence.GetSubSequences();
            var temp = mySequence;
            mySequence = mySequence.SubSequence;
            mySequence!.MotherSequence = temp;
        }
        var previousPrediction = 0;
        while (mySequence.MotherSequence != null)
        {
            if (mySequence.SubSequence == null)
            {
                previousPrediction = mySequence.MotherSequence.CurrentSequence[0] -mySequence.CurrentSequence[0];
            }
            else
            {
                previousPrediction = mySequence.MotherSequence.CurrentSequence[0] - mySequence.CurrentSequence[0];
            }
            mySequence = mySequence.MotherSequence;
            mySequence.CurrentSequence.Insert(0, previousPrediction);
        }
        previousNumbers.Add(previousPrediction);
    }
    Console.WriteLine(previousNumbers.Sum());
}
public class Sequence(List<int> sequence)
{
    public List<int> CurrentSequence { get; set; } = sequence;
    public Sequence? SubSequence { get; set; }
    public Sequence? MotherSequence { get; set; }

    public void GetSubSequences()
    {
        var subSequence = new List<int>();
        for (int i = 0; i < CurrentSequence.Count - 1; i++)
        {
            subSequence.Add(CurrentSequence[i + 1] - CurrentSequence[i]);
        }
        SubSequence = new Sequence(subSequence);
    }
}