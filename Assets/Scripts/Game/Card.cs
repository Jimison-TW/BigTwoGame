using Assets.Scripts.Game.Interface;

namespace Assets.Scripts.Game
{
    public class Card : ICardInfo
    {
        public int cardIndex { set; get; }
        public int cardFlower { set; get; }
        public int cardNumber { set; get; }

        public Card(int index, int flower, int number)
        {
            cardIndex = index;
            cardFlower = flower;
            cardNumber = number;
        }

        public bool compareTo(ICardInfo info)
        {
            if (cardIndex > info.cardIndex) return true;
            else return false;
        }
    }
}