using AncientLegacyMod.Common.Rarity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Neutrals.Tier3
{
    public class Doubloon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<NeutralRarity>().Type;
            Item.value = Item.sellPrice(gold: 3);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Просто помечаем, что аксессуар надет
            player.GetModPlayer<DoubloonPlayer>().hasDoubloon = true;
        }
    }

    public class DoubloonPlayer : ModPlayer
    {
        public bool hasDoubloon;

        public override void ResetEffects()
        {
            hasDoubloon = false;
        }

        public override void PostUpdateEquips()
        {
            if (hasDoubloon)
            {
                int totalPoints = Player.statLifeMax2 + Player.statManaMax2;
                int average = totalPoints / 2;

                Player.statLifeMax2 = average;
                Player.statManaMax2 = average;
            }
        }
    }
}
