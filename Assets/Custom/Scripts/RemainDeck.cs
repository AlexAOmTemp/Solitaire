using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RemainDeck : MonoBehaviour
{
    [SerializeField] private Transform _deckStack;
    [SerializeField] private Button _nextCardButton;

    private List<Transform> _cards => GetCards();
    private List<Transform> _openCards => GetOpenCards();

    private void Awake()
    {
        _nextCardButton.onClick.AddListener(OnNextCardClicked);
    }

    private void OnNextCardClicked()
    {
        if (_cards.Count == 0)
        {
            if (_openCards.Count != 0)
                RefreshRemain();
        }
        else
        {
            OpenCard();
        }
    }


    private List<Transform> GetCards()
    {
        return transform.Cast<Transform>().Where(child => child != _nextCardButton.transform).ToList();
    }

    private List<Transform> GetOpenCards()
    {
        return _deckStack.Cast<Transform>().ToList();
    }

    private void OpenCard()
    {
        GameController.MoveCard(_cards[^1], _deckStack, true, _openCards.Count + 1, true);
    }

    private void RefreshRemain()
    {
        List<Transform> reversed = new List<Transform>();
        reversed.AddRange(_openCards);
        reversed.Reverse();
        
        foreach (var openCard in reversed)
        {
            GameController.MoveCard(openCard, transform, false, _cards.Count + 1, false);
        }
    }
}