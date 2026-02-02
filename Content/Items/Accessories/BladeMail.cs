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

namespace testMod1.Content.Items.Accessories
{
    public class BladeMail : ModItem
    {
        public static bool isActive = false;
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<AccRarityPas>().Type;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 5;
            var modPlayer = player.GetModPlayer<modPlayer1>();
            modPlayer.isBMed = true;

            if (isActive && !player.HasBuff(ModContent.BuffType<BMCooldownBuff>()))
            {
                player.AddBuff(ModContent.BuffType<BMBuff>(), 8 * 60);
                player.AddBuff(ModContent.BuffType<BMCooldownBuff>(), 1 * 60);
            }
        }
    }
}
