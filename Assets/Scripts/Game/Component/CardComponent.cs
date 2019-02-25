using Assets.Scripts.Game.Interface;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Game.Component
{
    public class CardComponent : MonoBehaviour, ICardInfo
    {
        public bool isChoosed = false;
        public int cardIndex { set; get; }
        public int cardFlower { set; get; }
        public int cardNumber { set; get; }

        private UnityAction<bool, Card> clickEvent;
        private UnityAction resetPosEvent;

        public void Init(bool choosed, int index, int flower, int number)
        {
            isChoosed = choosed;
            cardIndex = index;
            cardFlower = flower;
            cardNumber = number;
        }

        public void Move(Vector3 endPos, float time)
        {
            transform.DOMove(endPos, time);
        }

        public Card getCardInfo()
        {
            return new Card(cardIndex, cardFlower, cardNumber);
        }

        public void setClickCardAction(UnityAction<bool, Card> action)
        {
            clickEvent = action;
        }

        public void setResetPosAction(UnityAction action)
        {
            resetPosEvent = action;
        }

        private void OnMouseUpAsButton()
        {
            clickEvent.Invoke(isChoosed, new Card(cardIndex, cardFlower, cardNumber));
        }
    }
}
