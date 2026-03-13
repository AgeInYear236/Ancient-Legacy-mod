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
    public class VoidRemnant : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 48;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(2))
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Demonite, 0, 0, 150, default, 1.5f);
                d.noGravity = true;
                d.velocity *= 0.5f;
            }

            Lighting.AddLight(Projectile.Center, 0.6f, 0.1f, 0.8f);

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && npc.Distance(Projectile.Center) < 500f)
                {
                    if (npc.noTileCollide || Collision.CanHit(npc.Center, 1, 1, Projectile.Center, 1, 1))
                    {
                        npc.target = 255;
                    }
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, 0, 0, 0, default, 2f);
                d.noGravity = true;
                d.velocity *= 3f;
            }
        }
    }
}
