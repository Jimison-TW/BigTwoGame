using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Game
{
    public class Settings
    {
        private UnityAction onOpenAction;
        private AudioSource audio;

        public void InitEvent(Toggle toggle,UnityAction action)
        {
            toggle.onValueChanged.AddListener(delegate
            {
                if (toggle.isOn)
                {
                    audio.volume = 0;
                }
                else
                {
                    audio.volume = 1;
                }
            });

            onOpenAction = action;
        }

        public void Open(Image body)
        {
            body.enabled = true;
            onOpenAction.Invoke();
        }


    }
}