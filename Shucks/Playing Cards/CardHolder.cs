using System;
using System.Collections.Generic;
using System.Linq;

namespace Shucks.Playing_Cards
{
    public class CardHolder
    {
        private string name;
        private List<Card> hand;

        public CardHolder(string name)
        {
            this.name = name;
            hand = new List<Card>();
        }

        public void AddCards(Card card)
        {
            hand.Add(card);
        }

        public void AddCards(List<Card> cards)
        {
            hand = hand.Concat(cards).ToList();
        }

        public List<Card> GetHand()
        {
            return hand;
        }

        public string GetName()
        {
            return name;
        }

        public string HandToString()
        {
            string ret = "";
            foreach (Card card in hand)
            {
                ret += card.ToString() + "\n";
            }
            return ret;
        }
    }
}
