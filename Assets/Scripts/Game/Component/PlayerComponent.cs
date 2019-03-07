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

        /// <summary>
        /// 取得麻將牌的遊戲物件，設定點擊牌的Action，重新整理位置，並存入HandCards
        /// </summary>
        /// <param name="card"></param>
        public void GetCard(CardComponent card)
        {
            playerCards.Add(card);
            card.setClickCardAction(clickAction);
            card.setResetPosAction(resetPosAction);
            card.transform.SetParent(transform);
            resetHandCards(card, playerCards.Count);
        }

        /// <summary>
        /// 重新整理所有手牌的位置
        /// </summary>
        public void ResetCards()
        {
            playerCards.resetHandCards();
        }

        /// <summary>
        /// 將要出的牌放入牌池中
        /// </summary>
        /// <param name="card">單張牌的資訊</param>
        public void setDropCardPool(Card card)
        {
            if (card == null)
            {
                dropInfoPool.Clear();
            }
            else
            {
                dropInfoPool.Add(card);
            }
        }

        /// <summary>
        /// 將要出的牌放入牌池中
        /// </summary>
        /// <param name="cards">多張牌的資訊，以List的形式傳入</param>
        public void setDropCardPool(List<Card> cards)
        {
            if (cards == null)
            {
                dropInfoPool.Clear();
            }
            else
            {
                dropInfoPool = cards;
            }
        }

        /// <summary>
        /// 取得要出牌的資訊，提供給DropCardArea來判斷是否能出牌
        /// </summary>
        /// <returns>要出的手牌List，泛型型態為Card，如果沒有能出的牌，則回傳null</returns>
        public List<Card> getDropCardPool()
        {
            if (dropInfoPool.Count == 0) return null;
            return dropInfoPool;
        }

        /// <summary>
        /// 將牌的物件從HandCards取出
        /// </summary>
        /// <returns>要出的手牌List，泛型型態為CardComponent</returns>
        public List<CardComponent> getDropCardsBody()
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

            card.TMove(endPos, 1);
            card.TRotate(endRotation, 1);
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
