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
            allCards[card.cardIndex] = card;
            allCardInfo.Add(card.getCardInfo());
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
            Card info = allCardInfo.Find((Card i) => i.cardFlower > cardFlower);
            return info;
        }

        public Card findBiggerNumber(int cardNumber)
        {
            Card info = allCardInfo.Find((Card i) => i.cardNumber > cardNumber);
            return info;
        }

        public Card findSameFlower(Card other)
        {
            Card info = allCardInfo.Find((Card self) => (self.cardFlower == other.cardFlower) && (self.cardIndex != other.cardIndex));
            return info;
        }

        public Card findSameNumber(Card other)
        {
            Card info = allCardInfo.Find((Card self) => (self.cardNumber == other.cardNumber) && (self.cardIndex != other.cardIndex));
            return info;
        }

        public Card findSameNumber(int number)
        {
            Card info = allCardInfo.Find((Card self) => (self.cardNumber == number));
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
    }
}