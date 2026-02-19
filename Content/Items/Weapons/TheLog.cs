using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Common.Rarity;
using AncientLegacyMod.Content.Items.Misc;
using AncientLegacyMod.Content.Items.Projectiles;

namespace AncientLegacyMod.Content.Items.Weapons
{
    public class TheLog : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 90;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4f;
            Item.rare = ModContent.GetInstance<MeleeRarity3>().Type;
            Item.UseSound = SoundID.Item1;

            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;

            Item.shoot = ModContent.ProjectileType<TheLogSpear>();
            Item.shootSpeed = 0.8f;
        }

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[ItemID.PossessedHatchet] = ModContent.ItemType<TheLog>();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.mana = 50;
                Item.DamageType = DamageClass.Magic;
            }
            else
            {
                Item.mana = 0;
                Item.DamageType = DamageClass.Melee;
            }

            return player.ownedProjectileCounts[ModContent.ProjectileType<TheLogSpear>()] < 1 &&
                   player.ownedProjectileCounts[ModContent.ProjectileType<EchoSlam>()] < 1;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                type = ModContent.ProjectileType<EchoSlam>();
                damage = (int)(damage * 0.8f);
            }
        }

        public override void HoldItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                float rangeMin = 50f;
                float rangeMax = 100f;

                if (Main.GameUpdateCount % 3 == 0)
                {
                    DrawRangePoint(player.MountedCenter + new Vector2(-rangeMin, 0));
                    DrawRangePoint(player.MountedCenter + new Vector2(-rangeMax, 0));

                    DrawRangePoint(player.MountedCenter + new Vector2(rangeMin, 0));
                    DrawRangePoint(player.MountedCenter + new Vector2(rangeMax, 0));
                }
            }
        }

        private void DrawRangePoint(Vector2 position)
        {
            Dust d = Dust.NewDustPerfect(position, DustID.WoodFurniture, Vector2.Zero, 150, default, 1.2f);
            d.noGravity = true;

            if (Main.LocalPlayer.statMana >= 50)
            {
                Dust dMagic = Dust.NewDustPerfect(position, DustID.Stone, Vector2.Zero, 100, Color.Brown, 1f);
                dMagic.noGravity = true;
            }
        }
    }
}
