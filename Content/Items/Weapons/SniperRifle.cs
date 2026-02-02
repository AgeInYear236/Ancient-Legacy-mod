using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Content.Items.Projectiles;

namespace testMod1.Content.Items.Weapons
{
    public class SniperRifle : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.crit = 30;
            Item.knockBack = 1;

            Item.width = 42;
            Item.height = 20;

            Item.useTime = 25;
            Item.useAnimation = 25;

            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<SniperProjectile>();

            Item.noMelee = true;
            Item.value = 10000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item45;
            Item.autoReuse = true;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-4, 2);

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) 
            {
                Item.useTime = 4;
                Item.useAnimation = 80;
                Item.autoReuse = false;
            }
            else
            {
                Item.useTime = 25;
                Item.useAnimation = 25;
                Item.autoReuse = true;
            }
            return base.CanUseItem(player);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                velocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));

                damage = (int)(damage * 0.7f);
            }
        }

        public override void HoldItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.itemAnimation > 0)
            {
                player.velocity.X *= 0.1f;
                player.controlLeft = false;
                player.controlRight = false;
                player.controlJump = false;

                if (player.itemTime == Item.useTime) 
                {
                    player.velocity -= velocityToDirection(player) * 0.5f;
                }
            }

        }

        private Vector2 velocityToDirection(Player player) =>
            (Main.MouseWorld - player.MountedCenter).SafeNormalize(Vector2.UnitX);

    }
}
