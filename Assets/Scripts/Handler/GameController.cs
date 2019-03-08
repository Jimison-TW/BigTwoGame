using Assets.Scripts.Data;
using Assets.Scripts.Game;
using Assets.Scripts.Game.Component;
using Assets.Scripts.Type;
using System.Collections.Generic;
using System.Linq;
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
            //test
            List<Card> testList = new List<Card>();
            //testList.Add(new Card(0, 1, 3)); //梅花三
            //testList.Add(new Card(1, 2, 3)); //方塊三
            //testList.Add(new Card(2, 3, 3)); //紅心三
            testList.Add(new Card(3, 4, 3)); //黑桃三
            testList.Add(new Card(4, 1, 4)); //梅花四
            testList.Add(new Card(5, 2, 4)); //方塊四
            testList.Add(new Card(6, 3, 4)); //紅心四
            //testList.Add(new Card(7, 4, 4)); //黑桃四
            testList.Add(new Card(8, 1, 5)); //梅花五
            testList.Add(new Card(9, 2, 5)); //方塊五
            testList.Add(new Card(10, 3, 5)); //紅心五
            testList.Add(new Card(11, 4, 5)); //黑桃五
            testList.Add(new Card(12, 1, 6)); //梅花六
            //testList.Add(new Card(13, 2, 6)); //方塊六
            //testList.Add(new Card(14, 3, 6)); //紅心六
            testList.Add(new Card(15, 4, 6)); //黑桃六
            testList.Add(new Card(16, 1, 7)); //梅花七
            testList.Add(new Card(17, 2, 7)); //方塊七
            testList.Add(new Card(18, 3, 7)); //紅心七
            testList.Add(new Card(19, 4, 7)); //黑桃七

            List<Card> result = findCardGroup(testList, new Card(2, 3, 3), 2);
            foreach (var card in result)
            {
                Debug.Log(result[result.Count - 1].cardIndex);
            }

            //var result = from item in testList   //每一项                        
            //             group item by item.cardNumber into gro   //按项分组，没组就是gro                        
            //             orderby gro.Count() descending   //按照每组的数量进行排序                        
            //             select new { num = gro.Key, count = gro.Count(), max = gro.OrderBy(i=>i.cardIndex).Last() };   //返回匿名类型对象，输出这个组的值和这个值出现的次数 
            //result = result.Take(2).OrderByDescending(i => i.num);
            //foreach (var r in result)
            //{
            //    Debug.Log($"數字{r.num}出現了{r.count}次");
            //}

            //Card ca = testList.FindAll(c => c.cardNumber == result.ElementAt(0).num).Last();
            //Debug.Log($"ca.cardIndex = {result.ElementAt(0).max.cardIndex}");

            dealCardsToPlayer();
        }

        private void Update()
        {
            if (startNextTurn)
            {
                Debug.Log($"回合開始，輪到{isWhoseTurn}出牌");
                startNextTurn = false;
                if (isWhoseTurn != ePlayerPosition.MySelf)
                {
                    Player enemy = opponent[isWhoseTurn];
                    enemy.ThinkResult(dropArea.lastDropResult);
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
            _dropAreaComponent.Init(delegate
            {
                isWhoseTurn += ((int)isWhoseTurn < 3) ? 1 : -3;
                startNextTurn = true;
            });
            _cardStackComponent.CreateCard(cardStack.getAllNumber());
            int pos = 0;
            foreach (var pComponent in _playerComponents)
            {
                pComponent.Init(pos);
                opponent[(ePlayerPosition)pos] = new Player((ePlayerPosition)pos, pComponent);
                pos++;
            }
            Debug.Log("完成所有初始化流程");
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
            if (_playerComponents[whoseTurn].getDropCardPool() == null)
            {
                onPassClick(); //若dropCardPool是空的那就跳過回合
                return;
            }
            DropResult drop = dropArea.checkCardType(_playerComponents[whoseTurn].getDropCardPool());
            if (drop != null)
            {
                if (dropArea.canDropCard(drop))
                {
                    Debug.Log($"玩家{isWhoseTurn}丟出了{drop.cardType}，最大的牌是{(eCardFlower)drop.maxCard.cardFlower}" +
                        $"{(eCardNumber)drop.maxCard.cardNumber}");
                    dropArea.lastDropPosition = isWhoseTurn;
                    dropArea.lastDropResult = drop;
                    _dropAreaComponent.GetDropCards(_playerComponents[whoseTurn].getDropCardsBody());
                    _playerComponents[whoseTurn].ResetCards();
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

        public void onPassClick()
        {
            Debug.Log("玩家跳過回合");
            isWhoseTurn += ((int)isWhoseTurn < 3) ? 1 : -3;
            if (dropArea.lastDropPosition == isWhoseTurn) dropArea.lastDropResult = null;
            startNextTurn = true;
        }

        public List<Card> findCardGroup(List<Card> allCard,Card other, int count)
        {
            var cardGroup = from item in allCard   //每一项                        
                            group item by item.cardNumber into gro   //按项分组，没组就是gro                        
                            orderby gro.Count()    //按照每组的数量进行排序              
                            //返回匿名类型对象，输出这个组的值和这个值出现的次数以及index最大的那張牌           
                            select new
                            {
                                num = gro.Key,
                                count = gro.Count(),
                                result = gro.ToList(),
                                max = gro.OrderBy(i => i.cardIndex).Last()
                            };
            foreach (var element in cardGroup.Take(13))
            {
                Debug.Log($"element.num = {element.num},element.count = {element.count}");
                if (element.count >= count &&
                    element.max.cardNumber >= other.cardNumber &&
                    element.max.cardIndex >= other.cardIndex) return element.result;
            }

            return null;
        }
    }
}