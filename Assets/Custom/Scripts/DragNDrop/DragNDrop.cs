using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas _thisCanvas;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private CardStackTransport _cardStackTransportPrefab;
   
    public event Action OnEndDragInvoked;

    public CardStackTransport Transport { get; private set; }

    private Canvas _mainCanvas;
    private Vector2 _savedPosition;
    private bool _isDropValidated;
    private int _savedSorting;

    public void Init(Canvas mainCanvas)
    {
        _mainCanvas = mainCanvas;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        List<Transform> cardTransformsPack = new();
        if (transform.parent.TryGetComponent(out CardStackSlot cardStackSlotScript))
        {
            cardTransformsPack = cardStackSlotScript.GetSelectedAndLyingBelow(transform);
            Debug.Log("Stack: " + cardStackSlotScript.transform.childCount);
        }
        else 
            cardTransformsPack.Add(transform);
        
        Transport = Instantiate(_cardStackTransportPrefab, GameController.Instance.transform).GetComponent<CardStackTransport>();
        Transport.Init(_mainCanvas, transform.parent, cardTransformsPack);
        Transport.BeginDrop(eventData);
        Transport.transform.position = transform.position;

    }

    public void OnDrag(PointerEventData eventData)
    {
        Transport.Drag(eventData);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        Transport.EndDrag(_savedSorting, _isDropValidated);
        Destroy(Transport.gameObject);
        Transport = null;
        _isDropValidated = false;
        OnEndDragInvoked?.Invoke();
    }

    public void DragState(bool isDrag)
    {
        _canvasGroup.alpha = isDrag? .6f : 1f;
        _canvasGroup.blocksRaycasts = !isDrag;
        if (isDrag)
            _savedSorting = _thisCanvas.sortingOrder;
        SetSortingOrder(isDrag ? _savedSorting + 40 : _savedSorting);
    }

    public void SetSortingOrder(int sortingOrder)
    {
        _thisCanvas.sortingOrder = sortingOrder;
    }

    public void ValidateDrop(int sortingOrder)
    {
        _savedSorting = sortingOrder;
        _isDropValidated = true;
    }
}