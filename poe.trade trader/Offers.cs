
namespace poe.trade_trader
{
    public class Offers
    {
        public string BuyCurrency, SellCurrency;
        public int BuyCurrencyAmount, SellCurrencyAmount, BuyStackSize, SellStackSize;
        public float buyratio, sellratio;
        public Offers(string BuyCurrency, string SellCurrency, int BuyCurrencyAmount, int SellCurrencyAmount, int BuyStackSize, int SellStackSize)
        {
            this.BuyCurrency = BuyCurrency;
            this.SellCurrency = SellCurrency;
            this.BuyCurrencyAmount = BuyCurrencyAmount;
            this.SellCurrencyAmount = SellCurrencyAmount;
            this.BuyStackSize = BuyStackSize;
            this.SellStackSize = SellStackSize;
            buyratio = (float)SellCurrencyAmount / BuyCurrencyAmount;
            sellratio = (float)BuyCurrencyAmount / SellCurrencyAmount;
        }
    }

}
