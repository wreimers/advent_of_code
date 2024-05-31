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
            // Console.WriteLine($"value {value}");
            // Console.WriteLine($"cardsInHandRanks {String.Join(", ", _cardsInHandRanks)}");
            // Array.Sort(_cardsInHandRanks);
            // Console.WriteLine($"sorted _cardsInHandRanks {String.Join(", ", _cardsInHandRanks)}");
            _cards = value;
        }
    }

    public string kind
    {
        get {
            return Kind();
        }
        set {}
    }

    public string Kind()
    {
        // Console.WriteLine($"cards {cards}");
        // Console.WriteLine($"_cardsInHandRanks {String.Join(", ", _cardsInHandRanks)}");
        int[] frequencies = new int[13];
        int highFrequency = 0;
        int highFrequencyRank = 0;
        int highFrequencySecond = 0;
        int highFrequencySecondRank = 0;
        for (int k=0; k<13; k+=1)
        {
            int result = _cardsInHandRanks.Count(x => x == k);
            frequencies[k] = result;
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
            // Console.WriteLine($"highFrequencyRank {highFrequencyRank} highFrequency {highFrequency}");
            // Console.WriteLine($"highFrequencySecondRank {highFrequencySecondRank} highFrequencySecond {highFrequencySecond}");
        }
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
            return Array.IndexOf(handRanks, Kind());
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
            return leftHand;
        }
    }

}