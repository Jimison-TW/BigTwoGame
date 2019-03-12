﻿using Assets.Scripts.Game.Component;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class HandCards
    {
        public int Count { get { return allCardInfo.Count; } private set { } }
        public List<Card> willDrop { get; private set; } = new List<Card>();

        private List<Card> allCardInfo = new List<Card>();
        private Dictionary<int, List<Card>> infoGroupByNumber = new Dictionary<int, List<Card>>();


        /// <summary>
        /// 將牌的資訊加到手牌中
        /// </summary>
        /// <param name="card">要加入手牌的卡牌物件</param>
        public void Add(Card info)
        {
            allCardInfo.Add(info);
            if (infoGroupByNumber.ContainsKey(info.cardValue))
            {
                infoGroupByNumber[info.cardValue].Add(info);
            }
            else
            {
                List<Card> group = new List<Card>();
                group.Add(info);
                infoGroupByNumber[info.cardValue] = group;
            }
        }

        /// <summary>
        /// 從手牌中取得要出的牌，並從手牌中移除
        /// </summary>
        /// <param name="cardIndex">卡牌的id編號</param>
        /// <returns>要出得卡牌物件</returns>
        public void Remove()
        {
            foreach (var info in willDrop)
            {
                allCardInfo.Remove(info);
                infoGroupByNumber[info.cardValue].Remove(info);
            }
            willDrop.Clear();
        }

        public void findSingle(Card enemyMaxCard)
        {
            Card card = findBiggerIndex(enemyMaxCard.cardIndex);
            if (card != null)
            {
                willDrop.Add(card);
            }
        }

        public void findPair(Card enemyMaxCard)
        {
            int index = enemyMaxCard.cardValue;
            for (int i = index; i <= 13; i++)
            {
                List<Card> result = findSameNumberGroup(index, 2);
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
        }

        public void findTwoPair(Card enemyMaxCard)
        {
            var cardGroup = from item in allCardInfo   //每一项                        
                            group item by item.cardValue into gro   //按项分组，没组就是gro                        
                            orderby gro.Count() descending   //按照每组的数量进行排序              
                            //返回匿名类型对象，输出这个组的值和这个值出现的次数以及index最大的那張牌           
                            select new
                            {
                                num = gro.Key,
                                count = gro.Count(),
                                result = gro.ToList(),
                                max = gro.OrderBy(i => i.cardIndex).Last()
                            };
            Card tmp = null;
            foreach (var group in cardGroup)
            {
                if (group.count < 2)
                {
                    Debug.LogWarning("超出搜尋範圍");
                    break;
                }
                if (willDrop.Count < 2)
                {
                    Debug.LogWarning($"0張牌{willDrop.Count}");
                    tmp = group.max;
                    for (int i = group.count - 1; i > group.count - 3; i--)
                    {
                        willDrop.Add(group.result.ElementAt(i));
                    }
                }
                else if (willDrop.Count < 4)
                {
                    Debug.LogWarning($"2張牌{willDrop.Count}");
                    if ((!tmp.isBigger(enemyMaxCard) && group.max.isBigger(enemyMaxCard)) || tmp.isBigger(enemyMaxCard))
                    {
                        for (int i = group.count - 1; i > group.count - 3; i--)
                        {
                            willDrop.Add(group.result.ElementAt(i));
                        }
                    }
                }
            }
            if (willDrop.Count < 4)
            {
                willDrop.Clear();
                return;
            }
            Debug.LogWarning($"4張牌{willDrop.Count}");
            tmp = allCardInfo.Find(card => card.cardNumber != willDrop[0].cardNumber && card.cardNumber != willDrop[2].cardNumber);
            if (tmp != null)
            {
                willDrop.Add(tmp);
            }
            else willDrop.Clear();
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
        public void findStraight(List<Card> enemyDropCard)
        {

        }

        public void findFullHouse(Card enemyMaxCard)
        {
            var cardGroup = from item in allCardInfo   //每一项                        
                            group item by item.cardValue into gro   //按项分组，没组就是gro                        
                            orderby gro.Count() descending   //按照每组的数量进行排序              
                            //返回匿名类型对象，输出这个组的值和这个值出现的次数以及index最大的那張牌           
                            select new
                            {
                                num = gro.Key,
                                count = gro.Count(),
                                result = gro.ToList(),
                                max = gro.OrderBy(i => i.cardIndex).Last()
                            };

            Card tmp = null;
            foreach (var group in cardGroup)
            {
                if (group.count >= 3 && willDrop.Count < 3 && group.max.isBigger(enemyMaxCard))
                {
                    tmp = group.max;
                    for (int i = group.count - 1; i > group.count - 4; i--)
                    {
                        willDrop.Add(group.result.ElementAt(i));
                    }
                    break;
                }
            }
            if (willDrop.Count < 3)
            {
                willDrop.Clear();
                return;
            }
            foreach (var group in cardGroup)
            {
                if (group.count >= 2 && group.max.cardNumber != tmp.cardNumber)
                {
                    for (int i = group.count - 1; i > group.count - 2; i--)
                    {
                        willDrop.Add(group.result.ElementAt(i));
                    }
                    break;
                }
            }
            if (willDrop.Count < 5) willDrop.Clear();
        }

        public void findFourInOne(Card enemyMaxCard)
        {
            var cardGroup = from item in allCardInfo   //每一项                        
                            group item by item.cardValue into gro   //按项分组，没组就是gro                        
                            orderby gro.Count() descending   //按照每组的数量进行排序              
                            //返回匿名类型对象，输出这个组的值和这个值出现的次数以及index最大的那張牌           
                            select new
                            {
                                num = gro.Key,
                                count = gro.Count(),
                                result = gro.ToList(),
                                max = gro.OrderBy(i => i.cardIndex).Last()
                            };

            Card tmp = null;
            foreach (var group in cardGroup)
            {
                if (group.count == 4 && willDrop.Count < 4 && group.max.isBigger(enemyMaxCard))
                {
                    tmp = group.max;
                    for (int i = group.count - 1; i > group.count - 5; i--)
                    {
                        willDrop.Add(group.result.ElementAt(i));
                    }
                    break;
                }
            }
            if (willDrop.Count < 4)
            {
                willDrop.Clear();
                return;
            }
            tmp = allCardInfo.Find(card => card.cardNumber != willDrop[0].cardNumber);
            if (tmp != null)
            {
                willDrop.Add(tmp);
            }
            else willDrop.Clear();
        }

        public void findFlushStraight(List<Card> enemyDropCard)
        {

        }

        private Card findBiggerIndex(int cardIndex)
        {
            Card info = allCardInfo.Find((Card i) => i.cardIndex > cardIndex);
            return info;
        }

        private Card findBiggerFlower(int cardFlower)
        {
            Card info = allCardInfo.Find((Card i) => (int)i.cardFlower > cardFlower);
            return info;
        }

        private Card findBiggerNumber(int cardNumber)
        {
            Card info = allCardInfo.Find((Card i) => i.cardValue > cardNumber);
            return info;
        }

        private Card findSameFlower(Card other)
        {
            Card info = allCardInfo.Find((Card self) => (self.cardFlower == other.cardFlower) && (self.cardIndex != other.cardIndex));
            return info;
        }

        private Card findSameNumber(Card other)
        {
            Card info = allCardInfo.Find((Card self) => (self.cardValue == other.cardValue) && (self.cardIndex != other.cardIndex));
            return info;
        }

        private Card findSameNumber(int number)
        {
            Card info = allCardInfo.Find((Card self) => (self.cardValue == number));
            return info;
        }

        public Card findMinCard()
        {
            allCardInfo.OrderBy(i => i.cardIndex).ToList();
            return allCardInfo[0];
        }

        private Card findMinNotInclude(List<Card> doNotInclude)
        {
            Card target = findMinCard();
            foreach (var card in doNotInclude)
            {
                if (target.cardIndex == card.cardIndex)
                {
                    target = findBiggerIndex(target.cardIndex);
                }
            }
            return target;
        }

        private List<Card> findStraight(int[] cardNumbers)
        {
            List<Card> result = new List<Card>();
            foreach (var i in cardNumbers)
            {
                Card card = findSameNumber(i);
                if (card == null) return null;
                else result.Add(card);
            }
            return result;
        }

        private List<Card> findFlushStraight(int[] cardNumbers)
        {
            List<Card> result = new List<Card>();
            Card tmpCard = null;
            foreach (var i in cardNumbers)
            {
                Card card = findSameNumber(i);
                if (card == null) return null;
                else if (card != null && tmpCard != null)
                {
                    if (card.cardFlower != tmpCard.cardFlower) return null;
                }
                tmpCard = card;
                result.Add(card);
            }
            return result;
        }

        private List<Card> findFourCard(int cardNumber)
        {
            var result = from item in allCardInfo   //每一项                        
                         group item by item.cardValue into gro   //按项分组，没组就是gro                        
                         orderby gro.Count() descending   //按照每组的数量进行排序              
                                                          //返回匿名类型对象，输出这个组的值和这个值出现的次数以及所有的牌           
                         select new { num = gro.Key, count = gro.Count(), items = gro.ToList() };
            foreach (var group in result)
            {
                if (group.count == 4 && group.num >= cardNumber)
                {
                    return group.items;
                }
            }
            return null;
        }

        private List<Card> findMajorCardGroup(Card other, int count)
        {
            var cardGroup = from item in allCardInfo   //每一项                        
                            group item by item.cardValue into gro   //按项分组，没组就是gro                        
                            orderby gro.Count() descending   //按照每组的数量进行排序              
                            //返回匿名类型对象，输出这个组的值和这个值出现的次数以及index最大的那張牌           
                            select new
                            {
                                num = gro.Key,
                                count = gro.Count(),
                                result = gro.ToList(),
                                max = gro.OrderBy(i => i.cardIndex).Last()
                            };


            foreach (var element in cardGroup)
            {
                if (element.count >= count &&
                    element.max.cardValue >= other.cardValue &&
                    element.max.cardIndex >= other.cardIndex) return element.result;
            }

            return null;
        }

        private List<Card> findMinorCardGroup(int number, int count)
        {
            var cardGroup = from item in allCardInfo   //每一项                        
                            group item by item.cardValue into gro   //按项分组，没组就是gro                        
                            orderby gro.Count() descending   //按照每组的数量进行排序              
                            //返回匿名类型对象，输出这个组的值和这个值出现的次数以及index最大的那張牌           
                            select new
                            {
                                num = gro.Key,
                                count = gro.Count(),
                                result = gro.ToList(),
                                max = gro.OrderBy(i => i.cardIndex).Last()
                            };


            foreach (var element in cardGroup)
            {
                if (element.count >= count) return element.result;
            }

            return null;
        }

        private List<Card> findSameNumberGroup(int numberKey, int count)
        {
            if (infoGroupByNumber.ContainsKey(numberKey) && infoGroupByNumber[numberKey].Count >= count)
            {
                return infoGroupByNumber[numberKey];
            }
            else
            {
                return null;
            }
        }
    }
}