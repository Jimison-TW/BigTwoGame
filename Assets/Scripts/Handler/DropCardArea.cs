using Assets.Scripts.Game;
using Assets.Scripts.Type;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Handler
{
    public class DropCardArea
    {
        private DropResult lastDrop = null;

        public bool canDropCard(DropResult result)
        {
            if (lastDrop == null) return true;
            switch (result.cardType)
            {
                case eDropCardType.Single:
                    if (result.maxCardIndex > lastDrop.maxCardIndex) return true;
                    else return false;
                case eDropCardType.Pair:
                    return false;
                case eDropCardType.Triple:
                    return false;
                case eDropCardType.TwoPair:
                    return false;
                case eDropCardType.Straight:
                    return false;
                case eDropCardType.FullHouse:
                    return false;
                case eDropCardType.Flush:
                    return false;
                case eDropCardType.FourInOne:
                    return false;
                case eDropCardType.FlushStraight:
                    return false;
                default: return false;
            }
        }
    }
}
