using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace testMod1.Content.Items.Projectiles
{
    public class ThunderStrikeProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 40;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 2;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, Color.Cyan.ToVector3() * 0.7f);

            Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, 0, 0, 100, default, 1.0f);
            d.noGravity = true;
            d.velocity *= 0.1f;
        }
    }
}