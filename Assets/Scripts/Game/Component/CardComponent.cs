using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Game.Component
{
    public class CardComponent : MonoBehaviour
    {
        public bool isChoosed = false;
        #pragma warning disable
        [SerializeField] private Card cardInfo;

        private UnityAction<bool, CardComponent> clickEvent;
        private UnityAction<CardComponent, int> resetPosEvent;

        /// <summary>
        /// 讓卡牌進行Tween動畫移動
        /// </summary>
        /// <param name="endPos">移動停止的位置</param>
        /// <param name="time">移動時間</param>
        public void TMove(Vector3 endPos, float time)
        {
            transform.DOMove(endPos, time);
        }

        /// <summary>
        /// 讓卡牌進行Tween動畫旋轉
        /// </summary>
        /// <param name="endRotate">轉動停止的位置</param>
        /// <param name="time">轉動時間</param>
        public void TRotate(Vector3 endRotate, float time)
        {
            transform.DORotate(endRotate, time);
        }

        /// <summary>
        /// 讓卡牌同時進行Tween的移動與旋轉，並在完成時進行回呼
        /// </summary>
        /// <param name="endPos">移動停止的位置</param>
        /// <param name="endRotate">轉動停止的位置</param>
        /// <param name="time">動畫時間</param>
        /// <param name="callback">完成時執行的callback</param>
        public void TSequence(Vector3 endPos, Vector3 endRotate, float time,TweenCallback callback = null)
        {
            Sequence mSequence = DOTween.Sequence();
            mSequence.Append(transform.DOMove(endPos, time));
            mSequence.Join(transform.DORotate(endRotate, time));
            if (callback != null) mSequence.AppendCallback(callback);
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
            return cardInfo;
        }

        public void setClickCardAction(UnityAction<bool, CardComponent> action)
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
