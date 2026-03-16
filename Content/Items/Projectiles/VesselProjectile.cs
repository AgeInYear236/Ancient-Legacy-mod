using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;

namespace AncientLegacyMod.Content.Items.Projectiles
{
    public class VesselProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit, 0, 0, 150, new Color(0,255,0), 1.2f);
            d.noGravity = true;
            d.velocity *= 0.5f;

            NPC target = FindClosestNPC(400f);
            if (target != null)
            {
                Vector2 direction = target.Center - Projectile.Center;
                direction.Normalize();
                direction *= 8f;
                Projectile.velocity = (Projectile.velocity * 20f + direction) / 21f;
            }
        }

        private NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;
            float shortestDistance = maxDetectDistance;

            foreach (var npc in Main.npc.Where(n => n.CanBeChasedBy()))
            {
                float distance = Vector2.Distance(npc.Center, Projectile.Center);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestNPC = npc;
                }
            }
            return closestNPC;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            player.statLife += 10;
            player.HealEffect(10);

            if (player.statLife > player.statLifeMax2)
            {
                player.statLife = player.statLifeMax2;
            }
        }
    }
}