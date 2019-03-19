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

        public bool isBigger(Card info)
        {
            if (cardIndex > info.cardIndex) return true;
            else return false;
        }
    }
}