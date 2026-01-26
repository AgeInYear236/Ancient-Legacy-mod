using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace testMod1.Content.Items.Projectiles
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

            Projectile.velocity *= 0.9f;

            Projectile.rotation += 0.3f;
        }
    }
}
