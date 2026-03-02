using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardStackTransport : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    
    public List<CardComponent> CardComponents { get; private set; } = new();
    public Transform OriginStackTransform { get; private set; }
    
    private List<Transform> _cards = new();
    private Canvas _mainCanvas;

    public void Init(Canvas mainCanvas, Transform originStackTransform, List<Transform> cards)
    {
        _mainCanvas = mainCanvas;
        CardComponents.Clear();
        _cards.Clear();
        _cards.AddRange(cards);
        OriginStackTransform = originStackTransform;
        
        foreach (Transform card in _cards)
        {
           card.SetParent(transform); 
           CardComponents.Add(card.GetComponent<CardComponent>());
        }
    }
    
    public int CardsCount()
    {
        return _cards.Count;
    }
    
    public CardComponent GetTopCard()
    {
        return _cards[0].GetComponent<CardComponent>();
    }

    public void BeginDrop(PointerEventData eventData)
    {
        foreach (CardComponent card in CardComponents)
        {
            card.DragNDrop.DragState(true);
        }
    }

    public void Drag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
    }

    public void EndDrag(int sortingOrder, bool isDropValid)
    {
        foreach (CardComponent card in CardComponents)
        {
            card.DragNDrop.DragState(false);
            if (isDropValid)
                card.DragNDrop.SetSortingOrder(sortingOrder++);
        }

        if (isDropValid && OriginStackTransform.TryGetComponent(out DropSlot drop))
                drop.CardLeftSlot();
        
        if (isDropValid == false)
            CancelTransport();
    }
    private void CancelTransport()
    {
        foreach (Transform card in _cards)
        {
            card.SetParent(OriginStackTransform); 
        }
    }
}
