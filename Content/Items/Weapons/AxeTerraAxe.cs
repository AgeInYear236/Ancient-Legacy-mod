using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace testMod1.Content.Items.Weapons
{
    public class AxeTerraAxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.DamageType = DamageClass.Melee;
            Item.width = 70;
            Item.height = 70;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = ItemRarityID.Yellow;
            Item.autoReuse = true;

            // Axe Power
            Item.axe = 40; // 20 * 5 = 100% axe power. 15 = 75% power.
        }

        // 1. Tell the game this item can use an "Alt Function" (Right Click)
        public override bool AltFunctionUse(Player player)
        {
            Main.NewText("Alt used");
            return true;
        }

        // 2. Change how the item behaves depending on which button is clicked
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                // RMB (Special Attack) Stats
                // Use the same swing style and power, but maybe different timing
                Item.useStyle = ItemUseStyleID.Swing;
                Item.useTime = 10; // Slower for the special attack
                Item.useAnimation = 10;
                Item.damage = 1; // Base damage doesn't matter much as we override the hit logic
                Item.axe = 0;
            }
            else
            {
                // LMB (Standard Axe) Stats
                Item.useStyle = ItemUseStyleID.Swing;
                Item.useTime = 100;
                Item.useAnimation = 100;
                Item.damage = 60;
                Item.UseSound = SoundID.Item1; // Play axe sound
            }
            return base.CanUseItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.altFunctionUse == 2)
            {
                int twentyPercentHealth = (int)(target.lifeMax * 0.4f);

                if (target.boss == false && target.life <= twentyPercentHealth)
                {
                    NPC.HitInfo killHit = new NPC.HitInfo();
                    killHit.Damage = 99999;
                    target.StrikeNPC(killHit);
                }
                else
                {
                    player.Hurt(PlayerDeathReason.ByCustomReason($"{player.name} demolished himself."), 2000, 0);
                }
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AxeAxe>(), 1)
                .AddIngredient(ModContent.ItemType<AxeAxeII>(), 1)
                .AddIngredient(ItemID.BrokenHeroSword, 2)
                .AddIngredient(ItemID.LihzahrdPowerCell, 1)
                .AddIngredient(ItemID.ChlorophyteBar, 12)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
