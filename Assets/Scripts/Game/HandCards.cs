using Assets.Scripts.Game.Component;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Game
{
    public class HandCards
    {
        public int Count { get { return allCards.Count; } private set { } }
        private List<Card> allCardInfo = new List<Card>();
        private Dictionary<int, CardComponent> allCards = new Dictionary<int, CardComponent>();
        private Dictionary<int, List<Card>> infoGroupByNumber = new Dictionary<int, List<Card>>();

        /// <summary>
        /// 取得手牌中牌的物件實例
        /// </summary>
        /// <param name="index">牌的id編號</param>
        /// <returns>要找的卡牌物件</returns>
        public CardComponent Find(int index)
        {
            return allCards[index];
        }

        /// <summary>
        /// 加入牌到手牌中
        /// </summary>
        /// <param name="card">要加入手牌的卡牌物件</param>
        public void Add(CardComponent card)
        {
            Card info = card.getCardInfo();
            allCards[info.cardIndex] = card;
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
        public CardComponent Drop(int cardIndex)
        {
            CardComponent card = allCards[cardIndex];
            Card info = allCardInfo.Find((Card i) => i.cardIndex == cardIndex);
            allCards.Remove(cardIndex);
            allCardInfo.Remove(info);
            infoGroupByNumber[info.cardValue].Remove(info);

            return card;
        }

        /// <summary>
        /// 呼叫所有手牌中牌的resetPosEvent
        /// </summary>
        public void resetHandCards()
        {
            int cardOrder = 0;
            foreach (KeyValuePair<int, CardComponent> item in allCards)
            {
                item.Value.Reset(cardOrder);
                cardOrder++;
            }
        }

        public Card findBiggerIndex(int cardIndex)
        {
            Card info = allCardInfo.Find((Card i) => i.cardIndex > cardIndex);
            return info;
        }

        public Card findBiggerFlower(int cardFlower)
        {
            Card info = allCardInfo.Find((Card i) => (int)i.cardFlower > cardFlower);
            return info;
        }

        public Card findBiggerNumber(int cardNumber)
        {
            Card info = allCardInfo.Find((Card i) => i.cardValue > cardNumber);
            return info;
        }

        public Card findSameFlower(Card other)
        {
            Card info = allCardInfo.Find((Card self) => (self.cardFlower == other.cardFlower) && (self.cardIndex != other.cardIndex));
            return info;
        }

        public Card findSameNumber(Card other)
        {
            Card info = allCardInfo.Find((Card self) => (self.cardValue == other.cardValue) && (self.cardIndex != other.cardIndex));
            return info;
        }

        public Card findSameNumber(int number)
        {
            Card info = allCardInfo.Find((Card self) => (self.cardValue == number));
            return info;
        }

        public Card findMinCard()
        {
            allCardInfo.OrderBy(i => i.cardIndex).ToList();
            return allCardInfo[0];
        }

        public Card findMinNotInclude(List<Card> doNotInclude)
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

        public List<Card> findStraight(int[] cardNumbers)
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

        public List<Card> findFlushStraight(int[] cardNumbers)
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

        public List<Card> findFourCard(int cardNumber)
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

        public List<Card> findMajorCardGroup(Card other, int count)
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

        public List<Card> findMinorCardGroup(int number, int count)
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

        public List<Card> findSameNumberGroup(int numberKey, int count)
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