using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FoundationsSlot : DropSlot
{
    private CardSuit _cardSuit = CardSuit.None;

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        
        if (Transport == null)
            return;
        
        if (Transport.OriginStackTransform == transform)
            return;
        
        if (Transport.CardsCount() > 1)
            return;
        
        if (_cardSuit == CardSuit.None)
        {
            if (DroppingCard.CardData.Rank != CardRank.Ace)
                return;

            _cardSuit = DroppingCard.CardData.Suit;
            AcceptTransport();
        }
        else
        {
            if (_cardSuit != DroppingCard.CardData.Suit)
                return;
            
            if (LastCard.CardData.Rank == DroppingCard.CardData.Rank - 1)
                AcceptTransport();
        }
    }
}
