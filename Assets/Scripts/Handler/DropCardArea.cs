using Assets.Scripts.Game;
using Assets.Scripts.Type;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Handler
{
    public class DropCardArea
    {
        public ePlayerPosition lastDropPosition { set; get; }
        public DropResult lastDropResult { set; get; }

        public bool canDropCard(DropResult result)
        {
            if (lastDropResult == null) return true;
            switch (result.cardType)
            {
                case eDropCardType.Single:
                    if (result.maxCard.isBigger(lastDropResult.maxCard)) return true;
                    else return false;
                case eDropCardType.Pair:
                    if (result.maxCard.isBigger(lastDropResult.maxCard)) return true;
                    else return false;
                //case eDropCardType.Triple:
                //    if (result.maxCard.compareTo(lastDrop.maxCard)) return true;
                //    return false;
                case eDropCardType.TwoPair:
                    if (result.maxCard.isBigger(lastDropResult.maxCard)) return true;
                    return false;
                case eDropCardType.Straight:
                    if (result.maxCard.isBigger(lastDropResult.maxCard)) return true;
                    return false;
                case eDropCardType.FullHouse:
                    if (result.maxCard.isBigger(lastDropResult.maxCard)) return true;
                    return false;
                //case eDropCardType.Flush:
                //    if (result.maxCard.compareTo(lastDrop.maxCard)) return true;
                //    return false;
                case eDropCardType.FourInOne:
                    if (result.maxCard.isBigger(lastDropResult.maxCard)) return true;
                    return false;
                case eDropCardType.FlushStraight:
                    if (result.maxCard.isBigger(lastDropResult.maxCard)) return true;
                    return false;
                default: return false;
            }
        }

        public DropResult checkCardType(List<Card> cards)
        {
            cards.Sort((x, y) => { return x.cardIndex.CompareTo(y.cardIndex); });
            if (cards.Count == 0 || cards.Count == 4) return null;
            switch (cards.Count)
            {
                case 1:
                    return new DropResult(eDropCardType.Single, cards);
                case 2:
                    if (cards[0].cardNumber != cards[1].cardNumber) return null;
                    return new DropResult(eDropCardType.Pair, cards);
                //case 3:
                //    if (cards[0].cardNumber != cards[1].cardNumber ||
                //        cards[1].cardNumber != cards[2].cardNumber) return null;
                //    setInfoToResult(result, eDropCardType.Triple, cards[cards.Count - 1]);
                //    return result;
                case 5:
                    return checkFiveCardType(cards);
                default: return null;
            }
        }

        private DropResult checkFiveCardType(List<Card> cards)
        {
            HashSet<int> flowers = new HashSet<int>();
            HashSet<int> numbers = new HashSet<int>();
            foreach (var card in cards)
            {
                flowers.Add((int)card.cardFlower);
                numbers.Add((int)card.cardNumber);
            }

            if (numbers.Count == 5)
            {
                if (flowers.Count > 1)
                {
                    return new DropResult(eDropCardType.Straight, cards);
                }
                else
                {
                    return new DropResult(eDropCardType.FlushStraight, cards);
                }
            }
            //else if (flowers.Count == 1)
            //{
            //    setInfoToResult(result, eDropCardType.Flush, cards[4]);
            //}
            else if (numbers.Count == 2)
            {
                if (cards[3].cardValue == cards[4].cardValue && cards[1].cardValue == cards[0].cardValue)
                {
                    //FullHouse
                    return new DropResult(eDropCardType.FullHouse, cards);
                }
                else
                {
                    //FourInOne
                    return new DropResult(eDropCardType.FourInOne, cards);
                }
            }
            else if (numbers.Count == 3)
            {
                //TwoPair
                return new DropResult(eDropCardType.TwoPair, cards);
            }
            else
            {
                return null;
            }
        }
    }
}
