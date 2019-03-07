using Assets.Scripts.Game;
using Assets.Scripts.Type;
using System.Collections.Generic;

namespace Assets.Scripts.Handler
{
    public class DropCardArea
    {
        public DropResult lastDrop { set; get; }

        public bool canDropCard(DropResult result)
        {
            if (lastDrop == null) return true;
            switch (result.cardType)
            {
                case eDropCardType.Single:
                    if (result.maxCard.compareTo(lastDrop.maxCard)) return true;
                    else return false;
                case eDropCardType.Pair:
                    if (result.maxCard.compareTo(lastDrop.maxCard)) return true;
                    else return false;
                //case eDropCardType.Triple:
                //    if (result.maxCard.compareTo(lastDrop.maxCard)) return true;
                //    return false;
                case eDropCardType.TwoPair:
                    if (result.maxCard.compareTo(lastDrop.maxCard)) return true;
                    return false;
                case eDropCardType.Straight:
                    if (result.maxCard.compareTo(lastDrop.maxCard)) return true;
                    return false;
                case eDropCardType.FullHouse:
                    if (result.maxCard.compareTo(lastDrop.maxCard)) return true;
                    return false;
                //case eDropCardType.Flush:
                //    if (result.maxCard.compareTo(lastDrop.maxCard)) return true;
                //    return false;
                case eDropCardType.FourInOne:
                    if (result.maxCard.compareTo(lastDrop.maxCard)) return true;
                    return false;
                case eDropCardType.FlushStraight:
                    if (result.maxCard.compareTo(lastDrop.maxCard)) return true;
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
                    DropResult result = checkFiveCardType(cards);
                    if ( result == null)
                    {
                        return null;
                    }
                    else
                    {
                        return result;
                    }
                default: return null;
            }
        }

        private DropResult checkFiveCardType(List<Card> cards)
        {
            HashSet<int> flowers = new HashSet<int>();
            HashSet<int> numbers = new HashSet<int>();
            foreach (var card in cards)
            {
                flowers.Add(card.cardFlower);
                numbers.Add(card.cardNumber);
            }

            if (numbers.Count == 5)
            {
                /*
                FlushStraight or Straight
                if (cards[4].cardNumber != 1 && cards[4].cardNumber != 2) //9 10 11 12 13
                {
                    theBig = cards[4];
                }
                else if (cards[4].cardNumber == 1) //10 J Q K A
                {
                    theBig = cards[4];
                }
                else if (cards[4].cardNumber == 2 && cards[3].cardNumber != 1) //2 3 4 5 6
                {
                    theBig = cards[4];
                }
                else if (cards[4].cardNumber == 2 && cards[3].cardNumber == 1) //1 2 3 4 5
                {
                    theBig = cards[2];
                }
                */

                /* 將上方簡化為 */
                Card maxCard = null;
                if (cards[4].cardNumber == 2 && cards[3].cardNumber == 1)
                {
                    maxCard = cards[2];
                }
                else
                {
                    maxCard = cards[4];
                }

                if (flowers.Count > 1)
                {
                    new DropResult(eDropCardType.Straight, cards);
                }
                else
                {
                    new DropResult(eDropCardType.FlushStraight, cards);
                }
            }
            //else if (flowers.Count == 1)
            //{
            //    setInfoToResult(result, eDropCardType.Flush, cards[4]);
            //}
            else if (numbers.Count == 2)
            {
                if (cards[3].cardNumber == cards[4].cardNumber && cards[1].cardNumber == cards[0].cardNumber)
                {
                    //FullHouse
                    if (cards[2].cardNumber != cards[3].cardNumber)
                        new DropResult(eDropCardType.FullHouse, cards);
                    else
                        new DropResult(eDropCardType.FullHouse, cards);
                }
                else
                {
                    //FourInOne
                    if (cards[4].cardNumber != cards[3].cardNumber)
                        new DropResult(eDropCardType.FullHouse, cards);
                    else
                        new DropResult(eDropCardType.FullHouse, cards);
                }

            }
            else if (numbers.Count == 3)
            {
                //TwoPair
                if (cards[3].cardNumber != cards[4].cardNumber)
                    new DropResult(eDropCardType.TwoPair, cards);
                else
                    new DropResult(eDropCardType.TwoPair, cards);
            }
            else
            {
                return null;
            }
            return null;
        }

        /*
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
        */
    }
}
