using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Localization;
using Terraria.ModLoader;
using testMod1.Content.Items.Misc;

namespace testMod1.Common.Systems
{
    public class GoldCurrencySystem : CustomCurrencySingleCoin
    {
        public GoldCurrencySystem(int coinItemID, long currencyCap) : base(coinItemID, currencyCap)
        {
            CurrencyTextKey = Language.GetTextValue("Mods.testMod1.Currency.GoldBagCurrency");
            CurrencyTextColor = Color.Yellow;
        }

        public GoldCurrencySystem(int coinItemId, long currencyCap, string currencyTextKey, Color currencyTextColor) : base(coinItemId, currencyCap)
        {
            CurrencyTextKey = currencyTextKey;
            CurrencyTextColor = currencyTextColor;
        }
    }
}
