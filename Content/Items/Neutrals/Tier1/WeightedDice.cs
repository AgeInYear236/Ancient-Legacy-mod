using AncientLegacyMod.Common.Rarity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Neutrals.Tier1
{
    public class WeightedDice : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<NeutralRarity>().Type;
            Item.value = Item.sellPrice(gold: 1);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DicePlayer>().hasDice = true;
        }
    }

    public class DicePlayer : ModPlayer
    {
        public bool hasDice;

        public override void ResetEffects()
        {
            hasDice = false;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (hasDice)
            {
                int roll = Main.rand.NextBool() ? 10 : -10;

                modifiers.SourceDamage.Flat += roll;

                if (roll > 0)
                {
                    Dust.QuickDust(target.Center, Color.Gold);
                }
                else
                {
                    Dust.QuickDust(target.Center, Color.Gray);
                }
            }
        }
    }
}
