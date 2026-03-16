using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Projectiles
{
    public class ParasmaProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 180;
            Projectile.aiStyle = 0;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            for (int i = 0; i < 3; i++)
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GreenTorch, 0, 0, 100, default, 1.2f);
                d.noGravity = true;
                d.velocity *= 1.5f;
                d.scale *= 1.1f;
            }

            Projectile.ai[0] += 1f;

            float frequency = 5f;
            float amplitude = 3f;

            float wave = (float)Math.Sin(Projectile.ai[0] * frequency) * amplitude;

            Vector2 direction = Vector2.Normalize(Projectile.velocity);
            Vector2 perpendicular = new Vector2(-direction.Y, direction.X);

            Projectile.velocity += perpendicular * (float)Math.Cos(Projectile.ai[0] * frequency) * amplitude * frequency * 0.1f;

            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Buffs.ParasmaDebuff>(), 300);
        }
    }
}