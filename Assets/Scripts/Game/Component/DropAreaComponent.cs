using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Component
{
    public class DropAreaComponent : MonoBehaviour
    {
        private List<CardComponent> lastDropCard = new List<CardComponent>();
        private float zeroPos = -0.04f;
        private float offset = 0.023f;

        public void GetDropCards(List<CardComponent> cards)
        {
            ClearDropArea();
            int index = 0;
            foreach (var card in cards)
            {
                Debug.Log(card.transform.localScale.x);
                card.transform.SetParent(transform);
                Vector3 endPos = transform.position;
                endPos.x += -(offset * (cards.Count - 1)) / 2 + offset * index;
                endPos.z = card.transform.position.z - 0.01f;
                Vector3 endRotation = transform.rotation.eulerAngles;
                endRotation.y += 180;

                card.transform.DOMove(endPos, 1);
                card.transform.DORotate(endRotation, 1);
                lastDropCard.Add(card);
                index++;
            }
        }

        public void ClearDropArea()
        {
            if (lastDropCard.Count == 0) return;
            foreach (var card in lastDropCard)
            {
                Destroy(card.gameObject);
            }
            lastDropCard.Clear();
        }
    }
}
