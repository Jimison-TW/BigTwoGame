using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Handler
{
    public class CardStack
    {
        private List<int> numberPool = new List<int>();
        private int whoIsFirst;

        //自訂玩家手牌，想要什麼牌直接在下方填入cardIndex(0-51)
        private int[] myCardNumber = { 0, 1, 2, 6, 5};

        public CardStack(UnityAction<int> whoFirstCallback)
        {
            for (int i = 0; i < 52; i++)
            {
                numberPool.Add(i);
            }

            randomCards();
            foreach (var i in numberPool)
            {
                Debug.Log(i);
            }
            //回傳由誰開始
            //whoFirstCallback.Invoke(numberPool.IndexOf(0) / 13);  //隨機玩家開始出牌
            whoFirstCallback.Invoke(0);  //由玩家開始出牌
            sortCards();
        }

        /* 隨機排列52張牌的順序 */
        private void randomCards()
        {
            int index = 0;
            for (int i = 0; i < myCardNumber.Length; i++)
            {
                int tmp = numberPool[i];
                numberPool[i] = numberPool[myCardNumber[i]];
                numberPool[myCardNumber[i]] = tmp;
                index++;
            }
            for (int j = index; j < 52; j++)
            {
                int tmp = numberPool[j];
                int intRnd = Mathf.FloorToInt(Random.value * (52 - index));
                numberPool[j] = numberPool[intRnd + index];
                numberPool[intRnd + index] = tmp;
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

        public List<int> getNumberPool()
        {
            return numberPool;
        }
    }
}