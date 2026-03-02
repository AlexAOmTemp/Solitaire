public class CardData
{
    public CardSuit Suit { get; }
    public CardRank Rank { get; }
    public bool IsSuitRed => Suit is CardSuit.Hearts or CardSuit.Diamonds; 
    public CardData(CardSuit suit, CardRank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public override string ToString() => $"{Rank} of {Suit}";
}