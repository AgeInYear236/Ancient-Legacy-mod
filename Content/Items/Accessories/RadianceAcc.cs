using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Common.Rarity;
using testMod1.Content.Buffs;
using testMod1.Content.Players;

namespace testMod1.Content.Items.Accessories
{
    public class RadianceAcc : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<AccRarityAura>().Type;
            Item.value = Item.buyPrice(gold: 2);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(ModContent.BuffType<RadianceBuff>(), 2);

            var modPlayer = player.GetModPlayer<RadiancePlayer>();
            modPlayer.radianceActive = true;
            modPlayer.radianceRadius = 140f; 
            modPlayer.damage = 15;
        }
    }
}
