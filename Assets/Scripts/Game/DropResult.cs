using System.Collections.Generic;
using Assets.Scripts.Type;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class DropResult
    {
        public eDropCardType cardType { get; private set; }
        public List<Card> dropCards { get; private set; }
        public Card maxCard { get {
                Debug.Log(dropCards[dropCards.Count - 1].cardIndex);
                return dropCards[dropCards.Count - 1]; } private set { } }
        public Card minCard { get { return dropCards[0]; } private set { } }

        public void setResult(eDropCardType type, List<Card> cards)
        {
            cards.Sort((x, y) => { return x.cardIndex.CompareTo(y.cardIndex); });
            cardType = type;
            dropCards = cards;
            foreach (var card in dropCards)
            {
                Debug.Log($"id={card.cardIndex}");
            }
        }
    }
}