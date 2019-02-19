namespace Assets.Scripts.Type
{
    public interface ICardInfo
    {
        bool isChoosed { set; get; }
        int cardIndex { set; get; }
        int cardFlower { set; get; }
        int cardNumber { set; get; }

        bool isBigger(ICardInfo info);
    }
}