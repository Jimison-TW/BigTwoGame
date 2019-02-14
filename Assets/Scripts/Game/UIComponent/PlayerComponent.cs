using Assets.Scripts.Type;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.UIComponent
{
    public class PlayerComponent : MonoBehaviour
    {
        public ePlayerPosition position;
        private float zeroPos = -0.1f;
        private float Offset = 0.05f;
        private List<CardComponent> handCards = new List<CardComponent>();

        public void resetCard(CardComponent card)
        {
            handCards.Add(card);
            card.cardTransform.SetParent(transform);
            Vector3 endPos = transform.position;
            switch (position)
            {
                case ePlayerPosition.MySelf:
                    endPos.x += zeroPos + Offset * handCards.Count;
                    break;
                case ePlayerPosition.RightSide:
                    endPos.y -= zeroPos + Offset * handCards.Count;
                    break;
                case ePlayerPosition.OppositeSide:
                    endPos.x -= zeroPos + Offset * handCards.Count;
                    break;
                case ePlayerPosition.LeftSide:
                    endPos.y += zeroPos + Offset * handCards.Count;
                    break;
            }
            Debug.Log(endPos);
            card.cardTransform.DOMove(endPos, 1);
            card.cardTransform.DORotate(transform.rotation.eulerAngles, 1);
        }


    }
}
