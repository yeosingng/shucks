using System;
using System.Collections.Generic;
using System.Text;

namespace Shucks.Playing_Cards
{
    class Card
    {
        private string suit;
        private string value;

        public Card(string suit, string value)
        {
            this.suit = suit;
            this.value = value;
        }

        public string GetSuit()
        {
            return suit;
        }

        public string GetValue()
        {
            return value;
        }

        override
        public string ToString()
        {
            return value + " of " + suit;
        }
    }
}
