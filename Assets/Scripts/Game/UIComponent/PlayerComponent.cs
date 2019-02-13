using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.UIComponent
{
    public class PlayerComponent : MonoBehaviour
    {
        private List<CardComponent> cardObjs = new List<CardComponent>();

        public void resetCard(CardComponent card)
        {
            cardObjs.Add(card);
            card.cardTransform.SetParent(transform);
            card.cardTransform.Translate(transform.position);
        }


    }
}
