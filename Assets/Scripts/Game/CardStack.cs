using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class CardStack
    {
        private List<int> numberPool = new List<int>();
        private int getCardPlayer = 1;

        private void Start()
        {
            init();
        }

        private void init()
        {
            for (int i = 0; i < 52; i++)
            {
                numberPool.Add(i);
            }

            randomCards();
        }

        /* 隨機排列52張牌的順序 */
        private void randomCards()
        {
            for (int j = 0; j < 52; j++)
            {
                int tmp = numberPool[j];
                int intRnd = Mathf.FloorToInt(Random.value * 52);
                numberPool[j] = numberPool[intRnd];
                numberPool[intRnd] = tmp;
            }
        }

        /// <summary>
        /// 玩家取得13張手牌
        /// </summary>
        /// <returns></returns>
        public List<int> getCards()
        {
            if (getCardPlayer > 4) return null;

            List<int> cards = new List<int>();
            for (int i = 0; i < 13; i++)
            {
                cards.Add(numberPool[i * getCardPlayer]);
            }
            Debug.Log($"第{getCardPlayer}位玩家拿牌");
            getCardPlayer++;

            return cards;
        }

        
    }
}