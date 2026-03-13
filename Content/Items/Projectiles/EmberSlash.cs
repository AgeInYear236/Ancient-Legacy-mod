using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Projectiles
{
    public class EmberSlash : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 35;
            Projectile.hide = true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active)
            {
                Projectile.Kill();
                return;
            }

            player.velocity = Vector2.Zero;

            player.immune = true;
            player.immuneTime = 2;
            player.invis = true;

            if (Projectile.timeLeft == 30 || Projectile.timeLeft == 15)
            {
                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item71, Projectile.Center);

                for (int i = 0; i < 25; i++)
                {
                    Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width/2, Projectile.height/2, DustID.SolarFlare, 0, 0, 0, default, 2f);
                    d.noGravity = true;
                    d.velocity *= 5f;
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            player.invis = false;
            player.AddBuff(BuffID.Slow, 180);
        }
    }
}