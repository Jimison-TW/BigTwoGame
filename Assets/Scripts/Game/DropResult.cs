using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Type;

namespace Assets.Scripts.Game
{
    public class DropResult
    {
        public eDropCardType cardType { get; private set; }
        public List<Card> dropCards { get; private set; }
        public Card maxCard
        {
            get
            {
                Card card = null;
                switch (cardType)
                {
                    case eDropCardType.Single:
                    case eDropCardType.Pair:
                    case eDropCardType.Straight:
                    case eDropCardType.FlushStraight:
                        card = dropCards[dropCards.Count - 1];
                        break;
                    case eDropCardType.TwoPair:
                    case eDropCardType.FullHouse:
                    case eDropCardType.FourInOne:
                        var result = from item in dropCards   //每一项                        
                                     group item by item.cardValue into gro   //按项分组，没组就是gro                        
                                     orderby gro.Count() descending   //按照每组的数量进行排序              
                                     //返回匿名类型对象，输出这个组的值和这个值出现的次数以及index最大的那張牌           
                                     select new { num = gro.Key, count = gro.Count(), max = gro.OrderBy(i => i.cardIndex).Last() };
                        if (result.ElementAt(0).count == result.ElementAt(1).count)
                        {
                            result = result.Take(2).OrderBy(i => i.num);
                        }
                        card = result.ElementAt(0).max;
                        break;
                }
                return card;
            }

            private set { }
        }

        public DropResult(eDropCardType type, List<Card> cards)
        {
            cards.Sort((x, y) => { return x.cardIndex.CompareTo(y.cardIndex); });
            cardType = type;
            dropCards = new List<Card>(cards);
        }
    }
}