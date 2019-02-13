﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.UIComponent
{
    public class CardStackComponent : MonoBehaviour
    {
        public Transform selfPos;
        public CardComponent[] cardPrefabs = new CardComponent[52];
        public List<CardComponent> cardObjects = new List<CardComponent>();

        public void createCardStack(List<int> cardNumber)
        {
            foreach (var i in cardNumber)
            {
                CardComponent obj = Instantiate(cardPrefabs[i], selfPos.position, Quaternion.identity, transform);
                cardObjects.Add(obj);
            }
            Debug.Log(cardObjects.Count);
        }

        private int cardId = 0;
        public void dealCards(PlayerComponent players, int playerIndex)
        {
            for (int i = cardId; i < 13 * (playerIndex + 1); i++)
            {
                Debug.Log(cardId);
                players.resetCard(cardObjects[i]);
                cardId++;
            }
        }
    }
}
