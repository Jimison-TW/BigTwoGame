using UnityEngine;
using UnityEngine.Events;
using Assets.Scripts.Game.Interface;

namespace Assets.Scripts.Game.Component
{
    public class CardComponent : MonoBehaviour
    {
        public bool isChoosed = false;

        private Card cardData;

        private UnityAction<bool, CardComponent> clickEvent;

        public void initCard(bool choosed, int index, int flower, int number)
        {
            cardData = new Card(index, flower, number);
        }

        public int getCardIndex()
        {
            return cardData.cardIndex;
        }

        public int getCardFlower()
        {
            return cardData.cardFlower;
        }

        public int getCardNumber()
        {
            return cardData.cardNumber;
        }

        public void setClickCardAction(UnityAction<bool, CardComponent> action)
        {
            clickEvent = action;
        }

        private void OnMouseUpAsButton()
        {
            clickEvent.Invoke(isChoosed, this);
        }
    }
}
