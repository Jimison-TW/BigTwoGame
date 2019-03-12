using Assets.Scripts.Type;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Game.Component
{
    public class PlayerComponent : MonoBehaviour
    {
        public ePlayerPosition position;
        private Dictionary<int, CardComponent> allCards = new Dictionary<int, CardComponent>();
        private float zeroPos = -0.16f;
        private float Offset = 0.023f;

        /// <summary>
        /// 取得麻將牌的遊戲物件，設定點擊牌的Action，重新整理位置，並存入HandCards
        /// </summary>
        /// <param name="card"></param>
        public void SaveCard(CardComponent card)
        {
            card.setResetPosAction(new UnityAction<CardComponent, int>(resetHandCards));
            allCards.Add(card.getCardInfo().cardIndex, card);
            card.transform.SetParent(transform);
            resetHandCards(card, allCards.Count);
        }

        public List<CardComponent> GetCardComponent(List<Card> dropCards)
        {
            List<CardComponent> cardComponents = new List<CardComponent>();
            foreach (var card in dropCards)
            {
                cardComponents.Add(allCards[card.cardIndex]);
                allCards.Remove(card.cardIndex);
            }
            return cardComponents;
        }

        ///// <summary>
        ///// 將要出的牌放入牌池中
        ///// </summary>
        ///// <param name="card">單張牌的資訊</param>
        //public void setChosedCards(int cardIndex)
        //{
        //    Debug.Log("setChosedCards(int cardIndex)");
        //    chosedCards.Add(allCards[cardIndex]);
        //}

        ///// <summary>
        ///// 將要出的牌放入牌池中
        ///// </summary>
        ///// <param name="cards">多張牌的資訊，以List的形式傳入</param>
        //public void setChosedCards(List<Card> cards)
        //{
        //    Debug.Log($"setChosedCards(List<Card> cards) = {cards.Count}");
        //    foreach (var card in cards)
        //    {
        //        chosedCards.Add(allCards[card.cardIndex]);
        //    }
        //}

        /// <summary>
        /// 呼叫所有手牌中牌的resetPosEvent
        /// </summary>
        public void ResetCards()
        {
            int cardOrder = 0;
            foreach (KeyValuePair<int, CardComponent> item in allCards)
            {
                item.Value.Reset(cardOrder);
                cardOrder++;
            }
        }

        private void resetHandCards(CardComponent card, int cardOrder)
        {
            Vector3 endPos = transform.position;
            Vector3 endRotation = transform.rotation.eulerAngles;
            switch (position)
            {
                case ePlayerPosition.MySelf:
                    endPos.x += zeroPos + Offset * cardOrder;
                    //endRotation.y += 180;
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
            endRotation.y += 180;  //將所有牌翻開
            endPos.z = card.transform.position.z;

            card.TMove(endPos, 1);
            card.TRotate(endRotation, 1);
        }

    }
}
