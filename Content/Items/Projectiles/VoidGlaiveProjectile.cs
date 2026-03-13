using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Projectiles
{
    public class VoidGlaiveProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = true;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active)
            {
                Projectile.Kill();
                return;
            }

            Projectile.rotation += 0.5f * Projectile.direction;

            if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[1] += 1f;

                if (Projectile.ai[1] >= 30f)
                {
                    Projectile.velocity *= 0.92f;

                    if (Projectile.velocity.Length() < 2f)
                    {
                        Projectile.ai[0] = 1f;
                    }
                }
            }
            else
            {
                float returnSpeed = 16f;
                float acceleration = 0.6f;

                Vector2 vectorToPlayer = player.Center - Projectile.Center;
                float distance = vectorToPlayer.Length();

                if (distance < 20f)
                {
                    Projectile.Kill();
                }

                vectorToPlayer.Normalize();
                vectorToPlayer *= returnSpeed;

                Projectile.velocity.X = (Projectile.velocity.X * 20f + vectorToPlayer.X) / 21f;
                Projectile.velocity.Y = (Projectile.velocity.Y * 20f + vectorToPlayer.Y) / 21f;

                Projectile.tileCollide = false;
            }

            if (Main.rand.NextBool(2))
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, 0, 0, 150, default, 1.2f);
                d.noGravity = true;
                d.velocity *= 0.2f;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[0] = 1f;
                Terraria.Audio.SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
            }
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.ShadowFlame, 180);
        }
    }
}