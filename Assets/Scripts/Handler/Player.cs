using Assets.Scripts.Game;
using Assets.Scripts.Game.Component;
using Assets.Scripts.Game.Interface;
using Assets.Scripts.Type;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Handler
{
    public class Player : IPlayerInfo
    {
        public ePlayerPosition position { get; set; }
        public HandCards playerCards { get; set; }
        private PlayerComponent component;

        public Player(ePlayerPosition position, PlayerComponent comp)
        {
            this.position = position;
            component = comp;
            position = comp.position;
            playerCards = comp.playerCards;
        }

        public void ThinkResult(DropResult result)
        {
            //如果是第一個出牌，直接出單張最小的
            if (result == null)
            {
                Card willDrop = playerCards.findMinCard();
                component.setDropCardPool(willDrop);
                return;
            }

            switch (result.cardType)
            {
                case eDropCardType.Single:
                    findSingle(result.maxCard);
                    break;
                case eDropCardType.Pair:
                    findPair(result.maxCard);
                    break;
                case eDropCardType.TwoPair:
                    findTwoPair(result.maxCard);
                    break;
                case eDropCardType.Straight:
                    findStraight(result.dropCards);
                    break;
                case eDropCardType.FullHouse:
                    findFullHouse(result.maxCard);
                    break;
                case eDropCardType.FourInOne:
                    findFourInOne(result.maxCard);
                    break;
                case eDropCardType.FlushStraight:
                    findFlushStraight(result.dropCards);
                    break;
            }
        }

        private void findSingle(Card enemyMaxCard)
        {
            Card willDrop = playerCards.findBiggerIndex(enemyMaxCard.cardIndex);
            component.setDropCardPool(willDrop);
        }

        private void findPair(Card enemyMaxCard)
        {
            List<Card> willDrop = new List<Card>();
            int index = enemyMaxCard.cardValue;
            if (enemyMaxCard.cardValue < 3) index += 13;
            for (int i = index; i <= 15; i++)
            {
                if (i > 13 && index > 3) index -= 13;
                List<Card> result = playerCards.findSameNumberGroup(index, 2);
                index++;
                if (result != null)
                {
                    for (int j = result.Count - 1; j >= 0; j--)
                    {
                        if (willDrop.Count == 2) break;
                        willDrop.Add(result[j]);
                    }
                    break;
                }
            }
            if (willDrop.Count == 0) willDrop = null;
            component.setDropCardPool(willDrop);
        }

        private void findTwoPair(Card enemyMaxCard)
        {
            List<Card> willDrop = null;
            List<Card> result = playerCards.findMinorCardGroup(3, 2);
            if (result != null)
            {
                willDrop = new List<Card>();
                for (int i = result.Count - 1; i > result.Count - 3; i--)
                {
                    willDrop.Add(result.ElementAt(i));
                }
            }
            if (willDrop != null && willDrop[0].cardValue < enemyMaxCard.cardValue)
            {
                //result = playerCards.findMajorCardGroup()
            }
            component.setDropCardPool(willDrop);
        }

        List<int[]> straightList = new List<int[]> {
            new int[] { 3, 4, 5, 6, 7 } ,
            new int[] { 4, 5, 6, 7, 8 } ,
            new int[] { 5, 6, 7, 8, 9 } ,
            new int[] { 6, 7, 8, 9,10 } ,
            new int[] { 7, 8, 9,10,11 } ,
            new int[] { 8, 9,10,11,12 } ,
            new int[] { 9,10,11,12,13 } ,
            new int[] { 10,11,12,13,1 } ,
            new int[] { 1, 2, 3, 4, 5 } ,
            new int[] { 2, 3, 4, 5, 6 }
        };
        private void findStraight(List<Card> enemyDropCard)
        {
            List<Card> sortByIndex = enemyDropCard.OrderBy(i => i.cardIndex).ToList();
            List<Card> sortByNumber = enemyDropCard.OrderBy(i => i.cardValue).ToList();
            List<Card> willDrop = null;
            bool firstRound = true;
            int index = -1;
            switch (sortByNumber[0].cardValue)
            {
                case 1:
                    if (sortByNumber[1].cardValue == 10) index = 8;
                    else index = 9;
                    break;
                case 2:
                    index = 10;
                    break;
                default:
                    index = sortByNumber[0].cardValue - 3;
                    break;
            }

            for (int i = index; i < straightList.Count; i++)
            {
                willDrop = playerCards.findStraight(straightList[index]);
                willDrop.OrderBy(card => card.cardIndex).ToList();
                if (!firstRound && willDrop != null)
                {
                    break;
                }
                else if (firstRound && willDrop != null && willDrop[5].compareTo(sortByIndex[5]))
                {
                    break;
                }
                else if (firstRound && willDrop != null && !willDrop[5].compareTo(sortByIndex[5]))
                {
                    Card replace = playerCards.findBiggerIndex(sortByIndex[5].cardIndex);
                    if (replace != null && replace.cardValue == sortByIndex[5].cardValue)
                    {
                        willDrop[5] = replace;
                        break;
                    }
                }
                firstRound = false;
                index++;
            }

            component.setDropCardPool(willDrop);
        }

        private void findFullHouse(Card enemyMaxCard)
        {
            List<Card> willDrop = null;
            List<Card> tripleResult = playerCards.findMajorCardGroup(enemyMaxCard, 3);
            if (tripleResult != null)
            {
                willDrop = new List<Card>();

                for (int i = tripleResult.Count - 1; i > tripleResult.Count - 4; i--)
                {
                    willDrop.Add(tripleResult.ElementAt(i));
                }

                //List<Card> pairResult = playerCards.findMultiCards(tripleResult, 2);
            }
            component.setDropCardPool(willDrop);
        }

        private void findFourInOne(Card enemyMaxCard)
        {
            List<Card> willDrop = new List<Card>();
            int unsearchCount = playerCards.Count;
            Card lastSearchCard = enemyMaxCard;
            while (willDrop.Count < 5)
            {
                if (unsearchCount < 5)
                {
                    willDrop = null;
                    break;
                }
                if (willDrop.Count == 4)
                {
                    Card lastOne = playerCards.findMinNotInclude(willDrop);
                    willDrop.Add(lastOne);
                    break;
                }
                Card bigger = playerCards.findBiggerNumber(lastSearchCard.cardValue);
                lastSearchCard = bigger;
                unsearchCount--;
                if (bigger.cardFlower != 0) continue;
                willDrop.Add(bigger);
                for (int i = 0; i < 3; i++)
                {
                    Card sameFlower = playerCards.Find(bigger.cardIndex + 1).getCardInfo();
                    lastSearchCard = sameFlower;
                    willDrop.Add(sameFlower);
                    unsearchCount--;
                }
            }
            component.setDropCardPool(willDrop);
        }

        private void findFlushStraight(List<Card> enemyDropCard)
        {
            List<Card> sortByIndex = enemyDropCard.OrderBy(i => i.cardIndex).ToList();
            List<Card> sortByNumber = enemyDropCard.OrderBy(i => i.cardValue).ToList();
            List<Card> willDrop = null;
            bool firstRound = true;
            int index = -1;
            switch (sortByNumber[0].cardValue)
            {
                case 1:
                    if (sortByNumber[1].cardValue == 10) index = 8;
                    else index = 9;
                    break;
                case 2:
                    index = 10;
                    break;
                default:
                    index = sortByNumber[1].cardValue - 3;
                    break;
            }

            for (int i = index; i < straightList.Count; i++)
            {
                willDrop = playerCards.findFlushStraight(straightList[index]);
                willDrop.OrderBy(card => card.cardIndex).ToList();
                if (!firstRound && willDrop != null)
                {
                    break;
                }
                else if (firstRound && willDrop != null && willDrop[5].compareTo(sortByIndex[5]))
                {
                    break;
                }
                else if (firstRound && willDrop != null && !willDrop[5].compareTo(sortByIndex[5]))
                {
                    Card replace = playerCards.findBiggerIndex(sortByIndex[5].cardIndex);
                    if (replace != null && replace.cardValue == sortByIndex[5].cardValue)
                    {
                        willDrop[5] = replace;
                        break;
                    }
                }
                firstRound = false;
                index++;
            }
            component.setDropCardPool(willDrop);
        }
    }
}