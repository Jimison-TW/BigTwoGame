using Assets.Scripts.Game.Component;
using System.Collections.Generic;

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

        public Card findSameFlower(int cardFlower)
        {
            Card info = allCardInfo.Find((Card i) => i.cardFlower == cardFlower);
            return info;
        }

        public Card findSameNumber(int cardNumber)
        {
            Card info = allCardInfo.Find((Card i) => i.cardNumber == cardNumber);
            return info;
        }
    }
}