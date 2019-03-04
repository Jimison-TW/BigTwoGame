using Assets.Scripts.Game;
using Assets.Scripts.Game.Component;
using Assets.Scripts.Game.Interface;
using Assets.Scripts.Type;
using System.Collections.Generic;
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
            HandCards tmpCards = playerCards;
            List<Card> willDrop = new List<Card>();
            int unsearchCount = playerCards.Count;
            Card lastSearchCard = enemyDropCards[enemyDropCards.Count - 1];
            do
            {
                if (unsearchCount < 2)
                {
                    willDrop = null;
                    break;
                }
                if (willDrop.Count > 0)
                {
                    Card card = tmpCards.findSameNumber(lastSearchCard.cardNumber);
                    if (card == null)
                    {
                        unsearchCount--;
                        willDrop.Clear();
                    }
                    else
                    {
                        willDrop.Add(card);
                        break;
                    }
                }
                else
                {
                    Card card = tmpCards.findBiggerIndex(lastSearchCard.cardIndex);
                    tmpCards.Drop(card.cardIndex);
                    lastSearchCard = card;
                    unsearchCount--;
                    willDrop.Add(card);
                }
            } while (willDrop.Count < 2);
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