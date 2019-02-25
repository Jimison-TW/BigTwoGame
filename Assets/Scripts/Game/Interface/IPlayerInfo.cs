using Assets.Scripts.Game.Component;
using Assets.Scripts.Type;
using System.Collections.Generic;

namespace Assets.Scripts.Game.Interface
{
    public interface IPlayerInfo
    {
        ePlayerPosition position { set; get; }
        HandCards playerCards { set; get; }
    }
}