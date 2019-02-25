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
        public HandCards playerCards { set; get; }
        public List<Card> dropCardPool = new List<Card>(); 
        private float zeroPos = -0.16f;
        private float Offset = 0.023f;

        private UnityAction<bool, Card> clickAction;
        private UnityAction<CardComponent, int> resetPosAction;

        public void Init(int playerPos)
        {
            position = (ePlayerPosition)playerPos;
            clickAction = new UnityAction<bool, Card>(clickCardAction);
            resetPosAction = new UnityAction<CardComponent, int>(resetHandCards);
            playerCards = new HandCards();
        }

        public void GetCard(CardComponent card)
        {
            playerCards.Get(card);
            card.setClickCardAction(clickAction);
            card.setResetPosAction(resetPosAction);
            card.transform.SetParent(transform);
            resetHandCards(card, playerCards.Count);
        }

        public void CardReset()
        {
            playerCards.resetHandCards();
        }

        public List<CardComponent> getDropCards()
        {
            List<CardComponent> dropArray = new List<CardComponent>();
            foreach (var drop in dropCardPool)
            {
                CardComponent card = playerCards.Drop(drop.cardIndex);
                dropArray.Add(card);
            }
            dropCardPool.Clear();

            return dropArray;
        }

        public List<Card> getDropCardsData()
        {
            if (dropCardPool.Count == 0) return null;
            return dropCardPool;
        }

        private void resetHandCards(CardComponent card, int cardOrder)
        {
            Vector3 endPos = transform.position;
            Vector3 endRotation = transform.rotation.eulerAngles;
            switch (position)
            {
                case ePlayerPosition.MySelf:
                    endPos.x += zeroPos + Offset * cardOrder;
                    endRotation.y += 180;
                    break;
                case ePlayerPosition.RightSide:
                    endPos.y += zeroPos + Offset * cardOrder;
                    break;
                case ePlayerPosition.OppositeSide:
                    endPos.x -= zeroPos + Offset * cardOrder;
                    break;
                case ePlayerPosition.LeftSide:
                    endPos.y -= zeroPos + Offset * cardOrder;
                    break;
            }
            endPos.z = card.transform.position.z;

            card.transform.DOMove(endPos, 1);
            card.transform.DORotate(endRotation, 1);
        }

        private void clickCardAction(bool choosed, Card card)
        {
            if (position != ePlayerPosition.MySelf) return;
            if (!choosed && dropCardPool.Count < 5)
            {
                dropCardPool.Add(card);
                playerCards.Find(card.cardIndex).isChoosed = true;
                playerCards.Find(card.cardIndex).transform.DOMoveY(transform.position.y + 0.01f, 0.1f);
            }
            else
            {
                dropCardPool.Remove(card);
                playerCards.Find(card.cardIndex).isChoosed = false;
                playerCards.Find(card.cardIndex).transform.DOMoveY(transform.position.y, 0.1f);
            }
        }
    }
}
