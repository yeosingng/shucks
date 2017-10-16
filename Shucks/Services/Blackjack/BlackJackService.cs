using System;
using System.Collections.Generic;
using System.Text;
using Shucks.Playing_Cards;

namespace Shucks.Services.Blackjack
{
    public class BlackJackService
    {
        private bool ongoingGame;
        private HashSet<CardHolder> cardHolders;
        private Deck gameDeck;

        public BlackJackService()
        {
            ongoingGame = false;
            gameDeck = new Deck();
        }

       public string StartGame()
        {
            if (ongoingGame.Equals(false))
            {
                ongoingGame = true;
                return "Game starting!";
            }
            return "Game already ongoing";
        }

        public string EndGame()
        {
            if (ongoingGame.Equals(true))
            {
                ongoingGame = false;
                cardHolders.Clear();
                return "The game has ended";
            }
            return "No game has started";
        }
    }
}
