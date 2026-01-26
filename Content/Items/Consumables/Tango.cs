using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace testMod1.Content.Items.Consumables
{
    public class Tango : ModItem
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
            if (player.statLife >= player.statLifeMax2)
                return false;

            if (player.HasBuff(ModContent.BuffType<TangoHealBuff>()))
                return false;

            return true;
        }

        public override bool? UseItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<TangoHealBuff>(), 300);
            return true;
        }
    }

    public class TangoHealBuff : ModBuff
    {
        Texture texture = null;
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.buffTime[buffIndex] > 0 && player.buffTime[buffIndex] % 4 == 0)
            {
                player.statLife += 1;
                if (player.statLife > player.statLifeMax2)
                    player.statLife = player.statLifeMax2;

            }
        }
    }
}
