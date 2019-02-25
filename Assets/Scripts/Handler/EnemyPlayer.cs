using Assets.Scripts.Game.Component;
using Assets.Scripts.Game;
using Assets.Scripts.Type;
using System.Collections.Generic;

namespace Assets.Scripts.Handler
{
    public class EnemyPlayer
    {
        private PlayerComponent component;
        private HashSet<int> handCardFlowers = new HashSet<int>();
        private HashSet<int> handCardNumbers = new HashSet<int>();
        private List<Card> possibleCard = new List<Card>();

        public void Init(PlayerComponent comp)
        {
            component = comp;
        }

        public void CheckDropCard(DropResult result)
        {
            switch (result.cardType)
            {
                case eDropCardType.Single:
                    findPossibleCard(result.maxCard);
                    break;
                case eDropCardType.Pair:
                    break;
                case eDropCardType.TwoPair:
                    break;
                case eDropCardType.Straight:
                    break;
                case eDropCardType.FullHouse:
                    break;
                case eDropCardType.FourInOne:
                    break;
                case eDropCardType.FlushStraight:
                    break;
            }
        }

        private void findPossibleCard(Card enemyMaxCard)
        {
            foreach (KeyValuePair<int, CardComponent> card in component.handCards)
            {
                if (!enemyMaxCard.compareTo(card.Value)){
                    possibleCard.Add(card.Value.getCardInfo());
                }
            }
        }
    }
}