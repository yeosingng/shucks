using System;
using System.Collections.Generic;
using System.Text;

namespace Shucks.Playing_Cards
{
    class Deck
    {
        private List<Card> deck;

        public Deck()
        {
            deck = new List<Card>();
            GenerateDeck();
        }

        public List<Card> DealCards(int numOfCards)
        {
            List<Card> cardsToDeal = new List<Card>(); 
            Random rand = new Random();

            int i = 0;
            while (i < numOfCards){
                int randomIndex = rand.Next(0, deck.Count);
                cardsToDeal.Add(deck[randomIndex]);
                deck.RemoveAt(randomIndex);
                i++;
            }
            return cardsToDeal;
        }

        private void GenerateDeck()
        {
            string[] suits = { "Diamonds", "Clubs", "Hearts", "Spades" };
            string[] values = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

            foreach (string suit in suits)
            {
                foreach (string value in values)
                {
                    deck.Add(new Card(suit, value));
                }
            }
        }
    }
}
