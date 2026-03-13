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
    public class EmberRemnant : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 48;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.timeLeft = 1200;
            Projectile.alpha = 100;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(3))
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0, 0, 100, default, 1f);
                d.noGravity = true;
                d.velocity *= 0.5f;
            }
            Lighting.AddLight(Projectile.Center, 0.5f, 0.7f, 1f);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 15; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Firefly);
            }
        }
    }
}
