using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Content.Buffs;

namespace testMod1.Content.Items.Projectiles
{
    public class MagnusPullProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 400;
            Projectile.height = 400;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 2;
            Projectile.hide = true;
        }

        public override void OnSpawn(Terraria.DataStructures.IEntitySource source)
        {
            float pullRadius = 350f;
            Vector2 pullTarget = Main.player[Projectile.owner].Center;

            for (int i = 0; i < 40; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(10f, 10f);
                Dust d = Dust.NewDustPerfect(pullTarget + speed * 20, DustID.BlueFlare, -speed * 2);
                d.noGravity = true;
                d.scale = 1.5f;
            }

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC target = Main.npc[i];

                if (target.CanBeChasedBy() && target.Distance(pullTarget) < pullRadius)
                {
                    Vector2 direction = pullTarget - target.Center;
                    float distance = direction.Length();

                    float pullStrength = MathHelper.Clamp(distance * 0.15f, 5f, 20f);
                    target.velocity = Vector2.Normalize(direction) * pullStrength;

                    target.velocity *= 0f;
                    target.position = pullTarget - new Vector2(target.width / 2, target.height / 2);

                    target.AddBuff(ModContent.BuffType<StunBuff>(), 120);

                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, i);
                }
            }
        }
    }
}
