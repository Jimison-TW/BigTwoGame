using UnityEngine;
using Assets.Scripts.Type;
using UnityEngine.EventSystems;
using DG.Tweening;
using Assets.Scripts.Game.UIComponent;

namespace Assets.Scripts.Game
{
    public class CardComponent : MonoBehaviour
    {
        public int cardIndex { set; get; }
        public eCardFlower flower;
        public eCardNumber number;

        private void OnMouseUpAsButton()
        {
            if (GetComponentInParent<PlayerComponent>().position == ePlayerPosition.MySelf)
            {
                transform.DOMoveY(transform.position.y + 0.01f, 0.1f);
            }
        }
    }
}
