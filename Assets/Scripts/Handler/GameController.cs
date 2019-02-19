using Assets.Scripts.Data;
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

        #endregion

        private void Start()
        {
            init();
            dealCardsToPlayer();
        }

        private void init()
        {
            gameData = new GameData();
            cardStack = new CardStack();
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
    }
}