using System;
using System.Collections.Generic;
using Shucks.Playing_Cards;

namespace Shucks.Services.Blackjack
{
    public class BlackJackService
    {
        private bool ongoingGame;
        private Dictionary<ulong, CardHolder> cardHolders;
        private Deck gameDeck;

        public BlackJackService()
        {
            ongoingGame = false;
            gameDeck = new Deck();
            cardHolders = new Dictionary<ulong, CardHolder>();
        }

       public string StartGame()
        {
            if (ongoingGame.Equals(false))
            {
                ongoingGame = true;
                DealCards();
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

        public string JoinGame(Discord.IUser user)
        {
            if (!cardHolders.ContainsKey(user.Id))
            {
                cardHolders.Add(user.Id, new CardHolder(user.Username));
                return "Users currently in game: " + UsersToString();
            }

            return String.Format("User {0} already in game", user.Username);
        }

        private string UsersToString()
        {
            string ret = "";
            foreach (CardHolder cardholder in cardHolders.Values)
            {
                ret += cardholder.GetName() + "\n";
            }
            return ret;
        }

        private void DealCards()
        {
            foreach (CardHolder cardholder in cardHolders.Values)
            {
                cardholder.AddCards(gameDeck.DealCards(2));
            }
        } 

        public Dictionary<ulong, CardHolder> GetCardHolders()
        {
            return cardHolders;
        }
    }
}
