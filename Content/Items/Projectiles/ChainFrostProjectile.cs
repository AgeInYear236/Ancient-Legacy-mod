using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace testMod1.Content.Items.Projectiles
{
    public class ChainFrostProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            ExecuteExplosion();

            if (Projectile.ai[0] < 5)
            {
                NPC nextTarget = FindNextTarget(target, 500f);

                if (nextTarget != null)
                {
                    Vector2 direction = nextTarget.Center - Projectile.Center;
                    direction.Normalize();
                    Vector2 velocity = direction * 12f;

                    Projectile.NewProjectile(
                        Projectile.GetSource_FromThis(),
                        Projectile.Center + direction * 5f, 
                        velocity,
                        Projectile.type,
                        Projectile.damage,
                        Projectile.knockBack,
                        Projectile.owner,
                        Projectile.ai[0] + 1
                    );
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            ExecuteExplosion();
        }

        private void ExecuteExplosion()
        {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int i = 0; i < 15; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.IceTorch, 0, 0, 100, default, 1.2f);
            }
        }

        private NPC FindNextTarget(NPC currentTarget, float range)
        {
            NPC closest = null;
            float closestDist = range;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc != currentTarget && npc.CanBeChasedBy())
                {
                    float dist = Vector2.Distance(Projectile.Center, npc.Center);
                    if (dist < closestDist)
                    {
                        closestDist = dist;
                        closest = npc;
                    }
                }
            }
            return closest;
        }
    }
}
