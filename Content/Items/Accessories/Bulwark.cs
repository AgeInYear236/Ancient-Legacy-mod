using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Content.Items.Materials;

namespace AncientLegacyMod.Content.Items.Accessories
{
    public class Bulwark : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.accessory = true;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 8);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var modPlayer = player.GetModPlayer<BulwarkPlayer>();
            modPlayer.hasBulwark = true;

            player.maxMinions += 1;
            player.statDefense += 10;
            player.GetDamage(DamageClass.Summon) += 0.10f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SquireShield, 1)
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddIngredient(ModContent.ItemType<RawFury>(), 10)
                .AddIngredient(ModContent.ItemType<Madstone>(), 25)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }

    public class BulwarkPlayer : ModPlayer
    {
        public bool hasBulwark = false;

        public override void ResetEffects()
        {
            hasBulwark = false;
        }

        public override void PostUpdateEquips()
        {
            if (hasBulwark)
            {
                int minionBonusDefense = (int)(Player.slotsMinions * 2);
                Player.statDefense += minionBonusDefense;

                if (Player.slotsMinions >= 5)
                {
                    Player.noKnockback = true;
                }
            }
        }

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            if (hasBulwark && Player.slotsMinions > 0)
            {
                float reduction = Math.Min(Player.slotsMinions * 0.01f, 0.15f);
                modifiers.FinalDamage *= (1f - reduction);
            }
        }
    }
}
