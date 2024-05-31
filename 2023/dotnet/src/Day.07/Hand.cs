public class Hand
{

    public required string hand {get; set; }
    public required double bid {get; set; }
    public static char[] cardRanks = ['2','3','4','5','6','7','8','9','T','J','Q','K','A'];
    public static string[] handRanks = ["highcard","1pair","2pair","3kind","fullhouse","4kind","5kind"];
    private int[]? _cardsInHandRanks = null;
    public string Kind()
    {
        // Console.WriteLine($"hand {hand}");
        if (_cardsInHandRanks is null) {
            // Console.WriteLine($"_cardsInHandRanks compose");
            _cardsInHandRanks = new int[5];
            char[] cards = hand.ToCharArray();
            // Console.WriteLine($"cards {String.Join(", ", cards.ToList())}");
            for (int c=0; c<5; c+=1) 
            {
                var result = Array.IndexOf(cardRanks, cards[c]);
                _cardsInHandRanks[c] = Array.IndexOf(cardRanks, cards[c]);
            }
        }
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
            // Console.WriteLine($"rank {k} frequency {result}");
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

}