using AncientLegacyMod.Content.Items.Weapons;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Map;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Projectiles
{
    public class BallLightningProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!player.channel || player.dead || player.statMana <= 1)
            {
                Projectile.Kill();
                return;
            }

            player.GetModPlayer<StormPlayer>().isBallLightning = true;

            player.statMana -= 3;
            player.manaRegenDelay = 60;
            player.immune = true;
            player.immuneTime = 2;

            Vector2 direction = Main.MouseWorld - player.Center;
            if (direction != Vector2.Zero && direction.Length() > 10f)
            {
                direction.Normalize();
                player.velocity = direction * 14f;
            }
            else
            {
                player.velocity = Vector2.Zero;
            }
            Projectile.Center = player.Center;


            for (int i = 0; i < 4; i++)
            {
                Vector2 pos = Projectile.Center + new Vector2(24, 0).RotatedBy(Projectile.rotation + i * MathHelper.PiOver2);
                Dust d = Dust.NewDustPerfect(pos, DustID.Electric, Vector2.Zero, 100, default, 1.2f);
                d.noGravity = true;

                Dust d2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, 0, 0, 100, default, 0.8f);
                d2.noGravity = true;
                d2.velocity *= 2f;
            }

            Lighting.AddLight(Projectile.Center, 0.4f, 0.7f, 1f);
        }
    }
}
