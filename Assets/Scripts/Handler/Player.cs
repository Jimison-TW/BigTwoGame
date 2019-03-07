using Assets.Scripts.Game;
using Assets.Scripts.Game.Component;
using Assets.Scripts.Game.Interface;
using Assets.Scripts.Type;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        private void findSingle(Card enemyDropCard)
        {
            Card willDrop = playerCards.findBiggerIndex(enemyDropCard.cardIndex);
            component.setDropCardPool(willDrop);
        }

        private List<Card> findPair(Card enemyDropCard, bool isAddPool = true)
        {
            List<Card> willDrop = new List<Card>();
            int unsearchCount = playerCards.Count;
            Card lastSearchCard = enemyDropCard;
            while (willDrop.Count < 2)
            {
                if (unsearchCount < 2)
                {
                    willDrop = null;
                    break;
                }
                if (willDrop.Count > 0)
                {
                    Card card = playerCards.findSameNumber(lastSearchCard);
                    if (card == null)
                    {
                        unsearchCount--;
                        willDrop.Clear();
                    }
                    else
                    {
                        willDrop.Add(card);
                        break;
                    }
                }
                else
                {
                    Card card = playerCards.findBiggerIndex(lastSearchCard.cardIndex);
                    if (card == null)
                    {
                        unsearchCount--;
                        willDrop.Clear();
                    }
                    else
                    {
                        lastSearchCard = card;
                        unsearchCount--;
                        willDrop.Add(card);
                    }
                }
            } 
            if (isAddPool) component.setDropCardPool(willDrop);
            return willDrop;
        }

        private void findTwoPair(Card enemyMaxCard)
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
                List<Card> result = findPair(lastSearchCard, false);
                if (result == null)
                {
                    willDrop.Clear();
                    continue;
                }
                foreach (var card in result)
                {
                    lastSearchCard = card;
                    willDrop.Add(card);
                    unsearchCount--;
                }
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
            List<Card> sortByNumber = enemyDropCard.OrderBy(i => i.cardNumber).ToList();
            List<Card> willDrop = null;
            bool firstRound = true;
            int index = -1;
            switch (sortByNumber[0].cardNumber)
            {
                case 1:
                    if (sortByNumber[1].cardNumber == 10) index = 8;
                    else index = 9;
                    break;
                case 2:
                    index = 10;
                    break;
                default:
                    index = sortByNumber[0].cardNumber - 3;
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
                    if (replace != null && replace.cardNumber == sortByIndex[5].cardNumber)
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
                if (willDrop.Count == 3)
                {
                    List<Card> pair = findPair(lastSearchCard, false);
                    if (pair[0].cardNumber == willDrop[0].cardNumber)
                    {
                        lastSearchCard = pair[1];
                        unsearchCount -= 2;
                        continue;
                    }
                    foreach (var card in pair)
                    {
                        willDrop.Add(card);
                        lastSearchCard = card;
                        unsearchCount--;
                    }
                    break;
                }
                Card bigger = playerCards.findBiggerNumber(lastSearchCard.cardNumber);
                lastSearchCard = bigger;
                unsearchCount--;
                if (bigger.cardFlower > 1) continue;
                for (int i = 0; i < 3; i++)
                {
                    Card result = playerCards.findSameNumber(lastSearchCard);
                    if (result == null)
                    {
                        willDrop.Clear();
                        break;
                    }
                    else
                    {
                        lastSearchCard = result;
                        willDrop.Add(result);
                        unsearchCount--;
                    }
                }
                if (willDrop.Count < 3) willDrop.Clear();
                else
                {
                    unsearchCount = playerCards.Count;
                    lastSearchCard = playerCards.findMinCard();
                }
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
                Card bigger = playerCards.findBiggerNumber(lastSearchCard.cardNumber);
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
            List<Card> sortByNumber = enemyDropCard.OrderBy(i => i.cardNumber).ToList();
            List<Card> willDrop = null;
            bool firstRound = true;
            int index = -1;
            switch (sortByNumber[0].cardNumber)
            {
                case 1:
                    if (sortByNumber[1].cardNumber == 10) index = 8;
                    else index = 9;
                    break;
                case 2:
                    index = 10;
                    break;
                default:
                    index = sortByNumber[1].cardNumber - 3;
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
                    if (replace != null && replace.cardNumber == sortByIndex[5].cardNumber)
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