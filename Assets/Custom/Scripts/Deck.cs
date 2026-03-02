using System;
using System.Collections.Generic;
using System.Linq;

public class Deck
{
    private List<CardData> cards;
    private Random random = new Random();

    public int CardsRemaining => cards.Count;

    public Deck(bool includeJokers = false)
    {
        InitializeDeck(includeJokers);
    }

    private void InitializeDeck(bool includeJokers)
    {
        cards = new List<CardData>();
        
        // Добавляем все комбинации мастей и достоинств
        foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
        {
            if (suit == CardSuit.None)
                continue;
            
            foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
            {
                cards.Add(new CardData(suit, rank));
            }
        }

        if (includeJokers)
        {
            cards.Add(new CardData(CardSuit.Hearts, (CardRank)0)); // Джокер 1
            cards.Add(new CardData(CardSuit.Diamonds, (CardRank)0)); // Джокер 2
        }
    }

    public void Shuffle()
    {
        // Алгоритм Фишера-Йетса для тасовки
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (cards[j], cards[i]) = (cards[i], cards[j]);
        }
    }

    public List<CardData> Draw(int count)
    {
        if (count > cards.Count)
            throw new ArgumentException($"Not enough cards in deck ({cards.Count})");

        var drawnCards = cards.Take(count).ToList();
        cards.RemoveRange(0, count);
        return drawnCards;
    }
    
    public List<CardData> DrawRemain()
    {
        List<CardData> drawnCards = new();
        drawnCards.AddRange(cards);
        cards.Clear();
        return drawnCards;
    }
    
    public void ReturnCards(List<CardData> cardsToReturn)
    {
        cards.AddRange(cardsToReturn);
    }

    public void ResetAndShuffle()
    {
        InitializeDeck(cards.Any(c => (int)c.Rank == 0)); // Сохраняем состояние джокеров
        Shuffle();
    }
}