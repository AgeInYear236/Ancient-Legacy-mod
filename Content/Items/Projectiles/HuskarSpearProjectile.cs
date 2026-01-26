using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Common.Systems;
using testMod1.Content.Items.Weapons;

namespace testMod1.Content.Items.Projectiles
{
    public class HuskarSpearProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;

            Projectile.aiStyle = ProjAIStyleID.Arrow;
            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);

            if (Projectile.velocity.X < 0)
            {
                Projectile.spriteDirection = -1; 
                Projectile.rotation += MathHelper.ToRadians(90f);
            }
            else
            {
                Projectile.spriteDirection = 1;
            }

            if (Main.rand.NextBool(2))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 0, 0, 0, default, 2f);
                dust.noGravity = true;
                dust.velocity *= 0.5f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<HuskarSpearProjectileBuff>(), 300);

            var globalNPC = target.GetGlobalNPC<BurnGlobalNPC>();
            globalNPC.burnDamage = HuskarSpear.itemDamage;

        }
    }
}
