using Assets.Scripts.Game;
using System;
using UnityEngine;

namespace Assets.Scripts.Handler
{
    [Serializable]
    public class UIController
    {
        #region

#pragma warning disable
        [SerializeField] private Timer timer;
        [SerializeField] private PauseMenu menu;
        [SerializeField] private Dialog dialog;
        [SerializeField] private Setting setting;

        #endregion

        public void initUIEvent()
        {
            timer.initTimerEvent();
            menu.initMenuEvent(
            delegate
            {
                menu.Close();
                setting.Open();
            },
            delegate
            {
                OpenQuitDialog();
            });
            dialog.initDialogAction(delegate
            {
                menu.Close();
                setting.Close();
            });
            setting.initToggleEvent(delegate
            {
                Debug.Log("關閉音量");
            });
        }

        public void StartTimer()
        {
            timer.Start();
        }

        public void OpenMenu()
        {
            dialog.Close();
            setting.Close();
            menu.Open();
        }

        public void OpenMessageDialog(string content)
        {
            menu.Close();
            setting.Close();
            dialog.Open(content, delegate
             {
                 dialog.Close();
             });
        }

        public void OpenQuitDialog()
        {
            menu.Close();
            setting.Close();
            dialog.Open("Are you Sure to quit the Game?",
            delegate
            {
                Application.Quit();
            },
            delegate
            {
                dialog.Close();
            });
        }

        public void OpenSetting()
        {
            menu.Close();
            dialog.Close();
        }
    }
}