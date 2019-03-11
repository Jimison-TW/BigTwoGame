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
                logTxt += cardObjects[i].getCardInfo().cardIndex + 
                    cardObjects[i].getCardInfo().cardFlower.ToString() + 
                    cardObjects[i].getCardInfo().cardNumber.ToString() + " , ";
            }
            Debug.Log($"{player.position}收到編號第{logTxt}張牌");
        }

        //private void initailCardInfo(int index, CardComponent card)
        //{
        //    int flower = 0;
        //    int number = 0;
        //    switch (index % 4)
        //    {
        //        case 0:
        //            flower = 1;
        //            break;
        //        case 1:
        //            flower = 2;
        //            break;
        //        case 2:
        //            flower = 3;
        //            break;
        //        case 3:
        //            flower = 4;
        //            break;
        //    }
        //    switch (index / 4)
        //    {
        //        case 0:
        //            number = 3;
        //            break;
        //        case 1:
        //            number = 4;
        //            break;
        //        case 2:
        //            number = 5;
        //            break;
        //        case 3:
        //            number = 6;
        //            break;
        //        case 4:
        //            number = 7;
        //            break;
        //        case 5:
        //            number = 8;
        //            break;
        //        case 6:
        //            number = 9;
        //            break;
        //        case 7:
        //            number = 10;
        //            break;
        //        case 8:
        //            number = 11;
        //            break;
        //        case 9:
        //            number = 12;
        //            break;
        //        case 10:
        //            number = 13;
        //            break;
        //        case 11:
        //            number = 1;
        //            break;
        //        case 12:
        //            number = 2;
        //            break;
        //    }
        //    card.Init(false, index, flower, number);
        //}
    }
}
