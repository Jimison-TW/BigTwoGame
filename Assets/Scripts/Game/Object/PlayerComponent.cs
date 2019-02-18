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
        private List<CardComponent> chosedCardPool = new List<CardComponent>();
        private Dictionary<int, CardComponent> handCards = new Dictionary<int, CardComponent>();

        public void resetHandCard(CardComponent card)
        {
            card.setClickCardAction(new UnityAction<ICardInfo>(clickCardAction));
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

            handCards[card.cardIndex] = card;
        }

        private void clickCardAction(ICardInfo info)
        {
            if (!info.isChoosed && position == ePlayerPosition.MySelf)
            {
                if (chosedCardPool.Count >= 5) return;
                chosedCardPool.Add(handCards[info.cardIndex]);
                handCards[info.cardIndex].isChoosed = true;
                handCards[info.cardIndex].transform.DOMoveY(transform.position.y + 0.01f, 0.1f);
                Debug.Log("Pool Count" + chosedCardPool.Count);
            }
            else
            {
                if (chosedCardPool.Count <= 0) return;
                chosedCardPool.Remove(handCards[info.cardIndex]);
                handCards[info.cardIndex].isChoosed = false;
                handCards[info.cardIndex].transform.DOMoveY(transform.position.y, 0.1f);
                Debug.Log("Pool Count" + chosedCardPool.Count);
            }
        }
    }
}
