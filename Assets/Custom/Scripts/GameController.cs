using System;
using System.Collections.Generic;
using System.Dynamic;
using Custom;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform _remainDeck;
    [SerializeField] private CardSpawner _cardSpawner;
    [SerializeField] private CardStacksSpawner _cardStacks;

    public static GameController Instance;
    
    private Deck _deck = DealerSingleton.Instance.Deck;

    public static void MoveCard(Transform cardTransform, Transform targetTransform, bool flip, int sortOrder,
        bool dragEnabled = true)
    {
        var cardScript = cardTransform.GetComponent<CardComponent>();
        cardScript.Flip(flip);
        cardTransform.SetParent(targetTransform);
        cardTransform.SetAsLastSibling();
        cardScript.DragNDrop.SetSortingOrder(sortOrder);
        cardScript.DragNDrop.enabled = dragEnabled;
    }
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        
        for (var i = 0; i < _cardStacks.CardStackSlots.Count; i++)
        {
            var spawned = _cardSpawner.SpawnNext(i+1);
            SetCardsToStack(spawned, _cardStacks.CardStackSlots[i]);
        }

        var remainCards = _cardSpawner.SpawnNext(_deck.CardsRemaining);
        
        int order = 1;
        foreach (GameObject remainCard in remainCards)
        {
            MoveCard(remainCard.transform, _remainDeck, false, order++,false);
        }
    }

    private void SetCardsToStack(List<GameObject> spawned, CardStackSlot slot)
    {
        for (var j = 0; j < spawned.Count; j++)
        {
            MoveCard(spawned[j].transform, slot.transform, j == spawned.Count - 1, j + 1);
        }
    }
    
 
    
}