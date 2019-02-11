using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.UIComponent
{
    public class CardStackComponent : MonoBehaviour
    {
        public Transform selfPos;
        public CardComponent[] cardPrefabs = new CardComponent[52];
        public PlayerComponent[] players = new PlayerComponent[4];

        public void createCardStack(List<int> cardNumber)
        {
            foreach (var i in cardNumber)
            {
                Instantiate(cardPrefabs[i], selfPos.position, Quaternion.identity, selfPos);
            }
        }

        public void dealCards(PlayerComponent[] players)
        {
            foreach(var player in players)
            {
                //player.resetCards();
            }
        }
    }
}
