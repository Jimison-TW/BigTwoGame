using Assets.Scripts.Game;
using Assets.Scripts.Type;
using System.Collections.Generic;

namespace Assets.Scripts.Handler
{
    public class Player
    {
        public DropResult checkCardType(List<Card> cards)
        {
            DropResult result = new DropResult();
            if (cards.Count == 0 || cards.Count == 4) return null;
            switch (cards.Count)
            {
                case 1:
                    setInfoToResult(result, eDropCardType.Single, cards[0]);
                    return result;
                case 2:
                    if (cards[0].cardNumber != cards[1].cardNumber) return null;
                    setInfoToResult(result, eDropCardType.Pair, getMaxIdCard(cards));
                    return result;
                case 3:
                    setInfoToResult(result, eDropCardType.Triple, cards[0]);
                    return result;
                case 5:
                    setInfoToResult(result, eDropCardType.Pair, cards[0]);
                    return result;
                default: return null;
            }
        }

        private Card getMaxIdCard(List<Card> cards)
        {
            Card tmpCard = null;
            foreach (var card in cards)
            {
                if (card == null)
                {
                    tmpCard = card;
                    break;
                }
                else
                {
                    tmpCard = (tmpCard.isBigger(card)) ? tmpCard : card;
                }
            }
            return tmpCard;
        }

        private void setInfoToResult(DropResult result, eDropCardType type, ICardInfo cardInfo)
        {
            result.setResult(type, cardInfo.cardIndex, cardInfo.cardFlower, cardInfo.cardNumber);
        }
    }
}