using Assets.Scripts.Data;
using Assets.Scripts.Game;
using Assets.Scripts.Game.UIComponent;
using UnityEngine;

namespace Assets.Scripts.Handler
{
    public class GameController : MonoBehaviour
    {
        #region 外部物件

        [SerializeField] private CardStackComponent _cardStackComponent;
        [SerializeField] private PlayerComponent[] _playerComponent;

        #endregion

        #region 內部物件

        private GameData gameData;
        private CardStack cardStack;

        #endregion

        private void Start()
        {
            init();
        }

        private void init()
        {
            gameData = new GameData();
            cardStack = new CardStack();
            _cardStackComponent.createCardStack(cardStack.getAllNumber());
        }

        private void dealCardsToPlayer()
        {
            _cardStackComponent.dealCards(_playerComponent);
        }
    }
}