using AncientLegacyMod.Common.Rarity;
using AncientLegacyMod.Content.Buffs;
using AncientLegacyMod.Content.Items.Materials;
using AncientLegacyMod.Content.Items.Misc;
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

namespace AncientLegacyMod.Content.Items.Weapons
{
    public class BallLightning : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 5;
            Item.crit = 0;
            Item.DamageType = DamageClass.Magic;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.rare = ModContent.GetInstance<MagicRarity2>().Type;
            Item.shoot = ModContent.ProjectileType<Projectiles.BallLightningProjectile>();
            Item.shootSpeed = 0f;
        }

        public override void HoldItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                player.ChangeDir(Main.MouseWorld.X < player.Center.X ? -1 : 1);
            }

            Vector2 dir = (Main.MouseWorld - player.MountedCenter).SafeNormalize(Vector2.UnitX);

            player.itemRotation = dir.ToRotation() + MathHelper.PiOver4;

            if (player.direction == -1)
            {
                player.itemRotation += MathHelper.PiOver2;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Star, 10);
            recipe.AddIngredient(ItemID.ManaCrystal, 3);
            recipe.AddIngredient(ItemID.Ectoplasm, 10);
            recipe.AddIngredient(ModContent.ItemType<MagicEnergy>(), 10);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

    }

    public class StormPlayer : ModPlayer
    {
        public bool isBallLightning;
        public bool hasOverloadSet; 
        private int lastMana;

        public override void ResetEffects()
        {
            isBallLightning = false;
            hasOverloadSet = false;
        }

        public override void PostUpdate()
        {
            if (hasOverloadSet && Player.statMana < lastMana)
            {
                Player.AddBuff(ModContent.BuffType<OverloadBuff>(), 180);
            }
            lastMana = Player.statMana;
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Player.HasBuff<OverloadBuff>())
            {
                TriggerOverloadExplosion(target);
            }
        }

        private void TriggerOverloadExplosion(NPC target)
        {
            float explosionRadius = 120f;
            int explosionDamage = 50;

            for (int i = 0; i < 20; i++)
            {
                Dust d = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Electric, 0, 0, 100, default, 1.5f);
                d.velocity *= 3f;
                d.noGravity = true;
            }

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && npc.Distance(target.Center) < explosionRadius)
                {
                    npc.StrikeNPC(new NPC.HitInfo { Damage = explosionDamage, Knockback = 2f, HitDirection = 1 });
                    npc.AddBuff(ModContent.BuffType<OverloadSlowBuff>(), 180);
                }
            }
        }
    }
}
