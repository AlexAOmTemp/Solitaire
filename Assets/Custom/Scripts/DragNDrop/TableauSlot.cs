using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TableauSlot : DropSlot
{
    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        
        if (Transport == null)
            return;
        
        if (Transport.OriginStackTransform == transform)
            return;
        
        if (CardsInStackCount == 0)
        {
            if (DroppingCard.CardData.Rank == CardRank.King)
                AcceptTransport(disableDrop: false);
            return;
        }

        if (LastCard.CardData.IsSuitRed == DroppingCard.CardData.IsSuitRed)
            return;

        if (LastCard.CardData.Rank == DroppingCard.CardData.Rank + 1)
            AcceptTransport(disableDrop: false);
    }

    public List<Transform> GetSelectedAndLyingBelow(Transform selectedCard)
    {
        List<Transform> cards = new List<Transform>();
        bool before = true;
        foreach (Transform card in transform)
        {
            if (card == selectedCard)
                before = false;
            if (before == false)
                cards.Add(card);
        }

        return cards;
    }

    public override void CardLeftSlot()
    {
        if (LastCard != null)
            LastCard.Flip(true);
    }
}