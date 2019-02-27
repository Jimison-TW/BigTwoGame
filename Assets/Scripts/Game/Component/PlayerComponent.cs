using Assets.Scripts.Game.Interface;
using Assets.Scripts.Type;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Game.Component
{
    public class PlayerComponent : MonoBehaviour, IPlayerInfo
    {
        public ePlayerPosition position { set; get; }
        public HandCards playerCards { set; get; }
        private List<Card> dropInfoPool;
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
            dropInfoPool = new List<Card>();
        }

        public void GetCard(CardComponent card)
        {
            playerCards.Add(card);
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
            foreach (var drop in dropInfoPool)
            {
                CardComponent card = playerCards.Drop(drop.cardIndex);
                dropArray.Add(card);
            }
            dropInfoPool.Clear();

            return dropArray;
        }

        public void setDropCardPool(List<Card> cards)
        {
            dropInfoPool = cards;
            //dropInfoPool = cards.Cast<Card>().ToList();
        }

        public void setDropCardPool(Card card)
        {
            dropInfoPool.Add(card);
        }

        public List<Card> getDropCardsData()
        {
            if (dropInfoPool.Count == 0) return null;
            List<Card> dropCards = new List<Card>();
            foreach (var info in dropInfoPool)
            {
                dropCards.Add(new Card(info));
            }
            return dropCards;
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

            card.Move(endPos, 1);
            card.Rotate(endRotation, 1);
        }

        private void clickCardAction(bool choosed, Card card)
        {
            if (position != ePlayerPosition.MySelf) return;
            if (!choosed && dropInfoPool.Count < 5)
            {
                dropInfoPool.Add(card);
                playerCards.Find(card.cardIndex).isChoosed = true;
                playerCards.Find(card.cardIndex).transform.DOMoveY(transform.position.y + 0.01f, 0.1f);
            }
            else
            {
                dropInfoPool.Remove(dropInfoPool.Find((Card c) => c.cardIndex == card.cardIndex));
                playerCards.Find(card.cardIndex).isChoosed = false;
                playerCards.Find(card.cardIndex).transform.DOMoveY(transform.position.y, 0.1f);
            }
        }
    }
}
