using Assets.Scripts.Type;
using System;

namespace Assets.Scripts.Game
{
    public class Card : ICardInfo
    {
        public bool isChoosed { set; get; }
        public int cardIndex { set; get; }
        public int cardFlower { set; get; }
        public int cardNumber { set; get; }

        public bool isBigger(ICardInfo info)
        {
            if (cardIndex > info.cardIndex) return true;
            else return false;
        }
    }
}