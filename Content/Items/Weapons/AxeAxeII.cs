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
    public class AxeAxeII : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Melee;
            Item.width = 55;
            Item.height = 55;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightPurple;
            Item.UseSound = SoundID.Item1; // Play axe sound
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
                Item.height = 100;
                Item.width = 100;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.useTime = 100;
                Item.useAnimation = 100;
                Item.damage = 40;
                Item.UseSound = SoundID.Item1; // Play axe sound
            }
            return base.CanUseItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.altFunctionUse == 2)
            {
                int twentyPercentHealth = (int)(target.lifeMax * 0.35f);

                if (target.boss == false && target.life <= twentyPercentHealth)
                {
                    NPC.HitInfo killHit = new NPC.HitInfo();
                    killHit.Damage = 99999;
                    target.StrikeNPC(killHit);
                }
                else
                {
                    player.Hurt(PlayerDeathReason.ByCustomReason($"{player.name} severely cut himself."), 500, 0);
                }
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AxeAxe>(), 1)
                .AddIngredient(ItemID.AdamantiteBar, 20)
                .AddIngredient(ItemID.SoulofMight, 5)
                .AddIngredient(ItemID.SoulofFright, 5)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
