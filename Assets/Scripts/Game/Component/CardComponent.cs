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
        private UnityAction<CardComponent, int> resetPosEvent;

        /// <summary>
        /// 填入卡牌物件的資訊
        /// </summary>
        /// <param name="choosed">是否被點選，預設為false</param>
        /// <param name="index">卡牌編號，0為梅花三，51為大老二</param>
        /// <param name="flower">卡牌花色</param>
        /// <param name="number">卡牌數字</param>
        public void Init(bool choosed, int index, int flower, int number)
        {
            isChoosed = choosed;
            cardIndex = index;
            cardFlower = flower;
            cardNumber = number;
        }

        /// <summary>
        /// 讓卡牌進行Tween動畫移動
        /// </summary>
        /// <param name="endPos">移動停止的位置</param>
        /// <param name="time">移動時間</param>
        public void Move(Vector3 endPos, float time)
        {
            transform.DOMove(endPos, time);
        }

        /// <summary>
        /// 讓卡牌進行Tween動畫旋轉
        /// </summary>
        /// <param name="endRotate">轉動停止的位置</param>
        /// <param name="time">轉動時間</param>
        public void Rotate(Vector3 endRotate, float time)
        {
            transform.DORotate(endRotate, time);
        }

        /// <summary>
        /// 觸發重新整理事件
        /// </summary>
        /// <param name="cardOrder">重新定位的位置順序</param>
        public void Reset(int cardOrder)
        {
            resetPosEvent.Invoke(this, cardOrder);
        }

        public Card getCardInfo()
        {
            return new Card(this);
        }

        public void setClickCardAction(UnityAction<bool, Card> action)
        {
            clickEvent = action;
        }

        public void setResetPosAction(UnityAction<CardComponent, int> action)
        {
            resetPosEvent = action;
        }

        private void OnMouseUpAsButton()
        {
            clickEvent.Invoke(isChoosed, new Card(this));
        }
    }
}
