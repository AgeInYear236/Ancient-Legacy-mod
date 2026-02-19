using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace AncientLegacyMod.Content.Items.Projectiles
{
    public class TechiesMineProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 3600;
            Projectile.penetrate = -1;

            //Projectile.DamageType = DamageClass.Generic;
        }

        public override bool? CanDamage() => false;

        public override void AI()
        {
            Projectile.velocity.Y += 0.4f;

            if (Projectile.velocity.Y > 10f) Projectile.velocity.Y = 10f;

            if (Main.rand.NextBool(40))
            {
                Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.RedTorch);
                d.noGravity = true;
                d.velocity *= 0.5f;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item62 with { Pitch = -0.2f, Volume = 1.2f }, Projectile.Center);

            int explosionRadius = 160;
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileID.SolarWhipSwordExplosion, 0, 0, Projectile.owner); // Просто визуал

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && npc.Distance(Projectile.Center) < explosionRadius)
                {
                    npc.SimpleStrikeNPC(Projectile.damage / 2, Projectile.direction, true, Projectile.knockBack);
                }
            }

            for (int i = 0; i < 40; i++)
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0, 0, 100, default, 2.5f);
                d.velocity *= 5f;
                if (Main.rand.NextBool(2))
                {
                    d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0, 0, 0, default, 3f);
                    d.noGravity = true;
                    d.velocity *= 8f;
                }
            }
        }
    }
}