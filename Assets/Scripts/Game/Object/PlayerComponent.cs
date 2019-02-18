using Assets.Scripts.Type;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Game.Object
{
    public class PlayerComponent : MonoBehaviour
    {
        public ePlayerPosition position;
        private float zeroPos = -0.16f;
        private float Offset = 0.023f;
        private List<CardComponent> dropCardPool = new List<CardComponent>();
        private Dictionary<int, CardComponent> handCards = new Dictionary<int, CardComponent>();
        private UnityAction<ICardInfo> clickAction;

        public void init()
        {
            clickAction = new UnityAction<ICardInfo>(clickCardAction);
        }

        public void resetHandCard(CardComponent card)
        {
            handCards[card.cardIndex] = card;
            card.setClickCardAction(clickAction);
            card.transform.SetParent(transform);
            Vector3 endPos = transform.position;
            Vector3 endRotation = transform.rotation.eulerAngles;
            switch (position)
            {
                case ePlayerPosition.MySelf:
                    endPos.x += zeroPos + Offset * handCards.Count;
                    endRotation.y += 180;
                    break;
                case ePlayerPosition.RightSide:
                    endPos.y += zeroPos + Offset * handCards.Count;
                    break;
                case ePlayerPosition.OppositeSide:
                    endPos.x -= zeroPos + Offset * handCards.Count;
                    break;
                case ePlayerPosition.LeftSide:
                    endPos.y -= zeroPos + Offset * handCards.Count;
                    break;
            }
            endPos.z = card.transform.position.z;

            card.transform.DOMove(endPos, 1);
            card.transform.DORotate(endRotation, 1);
        }

        public void setDropCardsAction()
        {

        }

        private void clickCardAction(ICardInfo info)
        {
            if (position != ePlayerPosition.MySelf) return;
            if (!info.isChoosed&&dropCardPool.Count < 5)
            {
                dropCardPool.Add(handCards[info.cardIndex]);
                handCards[info.cardIndex].isChoosed = true;
                handCards[info.cardIndex].transform.DOMoveY(transform.position.y + 0.01f, 0.1f);
            }
            else
            {
                dropCardPool.Remove(handCards[info.cardIndex]);
                handCards[info.cardIndex].isChoosed = false;
                handCards[info.cardIndex].transform.DOMoveY(transform.position.y, 0.1f);
            }
        }
    }
}
