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

        public void Init(int playerPos)
        {
            position = (ePlayerPosition)playerPos;
            clickAction = new UnityAction<bool, CardComponent>(clickCardAction);
            dropCardPool = new List<CardComponent>();
            handCards = new Dictionary<int, CardComponent>();
        }

        public void GetCard(CardComponent card)
        {
            handCards[card.cardIndex] = card;
            card.setClickCardAction(clickAction);
            card.transform.SetParent(transform);
            resetHandCards(card, handCards.Count);
        }

        public void DropCards()
        {
            foreach (var drop in dropCardPool)
            {
                handCards.Remove(drop.cardIndex);
            }
            dropCardPool.Clear();
            int cardOrder = 0;
            foreach (KeyValuePair<int, CardComponent> item in handCards)
            {
                resetHandCards(item.Value,cardOrder);
                cardOrder++;
            }
        }

        public List<Card> getDropCardsData()
        {
            if (dropCardPool.Count == 0) return null;
            List<Card> dropData = new List<Card>();
            foreach (var card in dropCardPool)
            {
                dropData.Add(card.getCardInfo());
            }
            return dropData;
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

        private void clickCardAction(bool choosed, CardComponent card)
        {
            if (position != ePlayerPosition.MySelf) return;
            if (!choosed && dropCardPool.Count < 5)
            {
                dropCardPool.Add(card);
                handCards[card.cardIndex].isChoosed = true;
                handCards[card.cardIndex].transform.DOMoveY(transform.position.y + 0.01f, 0.1f);
            }
            else
            {
                dropCardPool.Remove(card);
                handCards[card.cardIndex].isChoosed = false;
                handCards[card.cardIndex].transform.DOMoveY(transform.position.y, 0.1f);
            }
        }
    }
}
