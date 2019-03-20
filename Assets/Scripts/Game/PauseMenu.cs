using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game
{
    [Serializable]
    public class PauseMenu
    {
        [SerializeField] private Image menuBody;
        [SerializeField] private Button resumeBtn;
        [SerializeField] private Button restartBtn;
        [SerializeField] private Button settingBtn;
        [SerializeField] private Button quitBtn;
        
    }
}