using System.Diagnostics;

public class Hand
{
    public static char[] cardRanks = ['2','3','4','5','6','7','8','9','T','J','Q','K','A'];
    public static string[] handRanks = ["highcard","1pair","2pair","3kind","fullhouse","4kind","5kind"];

    private string _cards = "";
    public required double bid {get; set; }
    private int[] _cardsInHandRanks = new int[5];
    public required string cards
    {
        get { return _cards; }

        set
        {
            char[] cardsCharArray = value.ToCharArray();
            for (int c=0; c<5; c+=1) 
            {
                var result = Array.IndexOf(cardRanks, cardsCharArray[c]);
                _cardsInHandRanks[c] = Array.IndexOf(cardRanks, cardsCharArray[c]);
            }
            // Console.WriteLine($"cards required attribute initializer");
            // Console.WriteLine($"value {value}");
            // Console.WriteLine($"cardsInHandRanks {String.Join(", ", _cardsInHandRanks)}");
            Array.Sort(_cardsInHandRanks);
            // Console.WriteLine($"sorted _cardsInHandRanks {String.Join(", ", _cardsInHandRanks)}");
            _cards = value;
        }
    }
    private string _kind = "";
    public string kind
    {
        get {
            if (_kind=="")
            {
                _kind = _calculateKind();
            }
            return _kind;
        }
        set {}
    }

    private int[] _frequencies = new int[13];
    private bool frequenciesInitialized = false;
    public int[] frequencies
    {
        get
        {
            if (frequenciesInitialized is false) {
                frequenciesInitialized = true;
                for (int k=0; k<13; k+=1)
                {
                    int result = _cardsInHandRanks.Count(x => x == k);
                    _frequencies[k] = result;
                }
            }
            return _frequencies;
        }

        set {}
    }

    private string _calculateKind()
    {
        // Console.WriteLine($"cards {cards}");
        // Console.WriteLine($"_cardsInHandRanks {String.Join(", ", _cardsInHandRanks)}");
        int highFrequency = 0;
        int highFrequencyRank = 0;
        int highFrequencySecond = 0;
        int highFrequencySecondRank = 0;
        for (int k=0; k<13; k+=1)
        {
            int result = frequencies[k];
            if (result >= highFrequency)
            {
                highFrequencySecond = highFrequency;
                highFrequencySecondRank = highFrequencyRank;
                highFrequency = result;
                highFrequencyRank = k;
            }
            else if (result >= highFrequencySecond) {
                highFrequencySecond = result;
                highFrequencySecondRank = k;
            }
            // Console.WriteLine($"rank {k} frequency {result}");
        }
        // Console.WriteLine($"highFrequencyRank {highFrequencyRank} highFrequency {highFrequency}");
        // Console.WriteLine($"highFrequencySecondRank {highFrequencySecondRank} highFrequencySecond {highFrequencySecond}");
        if (highFrequency == 5) {
            return "5kind";
        }
        if (highFrequency == 4) {
            return "4kind";
        }
        if (highFrequency == 3 && highFrequencySecond == 2) {
            return "fullhouse";
        }
        if (highFrequency == 3) {
            return "3kind";
        }
        if (highFrequency == 2 && highFrequencySecond == 2) {
            return "2pair";
        }
        if (highFrequency == 2) {
            return "1pair";
        }
        return "highcard";
    }

    public int rank
    {
        get {
            return Array.IndexOf(handRanks, kind);
        }
        set {}
    }

    private int[] _sortedHand = new int[5];
    private bool sortedHandInitialized = false;
    public int[] sortedHand
    {
        get {
            if (sortedHandInitialized is false)
            {
                sortedHandInitialized = true;
                var tmpFrequencies = new int[13];
                // Console.WriteLine($"frequencies {String.Join(", ", frequencies)}");
                Array.Copy(frequencies, tmpFrequencies, 13);
                // Console.WriteLine($"tmpFrequencies {String.Join(", ", tmpFrequencies)}");
                for (int i=4; i>=0; i-=1)
                {
                    int maxFrequency = tmpFrequencies.Max();
                    // Console.WriteLine($"sortedHand i {i} maxFrequency {maxFrequency}");
                    int maxFrequencyIndex = tmpFrequencies.ToList().LastIndexOf(maxFrequency);
                    // Console.WriteLine($"sortedHand i {i} maxFrequencyIndex {maxFrequencyIndex}");
                    for (int f=0; f<maxFrequency; f+=1)
                    {
                        _sortedHand[i] = maxFrequencyIndex;
                        tmpFrequencies[maxFrequencyIndex] = 0;
                        i-=1;
                    }
                    i+=1;
                }
                // Console.WriteLine($"_sortedHand {String.Join(", ", _sortedHand)}");
            }
            return _sortedHand;
        }
        set {}
    }

    public static Hand BetterHand(Hand leftHand, Hand rightHand)
    {
        if (leftHand.rank > rightHand.rank)
        {
            return leftHand;
        }
        else if (leftHand.rank < rightHand.rank)
        {
            return rightHand;
        }
        else if (leftHand.cards == rightHand.cards)
        {
            return leftHand;
        }
        else {
            for (int i=4; i>=0; i-=1)
            {
                if (leftHand.sortedHand[i] > rightHand.sortedHand[i]) { return leftHand; }
                if (leftHand.sortedHand[i] < rightHand.sortedHand[i]) { return rightHand; }
            }
            throw new Exception("IF THEY ARE EQUAL HOW DID THIS HAPPEN");
        }
    }

    public override string? ToString()
    {
        return $"{cards}:{bid}:{kind}";
    }
}