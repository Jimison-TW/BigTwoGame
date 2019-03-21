using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Game
{
    [Serializable]
    public class Timer
    {
#pragma warning disable
        [SerializeField] private Image timerBody;
        [SerializeField] private Text timerTxt;

        private int remainTime;
        private UnityAction onTimesUpCallback;

        public void initTimerEvent()
        {

        }

        public void Start()
        {
            timerBody.enabled = true;
        }

        public void Pause()
        {

        }

        public void Stop()
        {
            timerBody.enabled = false;
            remainTime = 0;
        }
    }
}