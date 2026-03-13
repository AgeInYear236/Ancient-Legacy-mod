using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Projectiles
{
    public class FractureShard : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.aiStyle = 1;
            Projectile.timeLeft = 90;
            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.timeLeft > 60)
            {
                return false;
            }
            return null;
        }

        public override void AI()
        {
            if (Projectile.timeLeft <= 60)
            {
                if (Main.rand.NextBool(5))
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Stone);
                }
            }

            Projectile.rotation += 0.3f;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Stone);
            }
        }
    }
}