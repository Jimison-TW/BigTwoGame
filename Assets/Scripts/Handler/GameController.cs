﻿using Assets.Scripts.Data;
using Assets.Scripts.Game;
using Assets.Scripts.Game.Component;
using Assets.Scripts.Type;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Handler
{
    public class GameController : MonoBehaviour
    {
        #region 外部物件

        [SerializeField] private CardStackComponent _cardStackComponent = null;
        [SerializeField] private PlayerComponent[] _playerComponents = null;
        [SerializeField] private DropAreaComponent _dropAreaComponent = null;

        #endregion

        #region 內部物件

        private GameData gameData;
        private CardStack cardStack;
        private DropCardArea dropArea;

        private ePlayerPosition isWhoseTurn = 0;
        public bool startNextTurn = false;
        private Dictionary<ePlayerPosition, Player> opponent = new Dictionary<ePlayerPosition, Player>();

        #endregion

        private void Awake()
        {
            initialUtils();
        }

        private void Start()
        {
            dealCardsToPlayer();
        }

        private void Update()
        {
            //if (startNextTurn)
            //{
            //    startNextTurn = false;
            //    if (isWhoseTurn != ePlayerPosition.MySelf)
            //    {
            //        Debug.Log(isWhoseTurn);
            //        Player enemy = opponent[isWhoseTurn];
            //        enemy.ThinkResult(dropArea.getLastDrop());
            //        onDropCardClick();
            //    }
            //}
        }

        private void LateUpdate()
        {
            if (startNextTurn)
            {
                startNextTurn = false;
                if (isWhoseTurn != ePlayerPosition.MySelf)
                {
                    Debug.Log(isWhoseTurn);
                    Player enemy = opponent[isWhoseTurn];
                    enemy.ThinkResult(dropArea.getLastDrop());
                    onDropCardClick();
                }
            }
        }

        private void initialUtils()
        {
            gameData = new GameData();
            cardStack = new CardStack(delegate (int whoFirst) { isWhoseTurn = (ePlayerPosition)whoFirst; });
            //cardStack = new CardStack(delegate (int whoFirst) { isWhoseTurn = 0; });
            dropArea = new DropCardArea();
            _cardStackComponent.CreateCard(cardStack.getAllNumber());
            int pos = 0;
            foreach (var pComponent in _playerComponents)
            {
                Debug.Log((ePlayerPosition)pos);
                opponent[(ePlayerPosition)pos] = new Player((ePlayerPosition)pos, pComponent);
                pos++;
            }
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
            int whoseTurn = (int)isWhoseTurn;
            if (_playerComponents[whoseTurn].getDropCardsData() == null) return;
            DropResult drop = dropArea.checkCardType(_playerComponents[whoseTurn].getDropCardsData());
            if (drop != null)
            {
                if (dropArea.canDropCard(drop))
                {
                    Debug.Log($"玩家丟出了{drop.cardType}，最大的牌是{(eCardFlower)drop.maxCard.cardFlower}" +
                        $"{(eCardNumber)drop.maxCard.cardNumber}");
                    _dropAreaComponent.GetDropCards(_playerComponents[whoseTurn].getDropCards());
                    _playerComponents[whoseTurn].CardReset();
                }
                else
                {
                    //玩家出牌未超過上一位玩家
                    Debug.LogWarning("玩家出牌未超過上一位玩家");
                }
            }
            else
            {
                //玩家牌型選擇錯誤的處理
                Debug.LogWarning("玩家牌型選擇錯誤");
            }
        }
    }
}