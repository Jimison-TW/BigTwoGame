using Assets.Scripts.Game.Component;
using Assets.Scripts.Type;
using System.Collections.Generic;

namespace Assets.Scripts.Game.Interface
{
    public interface IPlayerInfo
    {
        ePlayerPosition position { set; get; }
        List<CardComponent> dropCardPool { set; get; }
        Dictionary<int, CardComponent> handCards { set; get; }
    }
}