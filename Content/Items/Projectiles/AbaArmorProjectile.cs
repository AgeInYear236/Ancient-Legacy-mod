using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace testMod1.Content.Items.Projectiles
{
    public class AbaArmorProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Vortex, Projectile.velocity * 0.2f, 100, Color.Red, 1.2f);
            d.noGravity = true;

            float maxDetectRadius = 600f;
            NPC closestNPC = null;

            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy() && Projectile.Distance(npc.Center) < maxDetectRadius)
                {
                    closestNPC = npc;
                    maxDetectRadius = Projectile.Distance(npc.Center);
                }
            }

            if (closestNPC != null)
            {
                Vector2 targetDir = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.UnitX);
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, targetDir * 12f, 0.08f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            int healAmount = Main.rand.Next(25, 41);
            player.statLife += healAmount;
            player.HealEffect(healAmount); 

            if (player.statLife > player.statLifeMax2) player.statLife = player.statLifeMax2;

            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item4, target.Center);

            for (int i = 0; i < 15; i++)
            {
                Dust.NewDust(target.position, target.width, target.height, DustID.PurpleTorch, 0, 0, 0, Color.Red, 1.5f);
            }
        }
    }
}