public class Hand
{

    public required string hand {get; set; }
    public required double bid {get; set; }
    public static char[] cardRanks = ['2','3','4','5','6','7','8','9','T','J','Q','K','A'];
    public static string[] handRanks = ["nothing","highcard","1pair","2pair","3kind","fullhouse","4kind","5kind"];
    private double[] _cardsInHandRanks = new double[5];
    private string _kind = "";
    public string kind()
    {
        if (_cardsInHandRanks is null) {
            _rankCardsInHand();
        }
        if (_kind == "") {
            _kind = "nothing";
        }
        return _kind;
    }
    public void _rankCardsInHand()
    {
        char[] cards = hand.ToCharArray();
        for (int c=0; c<5; c+=1) 
        {
            _cardsInHandRanks[c] = Array.IndexOf(cardRanks, cards[c]);
        }
    }
}