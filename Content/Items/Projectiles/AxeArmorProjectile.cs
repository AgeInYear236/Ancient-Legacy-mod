using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Projectiles
{
    public class AxeArmorProjectile : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 21;
            Projectile.height = 21;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee; 
            Projectile.penetrate = -1; 
            Projectile.timeLeft = 90;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;

            // This ensures it doesn't hit the same target every single frame
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 100;
        }

        public override void AI()
        {
            Projectile.alpha += 3;
            if (Projectile.alpha >= 255) Projectile.Kill();

            Projectile.velocity *= 0.7f;
            Projectile.rotation += 0.2f;

            if (Main.rand.NextBool(2))
            {
                Dust d = Dust.NewDustDirect(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.RedTorch,
                    0f, 0f,
                    100,
                    default,
                    1f
                );

                d.noGravity = true;
                d.velocity *= 0.3f; 
                d.scale *= 1.2f;
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 15; i++)
            {
                Vector2 speed = Main.rand.NextVector2Unit() * 2f;
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.RedMoss, speed, 100, default, 1.5f);
                d.noGravity = true;
                d.fadeIn = 1.2f;
            }
        }
    }
}
