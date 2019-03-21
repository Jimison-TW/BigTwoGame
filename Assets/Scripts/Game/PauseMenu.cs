using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Game
{
    [Serializable]
    public class PauseMenu
    {
#pragma warning disable
        [SerializeField] private Image menuBody;
        [SerializeField] private Button resumeBtn;
        [SerializeField] private Button restartBtn;
        [SerializeField] private Button settingBtn;
        [SerializeField] private Button quitBtn;

        public void initMenuEvent(UnityAction onSettingClick, UnityAction onQuitClick)
        {
            resumeBtn.onClick.AddListener(Close);
            settingBtn.onClick.AddListener(onSettingClick);
            restartBtn.onClick.AddListener(delegate
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            });
            quitBtn.onClick.AddListener(delegate
            {
                onQuitClick.Invoke();
            });
        }

        public void Open()
        {
            menuBody.enabled = true;
        }

        public void Close()
        {
            menuBody.enabled = false;
        }
    }
}