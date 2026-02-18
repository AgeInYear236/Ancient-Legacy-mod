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
    public class FireZoneProjectile : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.friendly = true;
            Projectile.tileCollide = false; 
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
            Projectile.DamageType = DamageClass.Ranged; 
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 45; 
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.9f, 0.5f, 0.2f);

            if (Main.rand.NextBool(3))
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0, 0, 100, default, 1.8f);
                d.noGravity = true;
                d.velocity *= 0.5f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 180);
        }
    }
}
