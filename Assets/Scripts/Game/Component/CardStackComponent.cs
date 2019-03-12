using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Type;
using Assets.Scripts.Handler;

namespace Assets.Scripts.Game.Component
{
    public class CardStackComponent : MonoBehaviour
    {
        public CardComponent[] cardPrefabs = new CardComponent[52];

        private List<CardComponent> cardObjects = new List<CardComponent>();

        /// <summary>
        /// 產生卡牌物件進入牌堆
        /// </summary>
        /// <param name="cardNumber"></param>
        public void CreateCard(List<int> cardNumber)
        {
            foreach (var i in cardNumber)
            {
                Vector3 objPos = transform.position;
                objPos.z -= 0.0001f * i;
                CardComponent obj = Instantiate(cardPrefabs[i], objPos, Quaternion.identity, transform);
                cardObjects.Add(obj);
            }
        }

        private int cardId = 0;
        /// <summary>
        /// 牌堆發牌並初始化卡牌資訊
        /// </summary>
        /// <param name="pComponent"></param>
        /// <param name="playerIndex"></param>
        public void dealCards(Player player, int playerIndex)
        {
            string logTxt = "";
            for (int i = cardId; i < 13 * (playerIndex + 1); i++)
            {
                player.GetCard(cardObjects[i]);
                cardId++;
                logTxt += cardObjects[i].getCardInfo().cardIndex + "_" +
                    cardObjects[i].getCardInfo().cardFlower.ToString() +
                    (int)cardObjects[i].getCardInfo().cardNumber + " , ";
            }
            Debug.Log($"{player.position}收到編號第{logTxt}張牌");
        }
    }
}
