using Assets.Scripts.Game.Interface;
using Assets.Scripts.Type;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Game.Component
{
    public class PlayerComponent : MonoBehaviour, IPlayerInfo
    {
        public ePlayerPosition position { set; get; }
        private float zeroPos = -0.16f;
        private float Offset = 0.023f;
        public List<CardComponent> dropCardPool { set; get; }
        public Dictionary<int, CardComponent> handCards { set; get; }
        private UnityAction<bool, CardComponent> clickAction;

        public void init(int playerPos)
        {
            position = (ePlayerPosition)playerPos;
            clickAction = new UnityAction<bool, CardComponent>(clickCardAction);
            dropCardPool = new List<CardComponent>();
            handCards = new Dictionary<int, CardComponent>();
        }

        public void getCards(CardComponent card)
        {
            handCards[card.getCardIndex()] = card;
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

        private void clickCardAction(bool choosed, CardComponent card)
        {
            if (position != ePlayerPosition.MySelf) return;
            if (!choosed && dropCardPool.Count < 5)
            {
                dropCardPool.Add(card);
                handCards[card.getCardIndex()].isChoosed = true;
                handCards[card.getCardIndex()].transform.DOMoveY(transform.position.y + 0.01f, 0.1f);
            }
            else
            {
                dropCardPool.Remove(card);
                handCards[card.getCardIndex()].isChoosed = false;
                handCards[card.getCardIndex()].transform.DOMoveY(transform.position.y, 0.1f);
            }
        }
    }
}
