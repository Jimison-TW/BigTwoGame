using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game.Component
{
    public class UIComponent : MonoBehaviour
    {
        //Timer
        public Image timerBody;
        public Text timerTxt;

        //Message Box
        public Image messageBody;
        public Text messageTxt;

        //Main Menu
        public Image menuBody;
        public Button resumeBtn;
        public Button restartBtn;
        public Button settingBtn;
        public Button quitBtn;

        //Settings
        public Image settingBody;
        public Toggle musicSwitch;

        //Dialog
        public Image dialogBody;
        public Text dialogTxt;
        public Button dialogYesBtn;
        public Button dialogNoBtn;

        //Game 
        public Button dropBtn;
        public Button passBtn;
    }
}
