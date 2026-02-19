using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Consumables
{
    public class Mango : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 24;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item2;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 8);
        }

        public override bool CanUseItem(Player player)
        {
            if (player.statMana >= player.statManaMax2)
                return false;

            if (player.HasBuff(ModContent.BuffType<MangoHealBuff>()))
                return false;

            return true;
        }

        public override bool? UseItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<MangoHealBuff>(), 300);
            return true;
        }
    }

    public class MangoHealBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.buffTime[buffIndex] > 0 && player.buffTime[buffIndex] % 6 == 0)
            {
                player.statMana += 1;
                if (player.statMana > player.statManaMax2)
                    player.statMana = player.statManaMax2;

            }
        }
    }
}
