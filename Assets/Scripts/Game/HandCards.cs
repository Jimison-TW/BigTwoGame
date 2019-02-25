using Assets.Scripts.Game.Component;
using System.Collections.Generic;

namespace Assets.Scripts.Game
{
    public class HandCards
    {
        public int Count { get { return allCards.Count; } private set { } }
        private List<int>[] cardList = new List<int>[4];
        private Dictionary<int, CardComponent> allCards = new Dictionary<int, CardComponent>();

        public HandCards()
        {
            List<int> clubCard = new List<int>();
            List<int> diamondCard = new List<int>();
            List<int> heartCard = new List<int>();
            List<int> spadeCard = new List<int>();
            cardList[0] = clubCard;
            cardList[1] = diamondCard;
            cardList[2] = heartCard;
            cardList[3] = spadeCard;
        }

        public HandCards(List<CardComponent> cards)
        {
            List<int> clubCard = new List<int>();
            List<int> diamondCard = new List<int>();
            List<int> heartCard = new List<int>();
            List<int> spadeCard = new List<int>();
            foreach (var card in cards)
            {
                allCards[card.cardIndex] = card;
                switch (card.cardFlower)
                {
                    case 1:
                        clubCard.Add(card.cardNumber);
                        break;
                    case 2:
                        diamondCard.Add(card.cardNumber);
                        break;
                    case 3:
                        heartCard.Add(card.cardNumber);
                        break;
                    case 4:
                        spadeCard.Add(card.cardNumber);
                        break;
                }
            }
            clubCard.Sort();
            diamondCard.Sort();
            heartCard.Sort();
            spadeCard.Sort();
            cardList[0] = clubCard;
            cardList[1] = diamondCard;
            cardList[2] = heartCard;
            cardList[3] = spadeCard;
        }

        public CardComponent Find(int index)
        {
            return allCards[index];
        }

        public void Get(CardComponent card)
        {
            allCards[card.cardIndex] = card;
            cardList[card.cardFlower - 1].Add(card.cardNumber);
        }

        public CardComponent Drop(int cardIndex)
        {
            return allCards[cardIndex];
        }

        public void resetHandCards()
        {
            int cardOrder = 0;
            foreach (KeyValuePair<int, CardComponent> item in allCards)
            {
                item.Value.Reset(cardOrder);
                cardOrder++;
            }
        }

        private void removeCard(List<Card> cardInfo)
        {
            foreach (var card in cardInfo)
            {
                allCards.Remove(card.cardIndex);
                cardList[card.cardFlower - 1].Remove(card.cardNumber);
            }
        }
    }
}