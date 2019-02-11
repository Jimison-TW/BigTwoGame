using UnityEngine;

namespace Assets.Scripts.Game.UIComponent
{
    public class PlayerComponent : MonoBehaviour
    {
        private CardComponent[] cardObjs;

        public void resetCards(CardComponent[] cards)
        {
            cardObjs = cards;
            foreach(var card in cardObjs)
            {
                card.cardTransform.Translate(transform.position);
            }
        }


    }
}
