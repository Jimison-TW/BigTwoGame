using UnityEngine;
using Assets.Scripts.Type;

namespace Assets.Scripts.Game
{
    public class CardComponent : MonoBehaviour
    {
        public int cardIndex { set; get; }
        public eCardFlower flower;
        public eCardNumber number;
    }
}
