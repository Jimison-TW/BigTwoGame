﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.UIComponent
{
    public class CardStackComponent : MonoBehaviour
    {
        public Transform selfPos;
        public CardComponent[] cardPrefabs = new CardComponent[52];

        private List<CardComponent> cardObjects = new List<CardComponent>();

        public void createCardStack(List<int> cardNumber)
        {
            foreach (var i in cardNumber)
            {
                Vector3 objPos = selfPos.position;
                objPos.z -= 0.0001f * i;
                CardComponent obj = Instantiate(cardPrefabs[i], objPos, Quaternion.identity, transform);
                obj.cardIndex = i;
                cardObjects.Add(obj);
            }
        }

        private int cardId = 0;
        public void dealCards(PlayerComponent players, int playerIndex)
        {
            string logTxt = "";
            for (int i = cardId; i < 13 * (playerIndex + 1); i++)
            {
                players.resetHandCard(cardObjects[i]);
                cardId++;
                logTxt += cardObjects[i].cardIndex + cardObjects[i].flower + "" + cardObjects[i].number + ",";
            }
            Debug.Log($"{players.name}收到編號第{logTxt}張牌");
        }
    }
}
