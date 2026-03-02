using System;

namespace Custom
{
    public class DealerSingleton
    {
        private static readonly Lazy<DealerSingleton> _instance = 
            new Lazy<DealerSingleton>(() => new DealerSingleton());
    
        public static DealerSingleton Instance => _instance.Value;

        public Deck Deck { get; private set; }
        
        private DealerSingleton()
        {
            Deck = new Deck(false);
            Deck.Shuffle();
        }
        
    }
}