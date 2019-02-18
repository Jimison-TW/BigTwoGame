namespace Assets.Scripts.Type
{
    public interface ICardInfo
    {
        bool isChoosed { set; get; }
        int cardIndex { set; get; }
        eCardFlower cardFlower { set; get; }
        eCardNumber cardNumber { set; get; }
    }
}