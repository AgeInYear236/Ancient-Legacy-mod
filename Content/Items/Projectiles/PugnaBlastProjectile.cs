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
    public class PugnaBlastProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 60;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(2))
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.TerraBlade, 0, 0, 150, new Color(200, 255, 0), 1.2f);
                d.noGravity = true;
                d.velocity *= 0.5f;
            }
            Projectile.rotation += 0.2f;
        }

        public override void OnKill(int timeLeft)
        {
            float explosionRadius = 60f;

            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            for (int i = 0; i < 50; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(8f, 8f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.TerraBlade, speed, 100, new Color(200, 255, 0), 2f);
                d.noGravity = true;
                d.fadeIn = 1.5f;
            }

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC target = Main.npc[i];
                if (target.active && !target.friendly && target.Distance(Projectile.Center) < explosionRadius)
                {
                    int damage = Projectile.damage;

                    target.StrikeNPC(target.CalculateHitInfo(damage, 0, false, Projectile.knockBack));

                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        NetMessage.SendData(MessageID.DamageNPC, -1, -1, null, i, damage);
                    }
                }
            }
        }
    }
}
