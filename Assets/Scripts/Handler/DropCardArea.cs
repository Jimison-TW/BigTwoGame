using Assets.Scripts.Game;
using Assets.Scripts.Game.Interface;
using Assets.Scripts.Type;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Handler
{
    public class DropCardArea
    {
        private DropResult lastDrop = null;

        public bool canDropCard(DropResult result)
        {
            if (lastDrop == null) return true;
            switch (result.cardType)
            {
                case eDropCardType.Single:
                    if (result.maxCardIndex > lastDrop.maxCardIndex) return true;
                    else return false;
                case eDropCardType.Pair:
                    return false;
                case eDropCardType.Triple:
                    return false;
                case eDropCardType.TwoPair:
                    return false;
                case eDropCardType.Straight:
                    return false;
                case eDropCardType.FullHouse:
                    return false;
                case eDropCardType.Flush:
                    return false;
                case eDropCardType.FourInOne:
                    return false;
                case eDropCardType.FlushStraight:
                    return false;
                default: return false;
            }
        }

        public DropResult checkCardType(List<Card> cards)
        {
            sortCardList(cards);
            DropResult result = new DropResult();
            if (cards.Count == 0 || cards.Count == 4) return null;
            switch (cards.Count)
            {
                case 1:
                    setInfoToResult(result, eDropCardType.Single, cards[0]);
                    return result;
                case 2:
                    if (cards[0].cardNumber != cards[1].cardNumber) return null;
                    setInfoToResult(result, eDropCardType.Pair, cards[cards.Count - 1]);
                    return result;
                case 3:
                    if (cards[0].cardNumber != cards[1].cardNumber ||
                        cards[1].cardNumber != cards[2].cardNumber) return null;
                    setInfoToResult(result, eDropCardType.Triple, cards[cards.Count - 1]);
                    return result;
                case 5:
                    setInfoToResult(result, eDropCardType.Pair, cards[cards.Count - 1]);
                    return result;
                default: return null;
            }
        }

        public void sortCardList(List<Card> cards)
        {
            Dictionary<int, Card> cardMap = new Dictionary<int, Card>();
            List<int> indexList = new List<int>();
            foreach (var card in cards)
            {
                indexList.Add(card.cardIndex);
                cardMap.Add(card.cardIndex, card);
            }
            indexList.Sort();
            Debug.Log(indexList);
            cards.Clear();
            foreach (int id in indexList)
            {
                cards.Add(cardMap[id]);
            }
        }

        private bool isTwoPair(List<Card> cards)
        {
            if (cards[0].cardNumber != cards[1].cardNumber &&
                cards[3].cardNumber != cards[4].cardNumber) return false;
            else if (cards[0].cardFlower == cards[1].cardFlower &&
                cards[2].cardFlower == cards[3].cardFlower) return true;
            else if (cards[3].cardFlower == cards[4].cardFlower &&
                cards[1].cardFlower == cards[2].cardFlower) return true;
            else return false;
        }

        private bool isStraight(List<Card> cards)
        {
            List<int> numbers = new List<int>();
            foreach (var card in cards)
            {
                numbers.Add(card.cardNumber);
            }
            numbers.Sort();
            int tmpNumber = 0;
            foreach (var i in numbers)
            {
                if (tmpNumber == 0) tmpNumber = i;
                else
                {
                    if (i - tmpNumber != 1) return false;
                    else tmpNumber = i;
                }
            }
            return true;
        }

        private bool isFlush(List<Card> cards)
        {
            int tmpFlower = 0;
            foreach (var card in cards)
            {
                if (tmpFlower == 0)
                {
                    tmpFlower = card.cardFlower;
                }
                else
                {
                    if (tmpFlower != card.cardFlower)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool isFullHouse(List<Card> cards)
        {
            int marker = 0;
            int tmpNumber = -10;
            foreach (var card in cards)
            {
                if (tmpNumber == -10)
                {
                    tmpNumber = card.cardFlower;
                }
                else
                {
                    if (card.cardNumber == tmpNumber)
                    {
                        marker += 1;
                    }
                    else
                    {
                        marker -= 1;
                    }
                    tmpNumber = card.cardNumber;
                }
            }
            if (marker == 0) return true;
            else return false;
        }

        private bool isFourInOne(List<Card> cards)
        {
            int marker = 0;
            int tmpNumber = -10;
            foreach (var card in cards)
            {
                if (tmpNumber == -10)
                {
                    tmpNumber = card.cardFlower;
                }
                else
                {
                    if (card.cardNumber == tmpNumber)
                    {
                        marker += 1;
                    }
                    else
                    {
                        marker -= 1;
                    }
                    tmpNumber = card.cardNumber;
                }
            }
            if (marker == 2) return true;
            else return false;
        }

        private bool isFlushStraight(List<Card> cards)
        {
            if (isFlush(cards) && isStraight(cards)) return true;
            return false;
        }

        private void setInfoToResult(DropResult result, eDropCardType type, ICardInfo cardInfo)
        {
            result.setResult(type, cardInfo.cardIndex, cardInfo.cardFlower, cardInfo.cardNumber);
        }
    }
}
