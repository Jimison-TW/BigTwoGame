using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.UIComponent
{
    public class PlayerComponent : MonoBehaviour
    {
        public float firstPosX = -0.1f;
        public float xOffset = 0.1f;
        private List<CardComponent> handCards = new List<CardComponent>();

        public void resetCard(CardComponent card)
        {
            handCards.Add(card);
            card.cardTransform.SetParent(transform);
            Vector3 endPos = transform.position;
            endPos.x += firstPosX + xOffset * handCards.Count;
            Debug.Log(endPos);
            card.cardTransform.DOMove(endPos, 1);
        }


    }
}
