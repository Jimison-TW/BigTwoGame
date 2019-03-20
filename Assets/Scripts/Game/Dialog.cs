using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Game
{
    [Serializable]
    public class Dialog
    {
        [SerializeField] private Image dialogBody;
        [SerializeField] private Text dialogTxt;
        [SerializeField] private Button dialogYesBtn;
        [SerializeField] private Button dialogNoBtn;

        private UnityAction onYesClickAction;
        private UnityAction onNoClickAction;

        public void Open(string content, UnityAction yesAction)
        {
            dialogBody.enabled = true;
            dialogTxt.text = content;
            dialogYesBtn.onClick.AddListener(yesAction);
            onYesClickAction = yesAction;
        }

        public void Open(string content, UnityAction yesAction, UnityAction noAction)
        {
            dialogBody.enabled = true;
            dialogTxt.text = content;
            dialogYesBtn.onClick.AddListener(yesAction);
            onYesClickAction = yesAction;
            dialogNoBtn.onClick.AddListener(noAction);
            onNoClickAction = noAction;
        }

        public void Close()
        {
            dialogBody.enabled = false;
            dialogTxt.text = "";
            if (onYesClickAction != null)
            {
                dialogYesBtn.onClick.RemoveListener(onYesClickAction);
                onYesClickAction = null;
            }
            if (onNoClickAction != null)
            {
                dialogNoBtn.onClick.RemoveListener(onNoClickAction);
                onNoClickAction = null;
            }
        }
    }
}