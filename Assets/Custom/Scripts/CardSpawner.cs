using System;
using System.Collections.Generic;
using UnityEngine;

namespace Custom
{
    public class CardSpawner : MonoBehaviour
    {
        [SerializeField] private Canvas _mainCanvas;
        [SerializeField] private GameObject _cardPrefab;
        [SerializeField] private Sprite _closeState;
        [SerializeField] private List<Sprite> Hearts = new();
        [SerializeField] private List<Sprite> Diamonds = new();
        [SerializeField] private List<Sprite> Clubs = new();
        [SerializeField] private List<Sprite> Spades = new();
       
        [NonSerialized] public readonly List<GameObject> InstantiatedCards = new List<GameObject>();
        
        public List<GameObject> SpawnNext(int count)
        {
            var deck = DealerSingleton.Instance.Deck;
            var cards = deck.Draw(count);

            List<GameObject> result = new();
            foreach (CardData cardData in cards)
            {
                GameObject card = CardSpawn(cardData);
                result.Add(card);
            }

            InstantiatedCards.AddRange(result);
            return result;
        }
        
        public GameObject TestSpawn(CardData cardData)
        {
            return CardSpawn(cardData);
        }
        
        private GameObject CardSpawn(CardData cardData)
        {
            Sprite openState = GetSprite(cardData.Suit, cardData.Rank);
            var card = Instantiate(_cardPrefab);
            var cardScript = card.GetComponent<CardComponent>();
            cardScript.Init(_mainCanvas, cardData, openState, _closeState);
            return card;
        }

        private Sprite GetSprite(CardSuit suit, CardRank rank)
        {
            Sprite sprite = suit switch
            {
                CardSuit.Hearts => Hearts[(int) rank - 1],
                CardSuit.Diamonds => Diamonds[(int) rank - 1],
                CardSuit.Clubs => Clubs[(int) rank - 1],
                CardSuit.Spades => Spades[(int) rank - 1],
                _ => throw new ArgumentOutOfRangeException(nameof(suit), suit, null)
            };
            return sprite;
        }
    }
}