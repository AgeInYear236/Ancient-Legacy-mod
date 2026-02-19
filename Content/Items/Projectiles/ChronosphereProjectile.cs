using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Projectiles
{
    public class ChronosphereProjectile : ModProjectile
    {
        private const float Radius = 250f;
        private float visualAlpha = 0f;

        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Projectile.Center = player.Center;

            if (Projectile.timeLeft > 540)
            {
                if (visualAlpha < 0.4f) visualAlpha += 0.05f;
            }
            else if (Projectile.timeLeft < 100)
            {
                visualAlpha -= 0.03f; 
            }
            else
            {
                visualAlpha = 0.4f;
            }

            if (!player.HasBuff(ModContent.BuffType<Buffs.ChronosphereBuff>()))
            {
                Projectile.Kill();
            }

            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && Vector2.Distance(Projectile.Center, npc.Center) < Radius)
                {
                    npc.velocity *= 0.1f;
                }
            }

            foreach (Projectile p in Main.projectile)
            {
                if (p.active && p.hostile && Vector2.Distance(Projectile.Center, p.Center) < Radius)
                {
                    p.velocity *= 0.1f;
                }
            }

            if (Main.rand.NextBool(2) && visualAlpha > 0.3f)
            {
                Vector2 pos = Projectile.Center + Main.rand.NextVector2CircularEdge(Radius, Radius);
                Dust d = Dust.NewDustDirect(pos, 0, 0, DustID.ShadowbeamStaff);
                d.noGravity = true;
                d.velocity = player.velocity;
                d.scale = 1.2f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
    }
}