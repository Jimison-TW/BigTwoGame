using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Handler
{
    public class CardStack
    {
        private List<int> numberPool = new List<int>();
        private int getCardPlayer = 1;
        private int whoIsFirst;

        public CardStack(UnityAction<int> whoFirstCallback)
        {
            for (int i = 0; i < 52; i++)
            {
                numberPool.Add(i);
            }

            randomCards();
            //回傳由誰開始
            whoFirstCallback.Invoke(numberPool.IndexOf(0) / 13);
            sortCards();
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

        /* 排序玩家手牌 */
        private void sortCards()
        {
            for (int i = 0; i < 4; i++)
            {
                List<int> tmpNumber = numberPool.GetRange(13 * i, 13);
                tmpNumber.Sort();
                for (int j = 0; j < 13; j++)
                {
                    int index = 13 * i + j;
                    numberPool[index] = tmpNumber[j];
                }
            }
        }

        public List<int> getAllNumber()
        {
            return numberPool;
        }

        /// <summary>
        /// 玩家取得13張手牌
        /// </summary>
        /// <returns></returns>
        public int[] getCards()
        {
            if (getCardPlayer > 4) return null;

            int[] cards = new int[13];
            for (int i = 0; i < 13; i++)
            {
                cards[i] = numberPool[i * getCardPlayer];
            }
            Debug.Log($"第{getCardPlayer}位玩家拿牌");
            getCardPlayer++;

            return cards;
        }
    }
}