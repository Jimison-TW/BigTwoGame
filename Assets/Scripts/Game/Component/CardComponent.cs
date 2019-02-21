using UnityEngine;
using UnityEngine.Events;
using Assets.Scripts.Game.Interface;
using DG.Tweening;

namespace Assets.Scripts.Game.Component
{
    public class CardComponent : MonoBehaviour
    {
        public bool isChoosed = false;

        private Card cardInfo;

        private UnityAction<bool, CardComponent> clickEvent;

        public void Init(bool choosed, int index, int flower, int number)
        {
            isChoosed = choosed;
            cardInfo = new Card(index, flower, number);
        }

        public void Move(Vector3 endPos, float time)
        {
            transform.DOMove(endPos, time);
        }

        public Card getCardInfo()
        {
            return cardInfo;
        }

        public int getCardIndex()
        {
            return cardInfo.cardIndex;
        }

        public int getCardFlower()
        {
            return cardInfo.cardFlower;
        }

        public int getCardNumber()
        {
            return cardInfo.cardNumber;
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
