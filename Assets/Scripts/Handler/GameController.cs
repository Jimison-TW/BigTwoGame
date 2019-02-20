using Assets.Scripts.Data;
using Assets.Scripts.Game;
using Assets.Scripts.Game.Component;
using UnityEngine;

namespace Assets.Scripts.Handler
{
    public class GameController : MonoBehaviour
    {
        #region 外部物件

        [SerializeField] private CardStackComponent _cardStackComponent = null;
        [SerializeField] private PlayerComponent[] _playerComponents = null;

        #endregion

        #region 內部物件

        private GameData gameData;
        private CardStack cardStack;
        private DropCardArea dropArea;

        private int isWhoseTurn = 0;

        #endregion

        private void Awake()
        {
            init();
        }

        private void Start()
        {
            dealCardsToPlayer();
        }

        private void init()
        {
            gameData = new GameData();
            cardStack = new CardStack();
            dropArea = new DropCardArea();
            _cardStackComponent.createCardStack(cardStack.getAllNumber());
        }

        private void dealCardsToPlayer()
        {
            for (int i = 0; i < gameData.playerCount; i++)
            {
                Debug.Log("開始發牌給玩家" + _playerComponents[i].name);
                _cardStackComponent.dealCards(_playerComponents[i], i);
            }
        }

        public void onDropCardClick()
        {
            DropResult drop = dropArea.checkCardType(_playerComponents[isWhoseTurn].dropCardPool);
            if (drop != null)
            {
                if (dropArea.canDropCard(drop))
                {
                    
                }
                else
                {
                    //玩家出牌未超過上一位玩家
                }
            }
            else
            {
                //玩家牌型選擇錯誤的處理
            }
        }
    }
}