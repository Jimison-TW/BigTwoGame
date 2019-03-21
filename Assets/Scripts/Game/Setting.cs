using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Game
{
    [Serializable]
    public class Setting
    {
#pragma warning disable
        [SerializeField] private Image settingBody;
        [SerializeField] private Toggle musicToggle;

        private UnityAction<bool> toggleEvent;

        public void initToggleEvent(UnityAction<bool> action)
        {
            musicToggle.onValueChanged.AddListener(action);
        }

        public void Open()
        {
            settingBody.enabled = true;
        }

        public void Close()
        {
            settingBody.enabled = false;
        }
    }
}