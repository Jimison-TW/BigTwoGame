using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Game
{
    [Serializable]
    public class Timer
    {
        [SerializeField] private Image timerBody;
        [SerializeField] private Text timerTxt;

        private int remainTime;
        private UnityAction onTimesUpCallback;
        
    }
}