using UnityEngine;
using Assets.Scripts.Type;
using UnityEngine.Events;

namespace Assets.Scripts.Game
{
    public class CardComponent : MonoBehaviour, ICardInfo
    {
        public bool isChoosed { set; get; }
        public int cardIndex { set; get; }
        public eCardFlower cardFlower { set; get; }
        public eCardNumber cardNumber { set; get; }

        private UnityAction<ICardInfo> clickEvent;

        public void setClickCardAction(UnityAction<ICardInfo> action)
        {
            clickEvent = action;
        }

        private void OnMouseUpAsButton()
        {
            clickEvent.Invoke(this);
        }
    }
}
