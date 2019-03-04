using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Game.Component
{
    public class DropAreaComponent : MonoBehaviour
    {
        private List<CardComponent> lastDropCard = new List<CardComponent>();
        private float offset = 0.023f;
        private TweenCallback onDropFinishedCallback;

        public void Init(TweenCallback callback)
        {
            onDropFinishedCallback = callback;
        }

        public void GetDropCards(List<CardComponent> cards)
        {
            ClearDropArea();
            int index = 0;
            foreach (var card in cards)
            {
                card.transform.SetParent(transform);
                Vector3 endPos = transform.position;
                endPos.x += -(offset * (cards.Count - 1)) / 2 + offset * index;
                endPos.z = card.transform.position.z - 0.01f;
                Vector3 endRotation = transform.rotation.eulerAngles;
                endRotation.y += 180;
                if (index < cards.Count - 1)
                {
                    card.TSequence(endPos, endRotation, 1);
                }
                else
                {
                    card.TSequence(endPos, endRotation, 1, onDropFinishedCallback);
                }
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
