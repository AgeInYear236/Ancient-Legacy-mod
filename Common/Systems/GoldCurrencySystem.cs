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
using AncientLegacyMod.Content.Items.Misc;

namespace AncientLegacyMod.Common.Systems
{
    public class GoldCurrencySystem : CustomCurrencySingleCoin
    {
        public GoldCurrencySystem(int coinItemID, long currencyCap) : base(coinItemID, currencyCap)
        {
            CurrencyTextKey = Language.GetTextValue("Mods.AncientLegacyMod.Currency.GoldBagCurrency");
            CurrencyTextColor = Color.Yellow;
        }

        public GoldCurrencySystem(int coinItemId, long currencyCap, string currencyTextKey, Color currencyTextColor) : base(coinItemId, currencyCap)
        {
            CurrencyTextKey = currencyTextKey;
            CurrencyTextColor = currencyTextColor;
        }
    }
}
