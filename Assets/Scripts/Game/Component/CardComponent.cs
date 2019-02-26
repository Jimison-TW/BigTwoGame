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

        private UnityAction<bool, ICardInfo> clickEvent;
        private UnityAction<CardComponent, int> resetPosEvent;

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

        public void Reset(int cardOrder)
        {
            resetPosEvent.Invoke(this, cardOrder);
        }

        public Card getCardInfo()
        {
            return new Card(cardIndex, cardFlower, cardNumber);
        }

        public void setClickCardAction(UnityAction<bool, ICardInfo> action)
        {
            clickEvent = action;
        }

        public void setResetPosAction(UnityAction<CardComponent, int> action)
        {
            resetPosEvent = action;
        }

        private void OnMouseUpAsButton()
        {
            clickEvent.Invoke(isChoosed, this);
        }
    }
}
