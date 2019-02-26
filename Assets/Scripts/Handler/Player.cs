using Assets.Scripts.Game.Component;
using Assets.Scripts.Game;
using Assets.Scripts.Type;
using System.Collections.Generic;
using Assets.Scripts.Game.Interface;

namespace Assets.Scripts.Handler
{
    public class Player : IPlayerInfo
    {
        public ePlayerPosition position { get; set; }
        public HandCards playerCards { get; set; }
        private PlayerComponent component;

        public void Init(PlayerComponent comp)
        {
            component = comp;
            position = comp.position;
            playerCards = comp.playerCards;
        }

        public void DropCard()
        {
            
        }

        public void ThinkResult(DropResult result)
        {
            switch (result.cardType)
            {
                case eDropCardType.Single:
                    findSingle(result.maxCard);
                    break;
                case eDropCardType.Pair:
                    findPair(result.maxCard);
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

        private void findSingle(Card enemyMaxCard)
        {
            Card willDrop = playerCards.FindCardById(enemyMaxCard.cardIndex);
            component.setDropCardPool(willDrop);
        }

        private void findPair(Card enemyMaxCard)
        {

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