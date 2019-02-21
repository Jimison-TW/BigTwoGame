using System.Collections.Generic;
using Assets.Scripts.Type;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class DropResult
    {
        public eDropCardType cardType { get; private set; }
        public Card maxCard { get; private set; }

        public void setResult(eDropCardType type,Card card)
        {
            cardType = type;
            maxCard = card;
        }
    }
}