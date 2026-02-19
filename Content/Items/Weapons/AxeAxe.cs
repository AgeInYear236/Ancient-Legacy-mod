using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using AncientLegacyMod.Common.Rarity;

namespace AncientLegacyMod.Content.Items.Weapons
{
    public class AxeAxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = ModContent.GetInstance<MeleeRarity>().Type;
            Item.autoReuse = true;

            Item.axe = 30;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.damage = 1;
                Item.axe = 0;
            }
            else
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.useTime = 100;
                Item.useAnimation = 100;
                Item.damage = 25;
                Item.UseSound = SoundID.Item1;
            }
            return base.CanUseItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.altFunctionUse == 2)
            {
                int twentyPercentHealth = (int)(target.lifeMax * 0.2f);

                if (target.boss == false && target.life <= twentyPercentHealth)
                {
                    NPC.HitInfo killHit = new NPC.HitInfo();
                    killHit.Damage = 99999;
                    target.StrikeNPC(killHit);
                }
                else
                {
                    player.Hurt(PlayerDeathReason.ByCustomReason(player.name + Language.GetTextValue("Mods.AncientLegacyMod.Death.Axe")), 200, 0);
                }
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HellstoneBar, 15)
                .AddIngredient(ItemID.TissueSample, 10)
                .AddIngredient(ItemID.Bone, 30)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
