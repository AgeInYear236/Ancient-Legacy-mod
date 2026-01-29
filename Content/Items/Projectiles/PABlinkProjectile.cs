using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace testMod1.Content.Items.Projectiles
{
    public class PABlinkProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 5;
            Projectile.hide = true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            NPC target = FindClosestEnemy(player, 300f);

            if (target != null)
            {
                int direction = (player.Center.X < target.Center.X) ? -1 : 1;
                player.Center = target.Center + new Vector2(direction * 45f, 0f);
                player.velocity = Vector2.Zero;
                player.direction = -direction;

                var hit = target.CalculateHitInfo(100, player.direction, false, 5);
                target.StrikeNPC(hit);

                for (int i = 0; i < 15; i++)
                {
                    Dust.NewDust(player.position, player.width, player.height, DustID.GreenFairy, 0, 0);
                    Dust.NewDust(target.position, target.width, target.height, DustID.TerraBlade);
                }
                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item71, target.Center); 

                player.AddBuff(ModContent.BuffType<Buffs.NoBloody>(), 600);

                if (Main.netMode != NetmodeID.SinglePlayer)
                {
                    NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
                }
            }

            Projectile.Kill();
        }

        private NPC FindClosestEnemy(Player player, float range)
        {
            NPC closest = null;
            float closestDist = range;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy() && !npc.friendly)
                {
                    float dist = Vector2.Distance(player.Center, npc.Center);
                    if (dist < closestDist)
                    {
                        closestDist = dist;
                        closest = npc;
                    }
                }
            }
            return closest;
        }
    }
}