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
    public class ShadowrazeProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 120;
            Projectile.height = 120;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 3;
            Projectile.hide = true; 
        }

        public override void OnKill(int timeLeft)
        {
            int dustCount = 40;
            for (int i = 0; i < dustCount; i++)
            {
                Vector2 velocity = Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / dustCount * i));

                velocity *= 5f;

                Dust d = Dust.NewDustPerfect(
                    Projectile.Center,
                    DustID.Shadowflame,
                    velocity,
                    100,
                    new Color(121, 7, 23),
                    2f
                );
                d.noGravity = true;
            }
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
        }

    }
}
