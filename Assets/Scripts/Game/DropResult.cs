using System.Collections.Generic;
using Assets.Scripts.Type;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class DropResult
    {
        public eDropCardType cardType { get; private set; }
        public int maxCardIndex { get; private set; }
        public int maxCardFlower { get; private set; }
        public int maxCardNumber { get; private set; }
        
        public void setResult(eDropCardType type,int maxId,int maxFlower,int maxNumber)
        {

        }
    }
}