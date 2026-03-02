using UnityEngine;
using UnityEngine.UI;

public class CardComponent : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private DragNDrop _dragNDrop;
    
    public CardData CardData { get; private set; }
    public DragNDrop DragNDrop => _dragNDrop;
    public bool IsOpen { get; private set; }
    public Sprite OpenState { get; private set; } 
    public Sprite CloseState { get; private set; }
    

    public void Init(Canvas canvas, CardData cardData, Sprite openState, Sprite closeState)
    {
        CardData = cardData;
        OpenState = openState;
        CloseState = closeState;
        _dragNDrop.Init(canvas);
        _image.sprite = openState;
    }
    
    public void Flip(bool isOpen)
    {
        _image.sprite = isOpen ? OpenState : CloseState;
        IsOpen = isOpen;
    }
}

