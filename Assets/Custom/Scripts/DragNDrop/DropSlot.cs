using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DropSlot : MonoBehaviour, IDropHandler
{
    protected CardStackTransport Transport;
    protected CardComponent DroppingCard;

    protected CardComponent LastCard =>
        CardsInStackCount > 0 ? transform.GetChild(transform.childCount - 1).GetComponent<CardComponent>() : null;

    protected int CardsInStackCount => transform.childCount;

    public virtual void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        if (eventData.pointerDrag.TryGetComponent(out DroppingCard))
            Transport = DroppingCard.DragNDrop.Transport;
    }

    public virtual void CardLeftSlot()
    {
    }

    protected void AcceptTransport(bool disableDrop = true)
    {
        foreach (CardComponent card in Transport.CardComponents)
       {
           card.transform.SetParent(transform);
           card.transform.SetAsLastSibling();
           card.DragNDrop.ValidateDrop(CardsInStackCount + 1);
           if (disableDrop)
               card.DragNDrop.OnEndDragInvoked += DragNDrop_OnEndDragInvoked;
       }
    }

    private void DragNDrop_OnEndDragInvoked()
    {
        DroppingCard.DragNDrop.enabled = false;
        DroppingCard.DragNDrop.OnEndDragInvoked -= DragNDrop_OnEndDragInvoked;
    }
}