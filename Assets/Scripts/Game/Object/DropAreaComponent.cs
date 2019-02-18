using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Object
{
    public class DropAreaComponent : MonoBehaviour
    {
        public void resetDropCard(List<CardComponent> cards)
        {
            foreach (var card in cards)
            {
                card.transform.SetParent(transform);
                Vector3 endPos = transform.position;
                Vector3 endRotation = transform.rotation.eulerAngles;

                card.transform.DOMove(endPos, 1);
                card.transform.DORotate(endRotation, 1);
            }
        }
    }
}
