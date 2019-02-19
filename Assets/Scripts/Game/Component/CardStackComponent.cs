using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Type;

namespace Assets.Scripts.Game.Component
{
    public class CardStackComponent : MonoBehaviour
    {
        public CardComponent[] cardPrefabs = new CardComponent[52];

        private List<CardComponent> cardObjects = new List<CardComponent>();

        public void createCardStack(List<int> cardNumber)
        {
            foreach (var i in cardNumber)
            {
                Vector3 objPos = transform.position;
                objPos.z -= 0.0001f * i;
                CardComponent obj = Instantiate(cardPrefabs[i], objPos, Quaternion.identity, transform);
                initailCardInfo(i, obj);
                cardObjects.Add(obj);
            }
        }

        private int cardId = 0;
        public void dealCards(PlayerComponent pComponent, int playerIndex)
        {
            pComponent.init();
            string logTxt = "";
            for (int i = cardId; i < 13 * (playerIndex + 1); i++)
            {
                pComponent.getCards(cardObjects[i]);
                cardId++;
                logTxt += cardObjects[i].cardIndex + cardObjects[i].cardFlower + "" + cardObjects[i].cardNumber + ",";
            }
            Debug.Log($"{pComponent.name}收到編號第{logTxt}張牌");
        }

        private void initailCardInfo(int index, CardComponent card)
        {
            card.cardIndex = index;
            card.isChoosed = false;
            switch (index % 4)
            {
                case 0:
                    card.cardFlower = 4;
                    break;
                case 1:
                    card.cardFlower = 1;
                    break;
                case 2:
                    card.cardFlower = 2;
                    break;
                case 3:
                    card.cardFlower = 3;
                    break;
            }
            switch (index % 13)
            {
                case 0:
                    card.cardNumber = 13;
                    break;
                case 1:
                    card.cardNumber = 1;
                    break;
                case 2:
                    card.cardNumber = 2;
                    break;
                case 3:
                    card.cardNumber = 3;
                    break;
                case 4:
                    card.cardNumber = 4;
                    break;
                case 5:
                    card.cardNumber = 5;
                    break;
                case 6:
                    card.cardNumber = 6;
                    break;
                case 7:
                    card.cardNumber = 7;
                    break;
                case 8:
                    card.cardNumber = 8;
                    break;
                case 9:
                    card.cardNumber = 9;
                    break;
                case 10:
                    card.cardNumber = 10;
                    break;
                case 11:
                    card.cardNumber = 11;
                    break;
                case 12:
                    card.cardNumber = 12;
                    break;
            }
        }
    }
}
