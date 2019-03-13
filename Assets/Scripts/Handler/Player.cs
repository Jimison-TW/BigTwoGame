using Assets.Scripts.Game;
using Assets.Scripts.Game.Component;
using Assets.Scripts.Type;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

namespace Assets.Scripts.Handler
{
    public class Player
    {
        public ePlayerPosition position { get; set; }
        public HandCards handCards { get; set; }
        private PlayerComponent component;

        public Player(ePlayerPosition position, PlayerComponent comp)
        {
            this.position = position;
            component = comp;
            position = comp.position;
            handCards = new HandCards();
        }

        public void GetCard(CardComponent card)
        {
            card.setClickCardAction(new UnityEngine.Events.UnityAction<bool, CardComponent>(clickCardAction));
            handCards.Add(card.getCardInfo());
            component.SaveCard(card);
        }

        /// <summary>
        /// 將牌的物件與資料取出並移除
        /// </summary>
        /// <returns>要出的手牌List，泛型型態為CardComponent</returns>
        public List<CardComponent> DropCard()
        {
            List<CardComponent> dropArray = component.GetCardComponent(handCards.willDrop);
            handCards.Remove();
            return dropArray;
        }

        /// <summary>
        /// 取得要出牌的資訊，提供給DropCardArea來判斷是否能出牌
        /// </summary>
        /// <returns>要出的手牌List，泛型型態為Card，如果沒有能出的牌，則回傳null</returns>
        public List<Card> getDropInfo()
        {
            if (handCards.willDrop != null && handCards.willDrop.Count == 0) return null;
            return handCards.willDrop;
        }

        public void ThinkResult(DropResult result)
        {
            //如果是第一個出牌，直接出單張最小的
            if (result == null)
            {
                handCards.findSingle(handCards.findMinCard());
                return;
            }
            
            Debug.Log($"上一個出牌的牌型是{result.cardType}，最大的牌是{result.maxCard.cardFlower}{(int)result.maxCard.cardNumber}");

            switch (result.cardType)
            {
                case eDropCardType.Single:
                    handCards.findSingle(result.maxCard);
                    break;
                case eDropCardType.Pair:
                    handCards.findPair(result.maxCard);
                    break;
                case eDropCardType.TwoPair:
                    handCards.findTwoPair(result.maxCard);
                    break;
                case eDropCardType.Straight:
                    handCards.findStraight(result.maxCard);
                    break;
                case eDropCardType.FullHouse:
                    handCards.findFullHouse(result.maxCard);
                    break;
                case eDropCardType.FourInOne:
                    handCards.findFourInOne(result.maxCard);
                    break;
                case eDropCardType.FlushStraight:
                    handCards.findFlushStraight(result.maxCard);
                    break;
            }
        }

        private void clickCardAction(bool choosed, CardComponent card)
        {
            if (position != ePlayerPosition.MySelf) return;
            if (!choosed && handCards.willDrop.Count < 5)
            {
                handCards.willDrop.Add(card.getCardInfo());
                card.isChoosed = true;
                card.transform.DOMoveY(component.transform.position.y + 0.01f, 0.1f);
            }
            else
            {
                handCards.willDrop.Remove(card.getCardInfo());
                card.isChoosed = false;
                card.transform.DOMoveY(component.transform.position.y, 0.1f);
            }
        }
    }
}