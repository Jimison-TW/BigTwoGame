using Assets.Scripts.Type;
using System;

namespace Assets.Scripts.Game
{
    [Serializable]
    public class Card
    {
        public int cardIndex;
        public int cardValue;
        public eCardFlower cardFlower;
        public eCardNumber cardNumber;

        //public Card(int index, int flower, int value, int number)
        //{
        //    cardIndex = index;
        //    cardFlower = flower;
        //    cardValue = value;
        //    cardNumber = number;
        //}

        //public Card(Card info)
        //{
        //    cardIndex = info.cardIndex;
        //    cardFlower = info.cardFlower;
        //    cardValue = info.cardValue;
        //    cardNumber = info.cardNumber;
        //}

        public bool isBigger(Card info)
        {
            if (cardIndex > info.cardIndex) return true;
            else return false;
        }
    }
}