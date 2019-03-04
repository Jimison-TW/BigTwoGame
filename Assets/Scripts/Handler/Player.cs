using Assets.Scripts.Game.Component;
using Assets.Scripts.Game;
using Assets.Scripts.Type;
using System.Collections.Generic;
using Assets.Scripts.Game.Interface;
using UnityEngine;
using System.Linq;

namespace Assets.Scripts.Handler
{
    public class Player : IPlayerInfo
    {
        public ePlayerPosition position { get; set; }
        public HandCards playerCards { get; set; }
        private PlayerComponent component;

        public Player(ePlayerPosition position, PlayerComponent comp)
        {
            this.position = position;
            component = comp;
            position = comp.position;
            playerCards = comp.playerCards;
        }

        public void DropCard()
        {
        }

        public void ThinkResult(DropResult result)
        {
            //如果是第一個出牌，直接出單張最小的
            if (result == null)
            {
                Card willDrop = playerCards.findMinCard();
                component.setDropCardPool(willDrop);
                return;
            }

            switch (result.cardType)
            {
                case eDropCardType.Single:
                    findSingle(result.maxCard);
                    break;
                case eDropCardType.Pair:
                    findPair(result.dropCards);
                    break;
                case eDropCardType.TwoPair:
                    findTwoPair(result.maxCard);
                    break;
                case eDropCardType.Straight:
                    findStraight(result.maxCard);
                    break;
                case eDropCardType.FullHouse:
                    findFullHouse(result.maxCard);
                    break;
                case eDropCardType.FourInOne:
                    findFourInOne(result.maxCard);
                    break;
                case eDropCardType.FlushStraight:
                    findFlushStraight(result.maxCard);
                    break;
            }
        }

        private void findSingle(Card enemyDropCard)
        {
            Card willDrop = playerCards.findBiggerIndex(enemyDropCard.cardIndex);
            component.setDropCardPool(willDrop);
        }

        private void findPair(List<Card> enemyDropCards)
        {
            enemyDropCards.OrderBy(i => i.cardIndex);
            List<Card> willDrop = new List<Card>();
            int unsearchCount = playerCards.Count;
            int lastSearchIndex = enemyDropCards[enemyDropCards.Count - 1].cardIndex;
            do
            {
                if (unsearchCount < 2)
                {
                    willDrop = null;
                    break;
                }
                if (willDrop.Count > 0)
                {

                }
                else
                {
                    Card card = playerCards.findBiggerIndex(lastSearchIndex);
                    lastSearchIndex = card.cardIndex;
                    willDrop.Add(card);
                }
            } while (willDrop == null);
            component.setDropCardPool(willDrop);
        }

        private void findTwoPair(Card enemyMaxCard)
        {

        }

        private void findStraight(Card enemyMaxCard)
        {

        }

        private void findFullHouse(Card enemyMaxCard)
        {

        }

        private void findFourInOne(Card enemyMaxCard)
        {

        }

        private void findFlushStraight(Card enemyMaxCard)
        {

        }
    }
}