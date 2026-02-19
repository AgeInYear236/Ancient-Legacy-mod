using Microsoft.Xna.Framework;
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
    public class AirWall : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 120;
            Projectile.friendly = true;
            Projectile.tileCollide = false; 
            Projectile.timeLeft = 240;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }

        public override void AI()
        {
            if (Projectile.alpha > 150) Projectile.alpha -= 5;

            if (Main.rand.NextBool(2))
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Cloud, 0, -2f, 100, new Color(224,224,224), 1.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity.X *= 0.3f;
            }
            if (Main.rand.NextBool(2))
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Cloud, 0, -2f, 100, new Color(153,255,255), 1.8f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity.X *= 0.4f;
            }
            if (Main.rand.NextBool(2))
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Cloud, 0, -2f, 100, new Color(204, 229, 255), 1.6f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity.X *= 0.5f;
            }

            Player player = Main.LocalPlayer; 

            if (player.active && !player.dead && player.Hitbox.Intersects(Projectile.Hitbox))
            {
                player.velocity.Y = -15f;

                if (player.justJumped || Main.GameUpdateCount % 30 == 0)
                {
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
                }

                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(player.position, player.width, player.height, DustID.Ash, 0, -5f);
                }
            }
        }

        public override bool? CanDamage() => false;
    }
}
